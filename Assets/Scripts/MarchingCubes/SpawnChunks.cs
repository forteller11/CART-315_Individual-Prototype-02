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
        public static int POINTS_IN_ROW = 3;
        public static int POINTS_IN_CHUNKS = POINTS_IN_ROW*POINTS_IN_ROW*POINTS_IN_ROW;
        public static float CHUNK_SIZE = 1;
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
                typeof(MarchingChunk)
                );
            var pointArchetype = ecsManager.CreateArchetype(
                typeof(MarchingPoint)
            );
            
            int chunkCount = ChunksToSpawn.x * ChunksToSpawn.y * ChunksToSpawn.z;
            var chunkEntities = ecsManager.CreateEntity(chunkArchetype, chunkCount, Allocator.Temp);
            
            Debug.Log(POINTS_IN_CHUNKS);
            Debug.Log(chunkCount);
            Debug.Log(POINTS_IN_CHUNKS * chunkCount);
            chunkEntities.IndexAsIf3D(ChunksToSpawn, (chunk, chunkX, chunkY, chunkZ, index) =>
            {
                ecsManager.SetName(chunk, $"Chunk [{chunkX}, {chunkY}, {chunkZ}]");
                
                ecsManager.SetSharedComponentData(chunk, new RenderMesh
                {
                    material = Material,
                    mesh = Mesh
                });

                ecsManager.SetComponentData(chunk, new Translation
                {
                    Value = new float3(chunkX,chunkY,chunkZ)
                });
            });
            
            for (int i = 0; i < chunkCount; i++)
            {
                var pointEntities = ecsManager.CreateEntity(pointArchetype, POINTS_IN_CHUNKS, Allocator.Temp);
                pointEntities.IndexAsIf3D(new int3(POINTS_IN_ROW,POINTS_IN_ROW,POINTS_IN_ROW), (point, jp, kp, wp, indexP) =>
                {
                    ecsManager.SetName(point, $"Point [{jp},{kp},{wp}], Chunk [{iP}");
                    ecsManager.SetComponentData(point, new MarchingPoint
                    {
                        ChunkPos = new float3(jp,kp,wp),
                        Density = 0.5f
                    });
                });
                
                ecsManager.SetComponentData(chunkEntities[i], new MarchingChunk (
                    pointEntities
                ));
                pointEntities.Dispose();
            }
            
            
            chunkEntities.Dispose();
        }
    }
}