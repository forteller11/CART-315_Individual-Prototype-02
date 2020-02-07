using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor;
using UnityEngine;

public class DotsTest : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle data)
    {
        float t = Time.DeltaTime;
        
        JobHandle jh = Entities.ForEach((
            ref Translation pos, in MoveBy moveBy) =>
        {
            pos.Value += moveBy.Speed;
        }).Schedule(data);

        return jh;
    }
}
[GenerateAuthoringComponent]
public struct MoveBy : IComponentData
{
    public float3 Speed; 
}
