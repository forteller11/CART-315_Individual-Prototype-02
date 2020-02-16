using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    public struct ChunkIndex : IComponentData
    {
        public int3 Value;
    }
}