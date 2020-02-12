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
    using Random = Unity.Mathematics.Random;
namespace MarchingCubes
{
    public class SpawnChunks : MonoBehaviour
    {
        [SerializeField]
        public GameObject ChunkGameObjectPrefab;
        public int PointsInRow = 3;
        public static float CHUNK_SIZE = 4;
        public int3 ChunksToSpawn = new int3(2,2,2);
        [Header("Debugging")]
        public bool DebugDraw = false;
        [Range(0,.5f)]
        public float BaseDensityAlpha = .1f;
        [Range(0,.5f)]
        public float BaseDensityVectorLength = 0.05f;

        [Header("Marching Cubes")] 
        [Range(0,1)]
        public float MarchingCubesThreshold = 0.5f;
        [Range(0,1)]
        public float InitialDensity = 0f;
        

        BlobAssetStore _blobAssetStore;
        void Start()
        {
            Random ran = new Random(300);
            var ecsManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _blobAssetStore = new BlobAssetStore();
            Debug.LogWarning("if physics acts weird it might be because you only have one blob asset for all chunks and are disposing it OnDestroy");
            var conversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
            Entity chunkEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(ChunkGameObjectPrefab, conversionSettings);
            
            ecsManager.AddComponent<ChunkData>(chunkEntity);
 
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
                typeof(DensityCube),
                typeof(Translation),
                typeof(ChunkData)
            );
            NativeArray<Entity> chunkEntities = new NativeArray<Entity>(ChunksToSpawn.Volume(), Allocator.Temp);
            for (int i = 0; i < chunkEntities.Length; i++)
                chunkEntities[i] = ecsManager.Instantiate(chunkEntity);
            
            NativeArray<ChunkCubeSpawnData> chunkSpawnDatas = new NativeArray<ChunkCubeSpawnData>(ChunksToSpawn.Volume(), Allocator.Temp);
            
            var halfOfAChunk = (new float3(1,1,1)*CHUNK_SIZE)/2f;

            chunkEntities.IndexAsIf3D(ChunksToSpawn, (chunk, indexSpatial, indexArr) =>
            {
                ecsManager.SetName(chunk, $"Chunk [{indexSpatial.x}, {indexSpatial.y}, {indexSpatial.z}] : {indexArr}");
                
                var chunkPos = new float3(indexSpatial.x, indexSpatial.y, indexSpatial.z) * CHUNK_SIZE;
                var chunkIndex = GetChunkIndex(chunkPos, CHUNK_SIZE);
                
                
                ecsManager.SetComponentData(chunk, new Scale { Value = 1 });

                ecsManager.SetComponentData(chunk, new Translation { Value = chunkPos });
                
                var chunkData = new ChunkData
                {
                    Index = chunkIndex,
                    ChunkWidth = CHUNK_SIZE,
                    PointsInARow = PointsInRow
                };
                
                ecsManager.SetSharedComponentData(chunk, chunkData);
                
                chunkSpawnDatas[indexArr] = new ChunkCubeSpawnData(chunkPos, chunkData);
                


            });

            int4 pointsToSpawn = new int4(ChunksToSpawn.Volume(),PointsInRow ,PointsInRow, PointsInRow);
            var pointEntities = ecsManager.CreateEntity(pointArchetype, pointsToSpawn.Volume(), Allocator.Temp);
            
            pointEntities.IndexAsIf4D(pointsToSpawn,(point, indexSpatial, indexArr) =>
            {
                ecsManager.SetName(point, $"Point [{indexSpatial.y}, {indexSpatial.z}, {indexSpatial.w}] Chunk {indexSpatial.x}");
                var cubeWidth = chunkSpawnDatas[indexSpatial.x].ChunkData.DensityCubeWidth;
                var chunkWidth = chunkSpawnDatas[indexSpatial.x].ChunkData.ChunkWidth;
                var posWithinChunk = (new float3(indexSpatial.y, indexSpatial.z, indexSpatial.w) * cubeWidth) + cubeWidth/2 ;
                posWithinChunk -= new float3(chunkWidth,chunkWidth,chunkWidth)/2;
                var worldPos = posWithinChunk + chunkSpawnDatas[indexSpatial.x].Position;
                
                ecsManager.SetComponentData(point, new Translation { Value = worldPos });
                    
                ecsManager.SetComponentData(point, new DensityCube (ran));

                ecsManager.SetSharedComponentData(point, new ChunkData
                {
                    Index = chunkSpawnDatas[indexSpatial.x].ChunkData.Index,
                    ChunkWidth = CHUNK_SIZE,
                    PointsInARow = PointsInRow
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

        private struct ChunkCubeSpawnData
        {
            public float3 Position;
            public ChunkData ChunkData;

            public ChunkCubeSpawnData(float3 position, ChunkData chunkData)
            {
                Position = position;
                ChunkData = chunkData;
            }
        }

        private void OnDestroy()
        {
            _blobAssetStore.Dispose();
        }

        void Update()
        {
            ChunkDebugger.DebugDraw = DebugDraw;
            ChunkDebugger.BaseAlpha = BaseDensityAlpha;
            ChunkDebugger.BaseSize = BaseDensityVectorLength;
            Systems.MarchingCubes.MarchingCubesThreshold = MarchingCubesThreshold;
        }
        
        
    }
    
    
}