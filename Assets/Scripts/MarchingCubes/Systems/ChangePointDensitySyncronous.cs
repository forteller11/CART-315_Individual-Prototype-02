//using System.Collections.Generic;
//using Unity.Collections;
//using Unity.Entities;
//using Unity.Mathematics;
//using Unity.Physics;
//using Unity.Physics;
//using Unity.Jobs;
//using Unity.Transforms;
//using UnityEngine;
//using Collider = Unity.Physics.Collider;
//using Math = System.Math;
//using RaycastHit = Unity.Physics.RaycastHit;
//
//namespace MarchingCubes.Systems
//{
//    public class ChangePointDensitySynchronous : ComponentSystem
//    {
//        private CollisionFilter _chunkFilter = new CollisionFilter
//        {
//            BelongsTo = 0xffffffff,
//            CollidesWith = 1 << 0,
//            GroupIndex = 0,
//        };
//
//        private float _buildRadius = 2f;
//        private float _maxBuildRate = 0.1f;
//        private float _minBuildRate = 0;
//        private PlayerControls _input;
//        
//        
//        protected override void OnStartRunning()
//        {
//            base.OnStartRunning();
//            _input = new PlayerControls();
//            _input.Enable();
//        }
//        
//        protected override void OnStopRunning()
//        {
//            base.OnStopRunning();
//            _input.Disable();
//        }
//
//        protected override void OnUpdate()
//        {
//            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
//            var buildPhysicsWorld = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
//            var collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;
//            
//
//            var camPos = Camera.main.gameObject.transform.position;
//            var camDir = Camera.main.gameObject.transform.forward;
//            
//            EntityQuery queryPoints = GetEntityQuery(
//                Unity.Entities.ComponentType.ReadWrite<DensityCube>(),
//                ComponentType.ReadOnly<ChunkIndex>(),
//                ComponentType.ReadOnly<Translation>());
//            
//            float buildInput = _input.PlayerMovement.Build.ReadValue<float>() - _input.PlayerMovement.Dig.ReadValue<float>();
//
//            Entities.ForEach((ref Translation translation, ref Input input) =>
//            {
//                //Debug.Log("Found an input entity");
//                
//                RaycastInput raycastInput = new RaycastInput
//                {
//                    Start = (float3) camPos,
//                    End = (float3) camDir * 100,
//                    Filter = _chunkFilter
//                };
//                
//                
//                RaycastHit hitChunk;
//                if (collisionWorld.CastRay(raycastInput, out hitChunk))
//                {
//                    Debug.DrawLine(camPos, hitChunk.Position, new Color(1f, 0.27f, 0.89f));
//                    PointDistanceInput pointDistanceInput = new PointDistanceInput
//                    {
//                        Position = hitChunk.Position,
//                        MaxDistance = _buildRadius,
//                        Filter = _chunkFilter
//                    };
//                    var distanceChunkHits = new NativeList<DistanceHit>(Allocator.Temp);
//                    if (collisionWorld.CalculateDistance(pointDistanceInput, ref distanceChunkHits))
//                    {
//                        //Debug.Log($"Collided with {distanceChunkHits.Length} chunks!");
//                        for (int i = 0; i < distanceChunkHits.Length; i++)
//                        {
//                            
//                            Debug.DrawLine(distanceChunkHits[i].Position,
//                                distanceChunkHits[i].Position + distanceChunkHits[i].SurfaceNormal,
//                                new Color(1f, 0.55f, 0.25f));
//                            
//                            bool chunkIsDirty = false;
//                            
//                            var rbIndex = distanceChunkHits[i].RigidBodyIndex;
//                            Entity hitChunkEntity = buildPhysicsWorld.PhysicsWorld.Bodies[rbIndex].Entity;
//                            
//                            if (!ecs.HasComponent<ChunkIndex>(hitChunkEntity)) //didnt hit chunk index
//                                return;
//                            
//                            var chunkIndexOfHit = ecs.GetSharedComponentData<ChunkIndex>(hitChunkEntity);
//                            
//                            queryPoints.SetSharedComponentFilter(chunkIndexOfHit);
//                            //Debug.Log($"chunk index {chunkIndexOfHit}");
//                            
//                            var densityCubes = queryPoints.ToComponentDataArray<DensityCube>(Allocator.TempJob);
//                            var pointPositions = queryPoints.ToComponentDataArray<Translation>(Allocator.TempJob);
//                            var pointEntities = queryPoints.ToEntityArray(Allocator.TempJob);
//                            
//                            
//                            for (int j = 0; j < pointEntities.Length; j++)
//                            {
//                                var distToCubeCenter = math.distance(hitChunk.Position, pointPositions[j].Value); //
//                                if (distToCubeCenter > _buildRadius + chunkIndexOfHit.DensityCubeWidth)
//                                    continue;
//                                
//                                if (Math.Abs(buildInput) > 0.08)
//                                    chunkIsDirty = true;
//                                    
//                                NativeList<float> densities = new NativeList<float>(8, Allocator.Temp);
//                                densityCubes[j].ForEach(pointPositions[j], chunkIndexOfHit, (density, pos) =>
//                                {
//                                    var distToDensity = math.distance(hitChunk.Position, pos);
//                                    float amountToDig = math.lerp(_maxBuildRate, _minBuildRate, distToDensity / _buildRadius);
//               
//                                    float densityDelta = buildInput * amountToDig;
//                                    float densityToChange = densityDelta + density;
//                                    if (buildInput < 0 && densityToChange > 0)
//                                        densityToChange = 0;
//                                    
//                                    if (buildInput > 0 && densityToChange < 0)
//                                        densityToChange = 0;
//                                    densities.Add(densityToChange);
//                                    //Debug.Log($"Density to Increase: {densityDelta}");
//                                });
//                                    
//                                ecs.SetComponentData(pointEntities[j], new DensityCube(densities));
//                                densities.Dispose();
//                                
//                                var pPos = ecs.GetComponentData<Translation>(pointEntities[j]).Value;
//                                Debug.DrawLine(pPos, pPos + new float3(.07f,.07f,.07f), new Color(0.67f, 1f, 0.73f,0.5f));
//                            }
//                            
//                            //if dirty and doesnt already have chunk
//                            if (chunkIsDirty && !ecs.HasComponent<Tag_DirtyChunk>(hitChunkEntity))
//                                ecs.AddComponent<Tag_DirtyChunk>(hitChunkEntity);
//
//
//                            pointEntities.Dispose();
//                            densityCubes.Dispose();
//                            pointPositions.Dispose();
//
//                        }
//                    }
//
//                    //marching cubes algo
//                    
//                    distanceChunkHits.Dispose();
//                }
//            });
//        }
//        
//        
//    }
//}