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
//    public class ChangePointDensity : JobComponentSystem
//    {
//        protected override JobHandle OnUpdate(JobHandle inputDeps)
//        {
//            throw new System.NotImplementedException();
//        }
////        protected override JobHandle OnUpdate(JobHandle inputDeps)
////        {
////            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
////            var buildPhysicsWorld = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
////            var collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;
////
////            var camPos = Camera.main.gameObject.transform.position;
////            var camDir = Camera.main.gameObject.transform.forward;
////            var jh = Entities.ForEach((ref Translation translation, ref Input input) =>
////            {
////                Debug.Log("Found an input entity");
////                
////                RaycastInput raycastInput = new RaycastInput
////                {
////                    Start = (float3) camPos,
////                    End = (float3) camDir * 100,
////                    Filter = CollisionFilter.Default
////                };
//////                
////                RaycastHit hitChunk;
////                if (collisionWorld.CastRay(raycastInput, out hitChunk))
////                {
//                   
////                    PointDistanceInput pointDistanceInput = new PointDistanceInput
////                    {
////                        Position = hitChunk.Position,
////                        MaxDistance = 4f,
////                        Filter = CollisionFilter.Default
////                    };
////                    var distanceChunkHits = new NativeList<DistanceHit>(Allocator.Temp);
////                    if (collisionWorld.CalculateDistance(pointDistanceInput, ref distanceChunkHits))
////                    {
////                        
////                        Debug.Log($"Collided with {distanceChunkHits.Length} chunks!");
////                        for (int i = 0; i < distanceChunkHits.Length; i++)
////                        {
////                            
////                            //get all points in chunk
////                        }
////                  
////                    }
////                    
////                    //find point of contact
////             
////                    
////                    //create sphere collider at point and get all collided chunks
////                    
////                    //get all collided points and increase their value
////                    
////                    //marching cubes algo
////                    distanceChunkHits.Dispose();
////                }
////            }).Schedule(inputDeps);
//
//            
////            return jh;
//
//
////        }
//        }
    }