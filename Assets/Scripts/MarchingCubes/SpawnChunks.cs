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
            
            chunkEntities.IndexAsIf3D(ChunksToSpawn, (chunk, index) =>
            {
                ecsManager.SetName(chunk, $"Chunk [{index.x}, {index.y}, {index.z}]");
                
                ecsManager.SetSharedComponentData(chunk, new RenderMesh
                {
                    material = Material,
                    mesh = Mesh
                });

                var position = new float3(index.x, index.y, index.z) * CHUNK_SIZE;
                ecsManager.SetComponentData(chunk, new Translation
                {
                    Value = position
                });
                
                ecsManager.SetSharedComponentData(chunk, new ChunkIndex
                {
                    Index = GetChunkIndex(position, CHUNK_SIZE)
                });
            });
            
                int3 pointsToSpawn = ChunksToSpawn * (PointsInRow);
                var halfOfAChunk = (new float3(1,1,1)*CHUNK_SIZE)/2f;
                float toIncrement = (float) CHUNK_SIZE / (PointsInRow-1);
                var pointEntities = ecsManager.CreateEntity(pointArchetype, pointsToSpawn.Volume(), Allocator.Temp);

                pointEntities.IndexAsIf3D(new int3(pointsToSpawn), (point, index) =>
                {

                    ecsManager.SetName(point, $"Point [{index.x}, {index.y}, {index.z}]");
                    
                    var position = (new float3(index.x, index.y, index.z) * toIncrement) - halfOfAChunk;

                    ecsManager.SetComponentData(point, new Translation
                    {
                        Value = position
                    });
                    
                    ecsManager.SetComponentData(point, new MarchingPoint
                    {
                        Density = 0.5f
                    });
                    
                    ecsManager.SetSharedComponentData(point, new ChunkIndex
                    {
                        Index = GetChunkIndex(position, CHUNK_SIZE)
                    });
                    
                });
                
                
                chunkEntities.Dispose();
                pointEntities.Dispose();
        }
        
        public int3 GetChunkIndex(float3 position, float chunkSize){
            return  (int3) (position / chunkSize);
        }
    }
    
    
}