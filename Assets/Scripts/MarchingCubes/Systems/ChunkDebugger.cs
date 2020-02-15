using System;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace MarchingCubes.Systems
{

    public class ChunkDebugger : ComponentSystem
    {
        public static bool DebugDraw = false;
        public static float BaseAlpha = 0.1f;
        public static float BaseSize = 0.05f;
        private Random _random = new Random(10);
        private float modR = 2;
        private float modG = 3;
        private float modB = 4;
        protected override void OnUpdate()
        {
            if (DebugDraw == false)
                return;

            var ecs = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entities.WithAll<ChunkIndex>().WithNone<DensityCube>().ForEach((Entity entity) =>
            {
                DebugDrawChunk(ecs.GetComponentData<Translation>(entity), ecs.GetSharedComponentData<ChunkIndex>(entity));
                //Debug.Log($"Chunk Debugger || Entity Index {entity.Index}");
            });
            
            Entities.WithAll<DensityCube, Translation>().ForEach((Entity entity) =>
            {
                var chunkIndex = ecs.GetSharedComponentData<ChunkIndex>(entity);
                var densityCube = ecs.GetComponentData<DensityCube>(entity);
                var translation = ecs.GetComponentData<Translation>(entity);
                
                densityCube.ForEach( translation, chunkIndex, (density, pos) =>
                {
                    
                    var r = 1/((chunkIndex.Value.x % modR)+1);
                    var g = 1/((chunkIndex.Value.y % modG)+1);
                    var b = 1/((chunkIndex.Value.z % modB)+1);
                    var col = new Color(r, g, b,density + BaseAlpha);
            
                    float len = (0.2f * density) + BaseSize;
            
                    float3 offset = new float3(
                        _random.NextFloat(-len,len),
                        _random.NextFloat(-len,len),
                        _random.NextFloat(-len,len));
                    
                    Debug.DrawLine(pos,pos + offset, col);

                });
                
                //Debug.Log($"Chunk Debugger || Entity Index {entity.Index}");
            });
            
        }


        void DebugDrawChunk(Translation pos, ChunkIndex index)
        {
            _random.state = (uint) index.Value.Volume()+1;
            
            float3 p = pos.Value;
            var w = (SpawnChunks.ChunkWidth/2)*.98f;
            
            //points on chunk cube
            var bl1 = new float3(-w, -w, w);
            var br1 = new float3(w, -w, w);
            var tr1 = new float3(w, w, w);
            var tl1 = new float3(-w, w, w);
                
            var bl2 = new float3(-w, -w, -w);
            var br2 = new float3(w, -w, -w);
            var tr2 = new float3(w, w, -w);
            var tl2 = new float3(-w, w, -w);
                
            var r = 1/((index.Value.x % modR)+1);
            var g = 1/((index.Value.y % modG)+1);
            var b = 1/((index.Value.z % modB)+1);
            
            var col = new Color(r, g, b,1);
  
            var leftCol = new Color(.2f,.4f,1);
            var rightCol = new Color(1,.3f,.2f);
            var vertCol = new Color(1,1,0);
                
            //sqr 1
            Debug.DrawLine(bl1 + p,br1 + p, col);
            Debug.DrawLine(br1 + p,tr1 + p, col);
            Debug.DrawLine(tr1 + p,tl1 + p, col);
            Debug.DrawLine(tl1 + p,bl1 + p, col);
                
            //sqr2
            Debug.DrawLine(bl2 + p,br2 + p, col);
            Debug.DrawLine(br2 + p,tr2 + p, col);
            Debug.DrawLine(tr2 + p,tl2 + p, col);
            Debug.DrawLine(tl2 + p,bl2 + p, col);
                
            //cnnect the sqrs
            Debug.DrawLine(bl1 + p,bl2 + p, col);
            Debug.DrawLine(br1 + p,br2 + p, col);
            Debug.DrawLine(tr1 + p,tr2 + p, col);
            Debug.DrawLine(tl1 + p,tl2 + p, col);
        }
        
        
    }
}
