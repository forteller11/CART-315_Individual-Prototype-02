using System;
using Unity.Entities;

namespace MarchingCubes
{
    [GenerateAuthoringComponent]
    [InternalBufferCapacity(3 * 3 * 3)]
    public struct ChunkDensities : IBufferElementData
    {
        public float Density;

        public ChunkDensities(float density)
        {
            Density = density;
        }

        public static implicit operator float(ChunkDensities e) { return e.Density; }
        public static implicit operator ChunkDensities(int e) { return new ChunkDensities { Density = e }; }

    }
}