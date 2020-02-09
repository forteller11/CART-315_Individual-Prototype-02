using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace MarchingCubes.Systems
{

    public class ChunkDebugger : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entities.WithAll<Translation, RenderMesh, ChunkIndex>().ForEach((Entity entity) =>
            {
                DebugDrawChunk(ecs.GetComponentData<Translation>(entity));
                //Debug.Log($"Chunk Debugger || Entity Index {entity.Index}");
            });
            
            Entities.WithAll<MarchingPoint, Translation>().ForEach((Entity entity) =>
            {
                DebugDrawPoint(ecs.GetComponentData<Translation>(entity), ecs.GetComponentData<MarchingPoint>(entity));
                //Debug.Log($"Chunk Debugger || Entity Index {entity.Index}");
            });
            
        }

        static void DebugDrawChunk(Translation pos)
        {
            float3 p = pos.Value;
            var w = (SpawnChunks.CHUNK_SIZE/2)*.98f;
            
            //points on chunk cube
            var bl1 = new float3(-w, -w, w);
            var br1 = new float3(w, -w, w);
            var tr1 = new float3(w, w, w);
            var tl1 = new float3(-w, w, w);
                
            var bl2 = new float3(-w, -w, -w);
            var br2 = new float3(w, -w, -w);
            var tr2 = new float3(w, w, -w);
            var tl2 = new float3(-w, w, -w);
                
            var leftCol = new Color(.2f,.4f,1);
            var rightCol = new Color(1,.3f,.2f);
            var vertCol = new Color(1,1,0);
                
            //sqr 1
            Debug.DrawLine(bl1 + p,br1 + p, vertCol);
            Debug.DrawLine(br1 + p,tr1 + p, rightCol);
            Debug.DrawLine(tr1 + p,tl1 + p, vertCol);
            Debug.DrawLine(tl1 + p,bl1 + p, leftCol);
                
            //sqr2
            Debug.DrawLine(bl2 + p,br2 + p, vertCol);
            Debug.DrawLine(br2 + p,tr2 + p, rightCol);
            Debug.DrawLine(tr2 + p,tl2 + p, vertCol);
            Debug.DrawLine(tl2 + p,bl2 + p, leftCol);
                
            //cnnect the sqrs
            Debug.DrawLine(bl1 + p,bl2 + p, leftCol);
            Debug.DrawLine(br1 + p,br2 + p, rightCol);
            Debug.DrawLine(tr1 + p,tr2 + p, rightCol);
            Debug.DrawLine(tl1 + p,tl2 + p, leftCol);
        }

        static void DebugDrawPoint(Translation pos, MarchingPoint point)
        {
            var value = point.Density;
            var p = pos.Value;
            var col = new Color(1, 1, 1-value,1f);
            
            Debug.DrawLine(p,p + new float3(0.1f), col);
        }
        
    }
}
