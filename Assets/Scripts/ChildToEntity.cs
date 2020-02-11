using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class ChildToEntity : MonoBehaviour
{
    public Entity ParentEntity;
    
    void Update()
    {
        transform.position = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<Translation>(ParentEntity).Value;
        transform.rotation = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<Rotation>(ParentEntity).Value;
    }
}
