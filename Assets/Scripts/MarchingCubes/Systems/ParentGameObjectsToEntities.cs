using Unity.Entities;
using Unity.Transforms;

namespace MarchingCubes.Systems
{
    public class ParentGameObjectsToEntities
    {
        public class PlayerMovement : ComponentSystem
        {

            protected override void OnUpdate()
            {
                Entities.ForEach((ref MakeGameObjectChild makeChild, in Translation translation, in Rotation rotation) =>
                {
                    makeChild.Transform.position = translation.Value;
                    makeChild.Transform.rotation = rotation.Value;
                });
            }
        }
    }
}