using Unity.Entities;
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
            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
            var buildPhysicsWorld = World.Active.GetExistingSystem<Unity.Physics.Systems.BuildPhysicsWorld>();
            var collisionWorld = buildPhysicsWorld.PhysicsWorld.CollisionWorld;
            
            var start = UnityEngine.Camera.main.gameObject.transform.position;
            var end = start + UnityEngine.Camera.main.gameObject.transform.forward * 10f;
            var filter = CollisionFilter.Default;

            RaycastInput raycast = new RaycastInput
            {
                Start = start,
                End = end,
                Filter = filter
            };

            var color = Color.green;
            RaycastHit hit = new RaycastHit();
            if (collisionWorld.CastRay(raycast, out hit))
            {
                Entity hitEntity =  buildPhysicsWorld.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
                Debug.Log(ecs.GetName(hitEntity));
                color = Color.red;
                //hit.RigidBodyIndex
                // Entity entity = _buildPhysicsWorld.

            }
            
            Debug.DrawRay(start, end, color);
        }
    }
}