using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
  [GenerateAuthoringComponent]
    public struct Input : IComponentData
    {
        public float2 Linear;
        public float2 Angular;
        public float Build;
    }
}