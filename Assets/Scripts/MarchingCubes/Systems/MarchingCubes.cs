using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace MarchingCubes.Systems
{
    public class MarchingCubes : ComponentSystem
    {
        public static float MarchingCubesThreshold = 0.5f;
        protected override void OnUpdate()
        {
       
            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
            EntityQuery queryDirtyChunks = GetEntityQuery(
                Unity.Entities.ComponentType.ReadOnly<RenderMesh>(),
                ComponentType.ReadOnly<Tag_DirtyChunk>(),
                ComponentType.ReadOnly<Translation>(),
                ComponentType.ReadOnly<ChunkData>()
                );
            
            EntityQuery queryPoints = GetEntityQuery(
                Unity.Entities.ComponentType.ReadOnly<DensityCube>(),
                ComponentType.ReadOnly<ChunkData>(),
                ComponentType.ReadOnly<Translation>());

            var dirtyChunks = queryDirtyChunks.ToEntityArray(Allocator.TempJob);
            Debug.Log($"dirty chunks queried {dirtyChunks.Length}");
            for (int ei = 0; ei < dirtyChunks.Length; ei++)
            {
                ecs.RemoveComponent<Tag_DirtyChunk>(dirtyChunks[ei]);
                ChunkData currentChunkData = ecs.GetSharedComponentData<ChunkData>(dirtyChunks[ei]);
                RenderMesh currentRenderMesh = ecs.GetSharedComponentData<RenderMesh>(dirtyChunks[ei]);
                
                queryPoints.SetSharedComponentFilter(currentChunkData);

                var densities = queryPoints.ToComponentDataArray<DensityCube>(Allocator.TempJob);
                
                List<Vector3> verts = new List<Vector3>(densities.Length * 8);
                List<int> tris = new List<int>();
                
                for (int di = 0; di < densities.Length; di++)
                {
                    densities[di].ForEach(new Translation {Value=0}, currentChunkData, (density, pos) =>
                    {
                        verts.Add(pos);
                    });
                    //front face
                    tris.AddRange( new int[]{di+0, di+2, di+3}); // |_
                    tris.AddRange( new int[]{di+1, di+3, di+2}); // _|
                    
                    //backface
                    tris.AddRange( new int[]{di+7, di+6, di+4}); // _|
                    tris.AddRange( new int[]{di+6, di+7, di+5}); // |_
                    
                    tris.AddRange( new int[]{di+0, di+1, di+2});
                    tris.AddRange( new int[]{di+0, di+1, di+2});
                    
                    tris.AddRange( new int[]{di+0, di+1, di+2});
                    tris.AddRange( new int[]{di+0, di+1, di+2});
                    
                    tris.AddRange( new int[]{di+0, di+1, di+2});
                    tris.AddRange( new int[]{di+0, di+1, di+2});
                    
                    tris.AddRange( new int[]{di+0, di+1, di+2});
                    tris.AddRange( new int[]{di+0, di+1, di+2});
                }
                
                currentRenderMesh.mesh.SetVertices(verts);
                currentRenderMesh.mesh.SetIndices(tris, MeshTopology.Triangles, 0); //ei instead of 0
                currentRenderMesh.mesh.RecalculateBounds();
                //ecs.SetSharedComponentData(dirtyChunks[ei], currentRenderMesh);
                Debug.Log("Draw mesh!");
                //Graphics.DrawMesh(currentRenderMesh.mesh, Matrix4x4.identity, currentRenderMesh.material, 0);

                densities.Dispose();
            }

            dirtyChunks.Dispose();
        }
    }
}