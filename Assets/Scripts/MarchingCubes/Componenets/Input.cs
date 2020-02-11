using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    [GenerateAuthoringComponent]
    public struct Input : IComponentData
    {
        public float3 LinearSensitivity;
        public float3 AngularSensitivty;
    }
}