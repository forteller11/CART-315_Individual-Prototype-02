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
        /// <param name="action"> where int represents index in flat array, in3 is 3d array, int3 is 3d array</param>

        public static void IndexAsIf3D(this NativeArray<Entity> entities, int3 dimensions, Action<int, int3, int3> action)
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
                        
                        action.Invoke(
                            index, 
                            new int3(i,j,k), 
                            new int3(dimensions.z * dimensions.y,dimensions.z,1));
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
        public static int ToInt(this bool x) => (x) ? 1 : 0;

    }
}