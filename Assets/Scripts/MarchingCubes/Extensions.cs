using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    public static class Extensions
    {

        

        
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Volume(this int3 n) =>  n.x * n.y * n.z; 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Volume(this int4 n) =>  n.x * n.y * n.z * n.w;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt(this bool x) => (x) ? 1 : 0;

    }
}