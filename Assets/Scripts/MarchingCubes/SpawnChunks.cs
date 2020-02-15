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
        [Range(0,16)] public int VoxelsInARow = 3;
        [Range(0,16)] public float ChunkWidth = 4;
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
            Debug.LogWarning("Make sure the internal buffer capacity of chunkDensities is (points in a row)^3");
            var conversionSettings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, _blobAssetStore);
            Entity chunkEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(ChunkGameObjectPrefab, conversionSettings);
            
            ecsManager.AddComponent<ChunkIndex>(chunkEntity);
 
            ecsManager.AddComponent<Scale>(chunkEntity);

            ///////CHUNK SETTINGS SINGLETON
            var chunkSettingsEntity = ecsManager.CreateEntity(typeof(ChunkSettingsSingleton));
                ecsManager.SetComponentData(chunkSettingsEntity, new ChunkSettingsSingleton {ChunkWidth = ChunkWidth, VoxelsInARow = VoxelsInARow});
            
            //Create a Instantiate array of chunks
            NativeArray<Entity> chunks = new NativeArray<Entity>(ChunksToSpawn.Volume(), Allocator.Temp);
            for (int i = 0; i < chunks.Length; i++)
                chunks[i] = ecsManager.Instantiate(chunkEntity);
            
            
            //set chunk datas
            chunks.IndexAsIf3D(ChunksToSpawn, (indexFlat, index3D, indexJumps) =>
            {
                ecsManager.SetName(chunks[indexFlat], $"Chunk [{index3D.x}, {index3D.y}, {index3D.z}] : {indexFlat}");
                
                var chunkPos = new float3(index3D.x, index3D.y, index3D.z) * ChunkWidth;
                var chunkIndex = GetChunkIndex(chunkPos, ChunkWidth);
                
                ecsManager.SetComponentData(chunks[indexFlat], new Scale { Value = 1 });
                ecsManager.SetComponentData(chunks[indexFlat], new Translation { Value = chunkPos });
                ecsManager.SetSharedComponentData(chunks[indexFlat], new ChunkIndex { Value = chunkIndex });
                Debug.LogWarning("make sure initial densities are in fact what you tried to set them to, or else this is copy by value not ref!");
                var densities = ecsManager.GetBuffer<ChunkDensities>(chunks[indexFlat]).Reinterpret<float>();
                densities[indexFlat] = ran.NextFloat(1f);

            });


//            pointEntities.IndexAsIf4D(pointsToSpawn,(point, indexSpatial, indexArr) =>
//            {
//                ecsManager.SetName(point, $"Point [{indexSpatial.y}, {indexSpatial.z}, {indexSpatial.w}] Chunk {indexSpatial.x}");
//                var cubeWidth = chunkSpawnDatas[indexSpatial.x].ChunkData.DensityCubeWidth;
//                var chunkWidth = chunkSpawnDatas[indexSpatial.x].ChunkData.ChunkWidth;
//                var posWithinChunk = (new float3(indexSpatial.y, indexSpatial.z, indexSpatial.w) * cubeWidth) + cubeWidth/2 ;
//                posWithinChunk -= new float3(chunkWidth,chunkWidth,chunkWidth)/2;
//                var worldPos = posWithinChunk + chunkSpawnDatas[indexSpatial.x].Position;
//                
//                ecsManager.SetComponentData(point, new Translation { Value = worldPos });
//                    
//                ecsManager.SetComponentData(point, new DensityCube (ran));
//
//                ecsManager.SetSharedComponentData(point, new ChunkIndex
//                {
//                    Value = chunkSpawnDatas[indexSpatial.x].ChunkData.Index,
//                    ChunkWidth = ChunkWidth,
//                    PointsInARow = POINTS_IN_CHUNK_ROW
//                });
//                    
//            });
//            
            ecsManager.DestroyEntity(chunkEntity);
            chunks.Dispose();
        }
        
        public int3 GetChunkIndex(float3 position, float chunkSize){
            return  (int3) (position / chunkSize);
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