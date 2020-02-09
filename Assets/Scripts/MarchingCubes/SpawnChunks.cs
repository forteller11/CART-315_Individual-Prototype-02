using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace MarchingCubes
{
    public class SpawnChunks : MonoBehaviour
    {
        public int PointsInRow = 3;
        public static float CHUNK_SIZE = 4;
        public int3 ChunksToSpawn = new int3(2,2,2);
        
        public Mesh Mesh;
        public Material Material;
        void Start()
        {
            
            var ecsManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var chunkArchetype = ecsManager.CreateArchetype(
                typeof(Translation),
                typeof(Rotation),
                typeof(RenderMesh),
                typeof(LocalToWorld),
                typeof(ChunkIndex)
            );
            var pointArchetype = ecsManager.CreateArchetype(
                typeof(MarchingPoint),
                typeof(Translation),
                typeof(ChunkIndex)
            );
            
            var chunkEntities = ecsManager.CreateEntity(chunkArchetype, ChunksToSpawn.Volume(), Allocator.Temp);
            NativeArray<ChunkSpawnData> chunkSpawnDatas = new NativeArray<ChunkSpawnData>(ChunksToSpawn.Volume(), Allocator.Temp);
            
            var halfOfAChunk = (new float3(1,1,1)*CHUNK_SIZE)/2f;
            float toIncrement = (float) CHUNK_SIZE / (PointsInRow-1);

            chunkEntities.IndexAsIf3D(ChunksToSpawn, (chunk, indexSpatial, indexArr) =>
            {
                ecsManager.SetName(chunk, $"Chunk [{indexSpatial.x}, {indexSpatial.y}, {indexSpatial.z}]");
                
                var chunkPos = new float3(indexSpatial.x, indexSpatial.y, indexSpatial.z) * CHUNK_SIZE;
                var chunkIndex = GetChunkIndex(chunkPos, CHUNK_SIZE);
                
                chunkSpawnDatas[indexArr] = new ChunkSpawnData(chunkPos, chunkIndex);
                
                ecsManager.SetSharedComponentData(chunk, new RenderMesh
                {
                    material = Material,
                    mesh = Mesh
                });

                
                ecsManager.SetComponentData(chunk, new Translation
                {
                    Value = chunkPos
                });
                
                ecsManager.SetSharedComponentData(chunk, new ChunkIndex
                {
                    Index = chunkIndex
                });


            });

            int4 pointsToSpawn = new int4(ChunksToSpawn.Volume(),PointsInRow ,PointsInRow, PointsInRow);
            var pointEntities = ecsManager.CreateEntity(pointArchetype, pointsToSpawn.Volume(), Allocator.Temp);
            
            pointEntities.IndexAsIf4D(pointsToSpawn,(point, indexSpatial, indexArr) =>
            {
                ecsManager.SetName(point, $"Point [{indexSpatial.y}, {indexSpatial.z}, {indexSpatial.w}] Chunk {indexSpatial.x}");
                var posWithinChunk = (new float3(indexSpatial.y, indexSpatial.z, indexSpatial.w) * toIncrement) - halfOfAChunk;
                var worldPos = posWithinChunk + chunkSpawnDatas[indexSpatial.x].Position;
                
                ecsManager.SetComponentData(point, new Translation
                {
                    Value = worldPos
                });
                    
                ecsManager.SetComponentData(point, new MarchingPoint
                {
                    Density = 0.5f
                });

                ecsManager.SetSharedComponentData(point, new ChunkIndex
                {
                    Index = chunkSpawnDatas[indexSpatial.x].Index
                });
                    
            });

            chunkSpawnDatas.Dispose();
            chunkEntities.Dispose();
            pointEntities.Dispose();
        }
        
        public int3 GetChunkIndex(float3 position, float chunkSize){
            return  (int3) (position / chunkSize);
        }

        private struct ChunkSpawnData
        {
            public float3 Position;
            public int3 Index;

            public ChunkSpawnData(float3 position, int3 index)
            {
                Position = position;
                Index = index;
            }
        }
    }
    
    
}