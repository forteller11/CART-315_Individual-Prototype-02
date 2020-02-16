using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;
using Collider = Unity.Physics.Collider;
using Math = System.Math;
using RaycastHit = Unity.Physics.RaycastHit;

namespace MarchingCubes.Systems
{
    public class ChangePointDensitySynchronous : ComponentSystem
    {
        private CollisionFilter _chunkFilter = new CollisionFilter
        {
            BelongsTo = 0xffffffff,
            CollidesWith = 0b_0001,
            GroupIndex = 0,
        };

        private float _buildRadius = 2f;
        private float _maxBuildRate = 0.1f;
        private float _minBuildRate = 0;
        private PlayerControls _input;
        
        
        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            _input = new PlayerControls();
            _input.Enable();
        }
        
        protected override void OnStopRunning()
        {
            base.OnStopRunning();
            _input.Disable();
        }

        protected override void OnUpdate()
        {
            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
            var buildPhysicsWorld = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
            var collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;
            EntityQuery settingsQuery = GetEntityQuery(Unity.Entities.ComponentType.ReadOnly<ChunkSettingsSingleton>());
            var chunkSettings = settingsQuery.ToComponentDataArray<ChunkSettingsSingleton>(Allocator.TempJob);

            var camPos = Camera.main.gameObject.transform.position;
            var camDir = Camera.main.gameObject.transform.forward;
            
            
            float buildInput = _input.PlayerMovement.Build.ReadValue<float>() - _input.PlayerMovement.Dig.ReadValue<float>();
            
            //see if entity hit chunk

            if (math.abs(buildInput) < 0.08f) //return if not inputting
                return;
            
            //hitting a chunk?
            Entities.ForEach((ref Translation translation, ref Input input) =>
            {
                //Debug.Log("Found an input entity");

                RaycastInput raycastInput = new RaycastInput
                {
                    Start = (float3) camPos,
                    End = (float3) camDir * 100,
                    Filter = _chunkFilter
                };


                if (collisionWorld.CastRay(raycastInput, out var hitChunk))
                {
                    Debug.DrawLine(camPos, hitChunk.Position, new Color(1f, 0.27f, 0.89f));
                    PointDistanceInput pointDistanceInput = new PointDistanceInput
                    {
                        Position = hitChunk.Position,
                        MaxDistance = _buildRadius,
                        Filter = _chunkFilter
                    };
                    var distanceChunkHits = new NativeList<DistanceHit>(Allocator.Temp);
                    if (collisionWorld.CalculateDistance(pointDistanceInput, ref distanceChunkHits))
                    {
                        for (int i = 0; i < distanceChunkHits.Length; i++)
                        {

                            Debug.DrawLine(distanceChunkHits[i].Position,
                                distanceChunkHits[i].Position + distanceChunkHits[i].SurfaceNormal/3,
                                new Color(1f, 0.55f, 0.25f));

                            var rbIndex = distanceChunkHits[i].RigidBodyIndex;
                            Entity hitDistanceChunkEntity = buildPhysicsWorld.PhysicsWorld.Bodies[rbIndex].Entity;
                            ecs.AddComponent<Tag_DirtyChunk>(hitDistanceChunkEntity);

//                            if (!ecs.HasComponent<ChunkIndex>(hitDistanceChunkEntity)) //didnt hit chunk index
//                                return;

                            var chunkIndexOfHit = ecs.GetComponentData<ChunkIndex>(hitDistanceChunkEntity);
                            var densities = ecs.GetBuffer<ChunkDensities>(hitDistanceChunkEntity).Reinterpret<float>();
                            var distanceHitChunkPos =
                                Utils.GetChunkPos(chunkIndexOfHit.Value, chunkSettings[0].ChunkWidth);
                            
                            Utils.IndexAsIf3D(new int3(chunkSettings[0].VoxelsInARow), (indexFlat, indexSpatial, indexJump) =>
                                {
                                    float3 densityPosModel =
                                        Utils.GetDensityPosModel(chunkSettings[0].WidthBetweenVoxels, indexSpatial);
                                    float3 densityPos = densityPosModel + distanceHitChunkPos;
                                    float distToPoint = math.distance(hitChunk.Position, densityPos);

                                    if (distToPoint < _buildRadius)
                                    {
                                        float amountToChangeDensity = math.lerp(_maxBuildRate, _minBuildRate,
                                            distToPoint / _buildRadius);
                                        densities[indexFlat] += amountToChangeDensity * buildInput;
                                        densities[indexFlat] = math.clamp(densities[indexFlat], 0f, 1f);
                                        Debug.DrawLine(densityPos, densityPos + new float3(.1f, .1f, .1f),
                                            new Color(0.67f, 1f, 0.73f, 0.58f));
                                    }

                                });
                        }
                    }

                    distanceChunkHits.Dispose();
                }
            }); //for each

        }
        
        
    }
}