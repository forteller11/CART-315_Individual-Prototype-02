using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace MarchingCubes
{
    public struct DensityCube : IComponentData
    {
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
        public void ForEach(Translation centerOfCube, ChunkIndex chunkIndex, Action<float, float3> pointBasedAction)
        {
            float3 c = centerOfCube.Value;
            float w = chunkIndex.ChunkWidth/chunkIndex.PointsInARow-1;
            
            
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
    }
}