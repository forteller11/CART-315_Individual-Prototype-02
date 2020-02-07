using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

[GenerateAuthoringComponent]
public class RandomMovement : ComponentSystem
{
    public Mesh Mesh;
    public Material Material;
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
//        Entities.ForEach(ref RenderMesh Translation) =>
//        {
//            renderMesh.material = new Material();
//            renderMesh.mesh = Mesh;
//        };
    }
}