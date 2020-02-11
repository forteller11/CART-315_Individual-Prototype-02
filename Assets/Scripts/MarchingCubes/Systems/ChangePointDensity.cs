using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using RaycastHit = Unity.Physics.RaycastHit;

namespace MarchingCubes.Systems
{
    public class ChangePointDensity : JobComponentSystem
    {
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

            var jh = Entities.WithAll<Input>().ForEach((ref Translation translation) =>
            {
                RaycastHit hitChunk;
                float3 start = (float3) UnityEngine.Camera.main.gameObject.transform.position;
                float3 offset = (float3) (UnityEngine.Camera.main.gameObject.transform.forward) * 100;
                CollisionFilter filter = CollisionFilter.Default;

                RaycastInput raycastInput = new RaycastInput
                {
                    Start = start,
                    End = offset,
                    Filter = filter
                };

                if (collisionWorld.CastRay(raycastInput, out hitChunk))
                {
                    //find point of contact
                    
                    //create sphere collider at point and get all collided chunks
                    
                    //get all collided points and increase their value
                    
                    //marching cubes algo
                    hitChunk.
                }
            }).Schedule(inputDeps);


            return jh;


        }
        }
    }