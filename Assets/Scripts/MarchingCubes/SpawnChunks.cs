using System;
using MarchingCubes.Systems;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using Collider = Unity.Physics.Collider;
using Material = UnityEngine.Material;

namespace MarchingCubes
{
    public class SpawnChunks : MonoBehaviour
    {
        public GameObject ChunkGameObjectPrefab;
        public int PointsInRow = 3;
        public static float CHUNK_SIZE = 4;
        public int3 ChunksToSpawn = new int3(2,2,2);

        public bool DebugDraw = false;

        public ConvertToEntity ChunkPrefab;
        
        public Mesh Mesh;
        public Material Material;

        BlobAssetStore _blobAssetStore;
        void Start()
        {
            
            var ecsManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _blobAssetStore = new BlobAssetStore();
            Debug.LogWarning("if physics acts weird it might be because you only have one blob asset for all chunks and are disposing it OnDestroy");
            var conversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
            Entity chunkEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(ChunkGameObjectPrefab, conversionSettings);
            
            ecsManager.AddComponent<ChunkIndex>(chunkEntity);
 
            ecsManager.AddComponent<Scale>(chunkEntity);

//            var chunkArchetype = ecsManager.CreateArchetype(
//                typeof(Translation),
//                typeof(Rotation),
//                typeof(RenderMesh),
//                typeof(LocalToWorld),
//                typeof(ChunkIndex),
//                typeof(PhysicsCollider)
//            );

            
            var pointArchetype = ecsManager.CreateArchetype(
                typeof(MarchingPoint),
                typeof(Translation),
                typeof(ChunkIndex)
            );
            NativeArray<Entity> chunkEntities = new NativeArray<Entity>(ChunksToSpawn.Volume(), Allocator.Temp);
            for (int i = 0; i < chunkEntities.Length; i++)
                chunkEntities[i] = ecsManager.Instantiate(chunkEntity);
            
            NativeArray<ChunkSpawnData> chunkSpawnDatas = new NativeArray<ChunkSpawnData>(ChunksToSpawn.Volume(), Allocator.Temp);
            
            var halfOfAChunk = (new float3(1,1,1)*CHUNK_SIZE)/2f;
            float toIncrement = (float) CHUNK_SIZE / (PointsInRow-1);

            chunkEntities.IndexAsIf3D(ChunksToSpawn, (chunk, indexSpatial, indexArr) =>
            {
                ecsManager.SetName(chunk, $"Chunk [{indexSpatial.x}, {indexSpatial.y}, {indexSpatial.z}] : {indexArr}");
                
                var chunkPos = new float3(indexSpatial.x, indexSpatial.y, indexSpatial.z) * CHUNK_SIZE;
                var chunkIndex = GetChunkIndex(chunkPos, CHUNK_SIZE);
                
                chunkSpawnDatas[indexArr] = new ChunkSpawnData(chunkPos, chunkIndex);
                
                ecsManager.SetComponentData(chunk, new Scale
                {
                    Value = 1
                });
//                ecsManager.SetSharedComponentData(chunk, new RenderMesh
//                {
//                    material = Material,
//                    mesh = Mesh
//                });

                ecsManager.SetComponentData(chunk, new Translation
                {
                    Value = chunkPos
                });
                
                ecsManager.SetSharedComponentData(chunk, new ChunkIndex
                {
                    Index = chunkIndex
                });
                
//                ecsManager.SetComponentData(chunk, new PhysicsCollider
//                {
//                    Value = new Collider();
//                    Value = new BlobAssetReference<Collider>();
//                });


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
            
            ecsManager.DestroyEntity(chunkEntity);
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

        private void OnDestroy()
        {
            _blobAssetStore.Dispose();
        }

        void Update()
        {
            ChunkDebugger.DebugDraw = DebugDraw;
        }
        
        
    }
    
    
}