using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    public struct MarchingPoint : IComponentData
    {
        private float _density;
        public float Density
        {
            get => _density;
            set
            {
                _density = value;
                _density = math.clamp(_density, 0, 1);
            }
        }

       
    }
}