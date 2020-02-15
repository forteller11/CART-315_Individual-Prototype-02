using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    public struct ChunkIndex : ISharedComponentData
    {
        public int3 Value;
    }
}