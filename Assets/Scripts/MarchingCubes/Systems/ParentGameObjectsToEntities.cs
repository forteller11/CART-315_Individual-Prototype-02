using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

namespace MarchingCubes.Systems
{
    public class ParentGameObjectsToEntities
    {
        public class PlayerMovement : JobComponentSystem
        {
            
            protected override JobHandle OnUpdate(JobHandle inputDeps)
            {
                var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
                return Entities.ForEach(
                    (in Entity entity, in Translation translation, in Rotation rotation) =>
                    {
                        var makeChild = ecs.GetSharedComponentData<MakeGameObjectChild>(entity);
                        makeChild.Transform.position = translation.Value;
                        makeChild.Transform.rotation = rotation.Value;
                    }).Schedule(inputDeps);
            }
        }
    }
}