using System;
using Unity.Mathematics;

namespace MarchingCubes
{
    public static class Utils
    {
        /// <summary>
        /// Allows convenient access to 3d array
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="dimensions"> dimensions of 3D array</param>
        /// <param name="action"> where int represents index in flat array, in3 is 3d array, int3 is 3d array</param>
        
        public static void IndexAsIf3D(int3 dimensions, Action<int, int3, int3> action)
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
        
        
    }
}