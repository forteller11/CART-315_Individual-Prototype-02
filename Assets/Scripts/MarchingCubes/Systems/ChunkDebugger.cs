using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace MarchingCubes.Systems
{

    public class ChunkDebugger : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entities.WithAll<MarchingChunk, Translation>().ForEach((Entity entity) =>
            {
                DebugDrawChunk(ecs.GetComponentData<Translation>(entity));
                Debug.Log($"Chunk Debugger || Entity Index {entity.Index}");
            });
            
        }

        static void DebugDrawChunk(Translation pos)
        {
            float3 p = pos.Value;
            var w = SpawnChunks.CHUNK_SIZE/2;
            
            //points on chunk cube
            var bl1 = new float3(-w, -w, w);
            var br1 = new float3(w, -w, w);
            var tr1 = new float3(w, w, w);
            var tl1 = new float3(-w, w, w);
                
            var bl2 = new float3(-w, -w, -w);
            var br2 = new float3(w, -w, -w);
            var tr2 = new float3(w, w, -w);
            var tl2 = new float3(-w, w, -w);
                
            var sqr1Col = new Color(1,0,0);
            var sqr2Col = new Color(0,1,0);
            var sqrConnectCol = new Color(0,0,1);
                
            //sqr 1
            Debug.DrawLine(bl1 + p,br1 + p, sqr1Col);
            Debug.DrawLine(br1 + p,tr1 + p, sqr1Col);
            Debug.DrawLine(tr1 + p,tl1 + p, sqr1Col);
            Debug.DrawLine(tl1 + p,bl1 + p, sqr1Col);
                
            //sqr2
            Debug.DrawLine(bl2 + p,br2 + p, sqr2Col);
            Debug.DrawLine(br2 + p,tr2 + p, sqr2Col);
            Debug.DrawLine(tr2 + p,tl2 + p, sqr2Col);
            Debug.DrawLine(tl2 + p,bl2 + p, sqr2Col);
                
            //cnnect the sqrs
            Debug.DrawLine(bl1 + p,bl2 + p, sqrConnectCol);
            Debug.DrawLine(br1 + p,br2 + p, sqrConnectCol);
            Debug.DrawLine(tr1 + p,tr2 + p, sqrConnectCol);
            Debug.DrawLine(tl1 + p,tl2 + p, sqrConnectCol);
        }
        
    }
}
