using System;
using Unity.Collections;
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
        public static float MaxAlpha = 1f;
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
            
            EntityQuery settingsQuery = GetEntityQuery(Unity.Entities.ComponentType.ReadOnly<ChunkSettingsSingleton>());
            var chunkSettings = settingsQuery.ToComponentDataArray<ChunkSettingsSingleton>(Allocator.TempJob);
        

                Entities.WithAll<ChunkIndex>().ForEach((Entity entity) => //chunk
                {
                    var chunkIndex = ecs.GetComponentData<ChunkIndex>(entity);
                    DebugDrawChunk(chunkIndex, chunkSettings[0]);

                    var chunkPos = (float3) chunkIndex.Value * chunkSettings[0].ChunkWidth;
                    var densitiesBuffer = ecs.GetBuffer<ChunkDensities>(entity);
                    var densities = densitiesBuffer.Reinterpret<float>();
                    
                    Utils.IndexAsIf3D(new int3 (chunkSettings[0].VoxelsInARow), (indexFlat, index3D, indexJump) =>
                    {
                        float3 posRelative = Utils.GetDensityPosModel(chunkSettings[0].WidthBetweenVoxels, index3D);
                        float3 pos = posRelative + chunkPos;
                            
                        var r = 1/((chunkIndex.Value.x % modR)+1);
                        var g = 1/((chunkIndex.Value.y % modG)+1);
                        var b = 1/((chunkIndex.Value.z % modB)+1);
                        var col = new Color(r, g, b,math.lerp(BaseAlpha,MaxAlpha,densities[indexFlat]));
                
                        float len = (0.3f * densities[indexFlat]) + BaseSize;
                
                        float3 offset = new float3(
                            _random.NextFloat(-len,len),
                            _random.NextFloat(-len,len),
                            _random.NextFloat(-len,len));
                        
                        Debug.DrawLine(pos,pos + offset, col);
                    });
            });
            
           
            chunkSettings.Dispose();
        }


        void DebugDrawChunk(ChunkIndex index, ChunkSettingsSingleton settings)
        {
            _random.state = (uint) index.Value.Volume()+1;
            
            float3 p = Utils.GetChunkPos(index.Value, settings.ChunkWidth);;
            float w = settings.ChunkWidth * 0.98f;
            
            //points on chunk cube
            var bl1 = new float3(0, 0, w);
            var br1 = new float3(w, 0, w);
            var tr1 = new float3(w, w, w);
            var tl1 = new float3(0, w, w);
                
            var bl2 = new float3(0, 0, 0);
            var br2 = new float3(w, 0, 0);
            var tr2 = new float3(w, w, 0);
            var tl2 = new float3(0, w, 0);
                
            var r = 1/((index.Value.x % modR)+1);
            var g = 1/((index.Value.y % modG)+1);
            var b = 1/((index.Value.z % modB)+1);
            
            var col = new Color(r, g, b,MaxAlpha);
            
                
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
