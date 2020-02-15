using Unity.Entities;

namespace MarchingCubes
{
    public struct ChunkSettingsSingleton : IComponentData
    {
        public float ChunkWidth;
        public int VoxelsInARow;
    }
}