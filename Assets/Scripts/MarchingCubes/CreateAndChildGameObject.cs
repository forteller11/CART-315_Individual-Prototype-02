using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CreateAndChildGameObject : MonoBehaviour
{
    public GameObject ParentToConvertToEntity;
    private Entity _parent;
    private BlobAssetStore _blobAssetStore;
    public float3 PositionOffset = float3.zero;
    public quaternion RotationOffset = quaternion.identity;
    void Awake()
    {
        var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
        _blobAssetStore = new BlobAssetStore();
        var conversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
        _parent = GameObjectConversionUtility.ConvertGameObjectHierarchy(ParentToConvertToEntity, conversionSettings);
        
        if (!ecs.HasComponent<Translation>(_parent) && !ecs.HasComponent<Rotation>(_parent))
            Debug.LogError("Parent Entity doesn't have translation or rotation components!");
        
        ecs.SetComponentData(_parent, new Translation { Value = transform.position });
        ecs.SetComponentData(_parent, new Rotation    { Value = transform.rotation });
        
    }
    
    void Update()
    {
        transform.position = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<Translation>(_parent).Value + PositionOffset;
        transform.rotation = math.mul( World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<Rotation>(_parent).Value, RotationOffset);
    }

    private void OnDestroy()
    {
        _blobAssetStore.Dispose();
    }
}
