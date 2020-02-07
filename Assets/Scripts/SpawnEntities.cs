using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    public int NumberOfEntities = 100;
    [SerializeField] public Mesh Mesh;
    [SerializeField] public Material Material;
    void Start()
    {
        var ecsManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var archetype = ecsManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(LocalToWorld)
        );
        //ecsManager.SetE

        NativeArray<Entity> entities = ecsManager.CreateEntity(archetype, NumberOfEntities, Allocator.Temp);

        foreach (var entity in entities)
        {
            ecsManager.SetComponentData(entity, 
                new Translation {Value = new float3(0,0,0.5f)}
                );
            
            ecsManager.SetSharedComponentData(entity, 
                new RenderMesh {mesh = Mesh, material = Material}
            );
        }
        Debug.Log(entities);
        Debug.Log(entities[0]);

        entities.Dispose();
    }
    
}