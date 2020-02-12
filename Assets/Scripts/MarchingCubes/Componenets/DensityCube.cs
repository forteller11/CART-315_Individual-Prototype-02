using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace MarchingCubes
{
    public struct DensityCube : IComponentData
    {

        public DensityCube(NativeList<float> densities) : this()
        {
            if (densities.Length != 8)
                Debug.LogError("make sure native list has 8 elements!");
            
            FDL = densities[0]; 
            FDR = densities[1]; 
            FUL = densities[2]; 
            FUR = densities[3]; 
                
            BDL = densities[4]; 
            BDR = densities[5]; 
            BUL = densities[6]; 
            BUR = densities[7]; 
        }
        //(forward/back, up/down, left/right)
        //values: 0-1
        
        //forward square
        private float _FDL;
        public float FDL
        {
            get => _FDL;
            set
            {
                _FDL = value;
                _FDL = math.clamp(_FDL, 0, 1);
            }
        }
        
        private float _FDR; //forward down right
        public float FDR
        {
            get => _FDR;
            set
            {
                _FDR = value;
                _FDR = math.clamp(_FDR, 0, 1);
            }
        }
        
        private float _FUL;
        public float FUL
        {
            get => _FUL;
            set
            {
                _FUL = value;
                _FUL = math.clamp(_FUL, 0, 1);
            }
        }
        
        private float _FUR;
        public float FUR
        {
            get => _FUR;
            set
            {
                _FUR = value;
                _FUR = math.clamp(_FUR, 0, 1);
            }
        }
        
        //back square
        private float _BDL;
        public float BDL
        {
            get => _BDL;
            set
            {
                _BDL = value;
                _BDL = math.clamp(_BDL, 0, 1);
            }
        }
        
        private float _BDR;
        public float BDR
        {
            get => _BDR;
            set
            {
                _BDR = value;
                _BDR = math.clamp(_BDR, 0, 1);
            }
        }
        
        private float _BUL;
        public float BUL
        {
            get => _BUL;
            set
            {
                _BUL = value;
                _BUL = math.clamp(_BUL, 0, 1);
            }
        }
        
        private float _BUR;
        public float BUR
        {
            get => _BUR;
            set
            {
                _BUR = value;
                _BUR = math.clamp(_BUR, 0, 1);
            }
        }

        /// <summary>
        /// allows to create algo for each point 
        /// </summary>
        /// <param name="centerOfCube"></param>
        /// <param name="chunkIndex"></param>
        /// <param name="pointBasedAction">density, position</param>
        public void ForEach(Translation centerOfCube, ChunkData chunkIndex, Action<float, float3> pointBasedAction)
        {
            float3 c = centerOfCube.Value;
            float w = chunkIndex.DensityCubeWidth/2;
            
            
            //calculate pos of each point
            pointBasedAction.Invoke(FDL, c + new float3(w,-w,-w));
            pointBasedAction.Invoke(FDR, c + new float3(w,-w,w));
            pointBasedAction.Invoke(FUL, c + new float3(w,w,-w));
            pointBasedAction.Invoke(FUR, c + new float3(w,w,w));
            
            pointBasedAction.Invoke(BDL, c + new float3(-w,-w,-w));
            pointBasedAction.Invoke(BDR, c + new float3(-w,-w,w));
            pointBasedAction.Invoke(BUL, c + new float3(-w,w,-w));
            pointBasedAction.Invoke(BUR, c + new float3(-w,w,w));

//            FDL = func.Invoke(FDL);
//            FDR = func.Invoke(FDR);
//            FUL = func.Invoke(FUL);
//            FUR = func.Invoke(FUR);
//            
//            BDL = func.Invoke(BDL);
//            BDR = func.Invoke(BDR);
//            BUL = func.Invoke(BUL);
//            BUR = func.Invoke(BUR);
        }

        public int EdgeFlagsFromThreshold(float threshold)
        {
            //BDR 0
            //FDR 1
            //FDL 2
            //BDL 3
            //BUR 4
            //FUR 5
            //FUL 6
            //BUL 7
            
            int v0 = (BDR > threshold) ? 1 << 0 : 0;
            int v1 = (FDR > threshold) ? 1 << 1 : 0;
            int v2 = (FDL > threshold) ? 1 << 2 : 0;
            int v3 = (BDL > threshold) ? 1 << 3 : 0;
            int v4 = (BUR > threshold) ? 1 << 4 : 0;
            int v5 = (FUR > threshold) ? 1 << 5 : 0;
            int v6 = (FUL > threshold) ? 1 << 6 : 0;
            int v7 = (BUL > threshold) ? 1 << 7 : 0;

            return v0 + v1 + v2 + v3 + v4 + v5 + v6 + v7;
        }
    }
}