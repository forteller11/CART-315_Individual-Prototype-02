using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

[GenerateAuthoringComponent]
[AlwaysSynchronizeSystem]
public class RandomMovement : ComponentSystem
{
    
    protected override void OnStartRunning()
    {
//        Entities.ForEach(out RenderMesh renderMesh) =>
//        {
//            renderMesh.material = new Material();
//            renderMesh.mesh = Mesh;
//        };
    }
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation pos) =>
        {
            float3 line = new float3(0.5f, 0.5f, 0.5f);
            Debug.DrawLine(pos.Value, line);
        });
    }
    
//    protected override JobHandle OnUpdate(JobHandle data)
//    {
//
//        var jh = Entities.ForEach((ref Translation pos) =>
//        {
//            float3 line = new float3(0.5f,0.5f,0.5f);
//            Debug.DrawLine(pos.Value, line);
//        }).Schedule(data);
//
//        return jh;
//    }

}