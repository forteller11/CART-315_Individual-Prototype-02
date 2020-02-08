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
            var pointEntities = ecsManager.CreateEntity(pointArchetype, POINTS_IN_CHUNKS*chunkCount, Allocator.Temp);
            Debug.Log(POINTS_IN_CHUNKS);
            Debug.Log(chunkCount);
            Debug.Log(POINTS_IN_CHUNKS * chunkCount);
            chunkEntities.IndexAsIf3D(ChunksToSpawn, (chunk, i, j, k, index) =>
            {
                ecsManager.SetName(chunk, $"Chunk [{i},{j},{k}]");
                
                ecsManager.SetSharedComponentData(chunk, new RenderMesh
                {
                    material = Material,
                    mesh = Mesh
                });

                ecsManager.SetComponentData(chunk, new Translation
                {
                    Value = new float3(i,j,k)
                });
                    
                pointEntities.IndexAsIf4D(new int4(chunkCount,POINTS_IN_ROW,POINTS_IN_ROW,POINTS_IN_ROW), (point, iP, jp, kp, wp, indexP) =>
                {
                    ecsManager.SetName(point, $"Point [{jp},{kp},{wp}], Chunk [{iP}");
                    ecsManager.SetComponentData(point, new MarchingPoint
                    {
                        ChunkPos = new float3(jp,kp,wp),
                        Density = 0.5f
                    });
                });
                
            });
            
            pointEntities.Dispose();
            chunkEntities.Dispose();
        }
    }
}