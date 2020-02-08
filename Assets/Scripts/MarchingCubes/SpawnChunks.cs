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
        public static int POINTS_IN_CHUNKS = 8;
        public static float CHUNK_SIZE = 4;
        public int3 ChunksToSpawn = new int3(4,3,4);
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
            int chunkCount = ChunksToSpawn.x * ChunksToSpawn.y * ChunksToSpawn.z;
            var chunkEntities = ecsManager.CreateEntity(chunkArchetype, chunkCount, Allocator.Temp);
            
            for (int i = 0; i < ChunksToSpawn.x; i++)
            {
                for (int j = 0; j < ChunksToSpawn.y; j++)
                {
                    for (int k = 0; k < ChunksToSpawn.z; k++)
                    {
                        int ii = i * ChunksToSpawn.y * ChunksToSpawn.z;
                        int jj = j * ChunksToSpawn.z;
                        int kk = k;
                        int index = ii + jj + kk;

                        ecsManager.SetSharedComponentData(chunkEntities[index], new RenderMesh
                        {
                            material = Material,
                            mesh = Mesh
                        });

                        ecsManager.SetComponentData(chunkEntities[index], new Translation
                        {
                            Value = new float3(i,j,k)
                        });
                        
                        ecsManager.SetName(chunkEntities[index], $"Chunk [{i},{j},{k}] --> {index}");
                    }
                }
            }

            chunkEntities.Dispose();
        }
    }
}