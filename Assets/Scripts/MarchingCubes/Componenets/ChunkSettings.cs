using System;
using Unity.Entities;

namespace MarchingCubes
{
    public struct ChunkSettingsSingleton : IComponentData
    {
        public float ChunkWidth;
        public int VoxelsInARow;
        public float WidthBetweenVoxels;

        public ChunkSettingsSingleton(float chunkWidth, int voxelsInARow)
        {
            ChunkWidth = chunkWidth;
            VoxelsInARow = voxelsInARow;
            
            if (voxelsInARow > 1)
                WidthBetweenVoxels = chunkWidth / (voxelsInARow - 1);
            else 
                throw new System.ArgumentException("Must have more than 1 point in each chunk row!");
        }
    }
}