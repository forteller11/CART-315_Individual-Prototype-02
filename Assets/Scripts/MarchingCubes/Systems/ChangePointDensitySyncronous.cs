using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using Collider = Unity.Physics.Collider;
using RaycastHit = Unity.Physics.RaycastHit;

namespace MarchingCubes.Systems
{
    public class ChangePointDensitySynchronous : ComponentSystem
    {
        private CollisionFilter _chunkFilter = new CollisionFilter
        {
            BelongsTo = 0xffffffff,
            CollidesWith = 1 << 0,
            GroupIndex = 0,
        };
        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            Debug.Log("CHnage Point Desnity Started running!");
        }

        protected override void OnUpdate()
        {
          
            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
            var buildPhysicsWorld = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
            var collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;
            

            var camPos = Camera.main.gameObject.transform.position;
            var camDir = Camera.main.gameObject.transform.forward;
            
            EntityQuery queryPoints = GetEntityQuery(typeof(MarchingPoint), ComponentType.ReadOnly<ChunkIndex>());
        
            Entities.ForEach((ref Translation translation, ref Input input) =>
            {
                Debug.Log("Found an input entity");
                
                RaycastInput raycastInput = new RaycastInput
                {
                    Start = (float3) camPos,
                    End = (float3) camDir * 100,
                    Filter = _chunkFilter
                };
//                
                RaycastHit hitChunk;
                if (collisionWorld.CastRay(raycastInput, out hitChunk))
                {
                    Debug.DrawLine(camPos, hitChunk.Position, new Color(1f, 0.27f, 0.89f));
                    PointDistanceInput pointDistanceInput = new PointDistanceInput
                    {
                        Position = hitChunk.Position,
                        MaxDistance = 1f,
                        Filter = _chunkFilter
                    };
                    var distanceChunkHits = new NativeList<DistanceHit>(Allocator.TempJob);
                    if (collisionWorld.CalculateDistance(pointDistanceInput, ref distanceChunkHits))
                    {
                        Debug.Log($"Collided with {distanceChunkHits.Length} chunks!");
                        for (int i = 0; i < distanceChunkHits.Length; i++)
                        {
                            Debug.DrawLine(distanceChunkHits[i].Position,
                                distanceChunkHits[i].Position + new float3(0.1f, 0.1f, 0.1f),
                                new Color(1f, 0.55f, 0.25f));
                            var rbIndex = distanceChunkHits[i].RigidBodyIndex;
                            Entity hitChunkEntity = buildPhysicsWorld.PhysicsWorld.Bodies[rbIndex].Entity;

                            var chunkIndexOfHit = ecs.GetSharedComponentData<ChunkIndex>(hitChunkEntity);
                            
                            queryPoints.ResetFilter();
                            queryPoints.SetSharedComponentFilter(chunkIndexOfHit);

                            var pointValues = queryPoints.ToComponentDataArray<MarchingPoint>(Allocator.TempJob);
                            var pointEntities = queryPoints.ToEntityArray(Allocator.TempJob);

                            for (int j = 0; j < pointValues.Length; j++)
                            {
                                //Debug.Log("increase density values");
                                ecs.SetComponentData(pointEntities[i], new MarchingPoint
                                {
                                    Density = pointValues[i].Density + 0.01f
                                });
                                var pPos = ecs.GetComponentData<Translation>(pointEntities[i]).Value;
                                Debug.DrawLine(pPos, pPos + new float3(.1f,.1f,.1f), new Color(0.67f, 1f, 0.73f));
                            }

                            pointEntities.Dispose();
                            pointValues.Dispose();
                            
                        }
                    }

                    //marching cubes algo
                    distanceChunkHits.Dispose();
                }
            });
        }
        
        
    }
}