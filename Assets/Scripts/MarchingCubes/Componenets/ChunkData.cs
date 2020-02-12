using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    public struct ChunkData : ISharedComponentData
    {
        public int3 Index;
        public float ChunkWidth;
        public float PointsInARow;

        public float DensityCubeWidth
        {
            get => ChunkWidth / (PointsInARow + 1);
        }
        
        
    }
}