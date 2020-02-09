using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    public static class Extensions
    {
        /// <summary>
        /// Allows convenient access to 3d array
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="dimensions"> dimensions of 3D array</param>
        /// <param name="action"> action to perform</param>
        public static void IndexAsIf3D(this NativeArray<Entity> entities, int3 dimensions, Action<Entity, int3, int> action)
        {
            for (int i = 0; i < dimensions.x; i++)
            {
                for (int j = 0; j < dimensions.y; j++)
                {
                    for (int k = 0; k < dimensions.z; k++)
                    {
                        int ii = (i * dimensions.z * dimensions.y);
                        int jj = (j * dimensions.z);
                        int kk = (k);
                        int index = ii + jj + kk;
                        
                        action.Invoke(entities[index], new int3(i,j,k), index);
                    }
                }
            }
        }

        
        public static void IndexAsIf4D(this NativeArray<Entity> entities, int4 dimensions, Action<Entity, int4, int> action)
        {
            for (int i = 0; i < dimensions.x; i++)
            {
                for (int j = 0; j < dimensions.y; j++)
                {
                    for (int k = 0; k < dimensions.z; k++)
                    {
                        for (int w = 0; w < dimensions.w; w++)
                        {
                            int ii = i * dimensions.w * dimensions.z * dimensions.y;
                            int jj = j * dimensions.w * dimensions.z;
                            int kk = k * dimensions.w;
                            int ww = w;
                            int index = ii + jj + kk + ww;

                            action.Invoke(entities[index], new int4(i,j,k,w), index);
                        }
                    }
                }
            }
        }

        public static int Volume(this int3 n) =>  n.x * n.y * n.z; 
        public static int Volume(this int4 n) =>  n.x * n.y * n.z * n.w; 

    }
}