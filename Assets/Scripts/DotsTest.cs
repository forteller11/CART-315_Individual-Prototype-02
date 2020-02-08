using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
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
        var t = Time.DeltaTime;
        JobHandle jh = Entities.ForEach((
            ref Translation pos, in MoveBy moveBy) =>
        {
            pos.Value += moveBy.Speed * t;
        }).Schedule(data);
        jh.Complete();
        
        JobHandle jh2 = Entities.ForEach((ref MoveBy moveBy) =>
        {
            moveBy.Speed *= moveBy.SlowDownBy ;
        }).Schedule(data);
        return jh2;
    }
    
    void MoveBy(ref Translation pos, in MoveBy moveBy){
        pos.Value += moveBy.Speed;
    }
    
}


[GenerateAuthoringComponent]
public struct MoveBy : IComponentData
{
    public float3 Speed;
    public float SlowDownBy;
}

