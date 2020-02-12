using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    public struct ChunkIndex : ISharedComponentData
    {
        public int3 Index;
        public float ChunkWidth;
        public float PointsInARow;

        public float DistBetweenDensityCubes
        {
            get => ChunkWidth / (PointsInARow + 1);
        }
    }
}