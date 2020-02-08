using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    public int NumberOfEntities = 100;
    public float RanSpeed = 0.1f;
    public float RanScale = 0.8f;
    public float SlowDownBy = .99f;
    [SerializeField] public Mesh Mesh;
    [SerializeField] public Material Material;
    
    void Start()
    {
        var ecsManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var archetype = ecsManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(MoveBy),
            typeof(Scale)
        );
        //ecsManager.SetE

        NativeArray<Entity> entities = ecsManager.CreateEntity(archetype, NumberOfEntities, Allocator.Temp);
        
        var ran = new Unity.Mathematics.Random(2);
   
        foreach (var entity in entities)
        {
            ecsManager.SetComponentData(entity, 
                new Translation {Value = new float3(0,0,0.5f)}
                );
            
            ecsManager.SetComponentData(entity, 
                new Scale {Value = ran.NextFloat(0, ran.NextFloat(0, RanScale))}
            );

            
            ecsManager.SetComponentData(entity, 
                new MoveBy
                {
                    Speed = new float3(ran.NextFloat(-RanSpeed,RanSpeed),ran.NextFloat(-RanSpeed,RanSpeed),ran.NextFloat(-RanSpeed,RanSpeed)),
                    SlowDownBy = SlowDownBy
                }
            );
            
            ecsManager.SetSharedComponentData(entity, 
                new RenderMesh {mesh = Mesh, material = Material}
            );
        }

        entities.Dispose();
    }
    
}