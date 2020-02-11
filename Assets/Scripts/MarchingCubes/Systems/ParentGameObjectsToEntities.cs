using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

namespace MarchingCubes.Systems
{
//        public class PlayerMovement : JobComponentSystem
//        {
//            
//            protected override JobHandle OnUpdate(JobHandle inputDeps)
//            {
//
//                var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
//                var jh = Entities.ForEach((in Entity entity, in Translation translation, in Rotation rotation) =>
//                {
//                    var makeChild = ecs.GetSharedComponentData<MakeGameObjectChild>(entity);
//                    makeChild.Value.transform.position = translation.Value;
//                    makeChild.Value.transform.rotation = rotation.Value;
//
//                }).Schedule(inputDeps);
//
//                return jh;
//            }
//
//
//
//        }

        public class PlayerMovement : ComponentSystem
        {
            protected override void OnUpdate()
            {
                var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
                Entities.ForEach((entity) =>
                {
                    if (ecs.HasComponent<Translation>(entity) && ecs.HasComponent<Rotation>(entity) && ecs.HasComponent<MakeGameObjectChild>(entity))
                    {
                        var makeChild = ecs.GetSharedComponentData<MakeGameObjectChild>(entity);
                        var translation = ecs.GetComponentData<Translation>(entity);
                        var rotation = ecs.GetComponentData<Rotation>(entity);
                        
                        makeChild.Value.transform.position = translation.Value;
                        makeChild.Value.transform.rotation = rotation.Value;
                    }

                });
            }
        }
}