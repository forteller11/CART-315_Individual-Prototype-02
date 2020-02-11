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
    public class ChangePointDensity : JobComponentSystem
    {
        private float _buildRadius = 4f;

        private NativeArray<float3> _hitTrackers;
//        private Unity.Physics.Systems.BuildPhysicsWorld _physicsWorldSystem;
//        private CollisionWorld _collisionWorld;
        
//        protected override void OnStartRunning()
//        {
//            var _buildPhysicsWorld = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
//            var _collisionWorld = _buildPhysicsWorld.PhysicsWorld.CollisionWorld;
//            
//        }

       


        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
            var buildPhysicsWorld = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
            var collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;

            var jh = Entities.WithAll<Input>().ForEach((ref Translation translation, ref Input input) =>
            {
                
                RaycastInput raycastInput = new RaycastInput
                {
                    Start = (float3) UnityEngine.Camera.main.gameObject.transform.position,
                    End = (float3) (UnityEngine.Camera.main.gameObject.transform.forward) * 100,
                    Filter = CollisionFilter.Default
                };
                RaycastHit hitChunk;
                if (collisionWorld.CastRay(raycastInput, out hitChunk))
                {
                    PointDistanceInput pointDistanceInput = new PointDistanceInput
                    {
                        Position = hitChunk.Position,
                        MaxDistance = _buildRadius,
                        Filter = CollisionFilter.Default
                    };
                    var distanceChunkHits = new NativeList<DistanceHit>(Allocator.Temp);
                    if (collisionWorld.CalculateDistance(pointDistanceInput, ref distanceChunkHits))
                    {
                        _hitTrackers.Dispose();
                        _hitTrackers = new NativeArray<float3>(distanceChunkHits.Length, Allocator.Persistent);
                        for (int i = 0; i < distanceChunkHits.Length; i++)
                        {
                            _hitTrackers[i] = distanceChunkHits[i].Position;
                            //use entity command buffer to draw debug points
                        }
                  
                    }
          
                    
                    //find point of contact
             
                    
                    //create sphere collider at point and get all collided chunks
                    
                    //get all collided points and increase their value
                    
                    //marching cubes algo
                    distanceChunkHits.Dispose();
                }
            }).Schedule(inputDeps);

            
            return jh;


        }
        }
    }