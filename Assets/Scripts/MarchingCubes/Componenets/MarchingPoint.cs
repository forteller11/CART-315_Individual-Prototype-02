using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    public struct MarchingPoint : IComponentData
    {
        public float Density;
    }
}