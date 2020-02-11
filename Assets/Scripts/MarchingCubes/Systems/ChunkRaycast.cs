using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics;
using UnityEngine;
using RaycastHit = Unity.Physics.RaycastHit;

namespace MarchingCubes.Systems
{
    public class ChunkRaycast : ComponentSystem
    {
//        private Unity.Physics.Systems.BuildPhysicsWorld _physicsWorldSystem;
//        private CollisionWorld _collisionWorld;
        
//        protected override void OnStartRunning()
//        {
//            var _buildPhysicsWorld = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
//            var _collisionWorld = _buildPhysicsWorld.PhysicsWorld.CollisionWorld;
//            
//        }

        protected override void OnUpdate()
        {
//            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
//            var buildPhysicsWorld = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
//            var collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;
//            
//            float3 start = (float3) UnityEngine.Camera.main.gameObject.transform.position;
//            float3 offset =   (float3)(UnityEngine.Camera.main.gameObject.transform.forward)*100;
//            CollisionFilter filter = CollisionFilter.Default;
//
//            RaycastInput raycast = new RaycastInput
//            {
//                Start = start,
//                End = offset,
//                Filter = filter
//            };
//
//            var color = Color.green;
//            NativeList<RaycastHit> hits = new NativeList<RaycastHit>(Allocator.Temp);
//            if (collisionWorld.CastRay(raycast, ref hits))
//            {
//                color += new Color(0,0,1);
//                //Debug.Log("HIT");
//                foreach (var hit in hits)
//                {
//                    color += (Color.red - Color.green)/4;
//                    Entity e =  buildPhysicsWorld.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
//                    //Debug.Log(ecs.GetName(e));
//                }
//
//            }
//
//            hits.Dispose();
//            Debug.DrawLine(start,start+offset, color);
           
        }


    }
}