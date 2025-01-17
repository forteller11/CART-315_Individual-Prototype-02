﻿//using System;
//using Unity.Burst;
//using Unity.Collections;
//using Unity.Entities;
//using Unity.Mathematics;
//using Unity.Transforms;
//using UnityEngine;
//using Random = Unity.Mathematics.Random;
//
//namespace MarchingCubes
//{
//    public struct DensityCube : IComponentData
//    {
//        public DensityCube(Random ran) : this()
//        {
//            FDL = ran.NextFloat(1f); 
//            FDR = ran.NextFloat(1f); 
//            FUL = ran.NextFloat(1f); 
//            FUR = ran.NextFloat(1f); 
//                
//            BDL = ran.NextFloat(1f); 
//            BDR = ran.NextFloat(1f); 
//            BUL = ran.NextFloat(1f); 
//            BUR = ran.NextFloat(1f);
//        }
//        public DensityCube(NativeList<float> densities) : this()
//        {
//            if (densities.Length != 8)
//                Debug.LogError("make sure native list has 8 elements!");
//            
//            FDL = densities[0]; 
//            FDR = densities[1]; 
//            FUL = densities[2]; 
//            FUR = densities[3]; 
//                
//            BDL = densities[4]; 
//            BDR = densities[5]; 
//            BUL = densities[6]; 
//            BUR = densities[7]; 
//        }
//        //(forward/back, up/down, left/right)
//        //values: 0-1
//        
//        //forward square
//        private float _FDL;
//        public float FDL
//        {
//            get => _FDL;
//            set
//            {
//                _FDL = value;
//                _FDL = math.clamp(_FDL, 0, 1);
//            }
//        }
//        
//        private float _FDR; //forward down right
//        public float FDR
//        {
//            get => _FDR;
//            set
//            {
//                _FDR = value;
//                _FDR = math.clamp(_FDR, 0, 1);
//            }
//        }
//        
//        private float _FUL;
//        public float FUL
//        {
//            get => _FUL;
//            set
//            {
//                _FUL = value;
//                _FUL = math.clamp(_FUL, 0, 1);
//            }
//        }
//        
//        private float _FUR;
//        public float FUR
//        {
//            get => _FUR;
//            set
//            {
//                _FUR = value;
//                _FUR = math.clamp(_FUR, 0, 1);
//            }
//        }
//        
//        //back square
//        private float _BDL;
//        public float BDL
//        {
//            get => _BDL;
//            set
//            {
//                _BDL = value;
//                _BDL = math.clamp(_BDL, 0, 1);
//            }
//        }
//        
//        private float _BDR;
//        public float BDR
//        {
//            get => _BDR;
//            set
//            {
//                _BDR = value;
//                _BDR = math.clamp(_BDR, 0, 1);
//            }
//        }
//        
//        private float _BUL;
//        public float BUL
//        {
//            get => _BUL;
//            set
//            {
//                _BUL = value;
//                _BUL = math.clamp(_BUL, 0, 1);
//            }
//        }
//        
//        private float _BUR;
//        public float BUR
//        {
//            get => _BUR;
//            set
//            {
//                _BUR = value;
//                _BUR = math.clamp(_BUR, 0, 1);
//            }
//        }
//
//        /// <summary>
//        /// allows to create algo for each point 
//        /// </summary>
//        /// <param name="centerOfCube"></param>
//        /// <param name="chunkIndex"></param>
//        /// <param name="pointBasedAction">density, position</param>
//        public void ForEach(Translation centerOfCube, ChunkIndex chunkIndex, Action<float, float3> pointBasedAction)
//        {
//            float3 c = centerOfCube.Value;
//            float w = chunkIndex.DensityCubeWidth/2;
//            
//            
//            //calculate pos of each point
//            pointBasedAction.Invoke(FDL, c + new float3(w,-w,-w));
//            pointBasedAction.Invoke(FDR, c + new float3(w,-w,w));
//            pointBasedAction.Invoke(FUL, c + new float3(w,w,-w));
//            pointBasedAction.Invoke(FUR, c + new float3(w,w,w));
//            
//            pointBasedAction.Invoke(BDL, c + new float3(-w,-w,-w));
//            pointBasedAction.Invoke(BDR, c + new float3(-w,-w,w));
//            pointBasedAction.Invoke(BUL, c + new float3(-w,w,-w));
//            pointBasedAction.Invoke(BUR, c + new float3(-w,w,w));
//
////            FDL = func.Invoke(FDL);
////            FDR = func.Invoke(FDR);
////            FUL = func.Invoke(FUL);
////            FUR = func.Invoke(FUR);
////            
////            BDL = func.Invoke(BDL);
////            BDR = func.Invoke(BDR);
////            BUL = func.Invoke(BUL);
////            BUR = func.Invoke(BUR);
//        }
//
//        public int VertFlagsFromThreshold(float threshold)
//        {
//            //BDR 0
//            //FDR 1
//            //FDL 2
//            //BDL 3
//            //BUR 4
//            //FUR 5
//            //FUL 6
//            //BUL 7
//            
//            int v0 = (BDR > threshold) ? 1 << 0 : 0;
//            int v1 = (FDR > threshold) ? 1 << 1 : 0;
//            int v2 = (FDL > threshold) ? 1 << 2 : 0;
//            int v3 = (BDL > threshold) ? 1 << 3 : 0;
//            int v4 = (BUR > threshold) ? 1 << 4 : 0;
//            int v5 = (FUR > threshold) ? 1 << 5 : 0;
//            int v6 = (FUL > threshold) ? 1 << 6 : 0;
//            int v7 = (BUL > threshold) ? 1 << 7 : 0;
//
//            return v0 + v1 + v2 + v3 + v4 + v5 + v6 + v7;
//        }
//
//        public float3 EdgePositionFromIndex(int index)
//        {
//            //0 fdr, bdr
//            //1 fdr, fdl
//            //2 fdl, bdl
//            //3 bdl, bdr
//            //4 fur, bur
//            //5 fur ful
//            //6 ful bul
//            //7 bul bur
//            //8 bur bdr
//            //9 fdr fur
//            //10 fur fdr
//            //11 bur bdr
//            if (index == 0) return (FDR + BDR) / 2f;
//            if (index == 1) return (FDR + FDL) / 2f;
//            if (index == 2) return (FDL + BDL) / 2f;
//            if (index == 3) return (BDL + BDR) / 2f;
//            if (index == 4) return (FUR + BUR) / 2f;
//            if (index == 5) return (FUR + FUL) / 2f;
//            if (index == 6) return (FUL + BUL) / 2f;
//            if (index == 7) return (BUL + BUR) / 2f;
//            if (index == 8) return (BUR + BDR) / 2f;
//            if (index == 9) return (FDR + FUR) / 2f;
//            if (index == 10) return (FUR + FDR) / 2f;
//            if (index == 11) return (BUR + BDR) / 2f;
//
//            return new float3 (index, -99, -99);
//            throw new System.ArgumentOutOfRangeException("Index must be between 0-11!");
//                    
//            }
//
//        public Vector3[] GetAllEdgePositions(float3 centerOfCube, float3 centerOfChunk, float CubeWidth)
//        {
//            Vector3 c = centerOfCube-centerOfChunk; //pos relative to chunk center
//            float w = CubeWidth;
//            
//            //MAKE POSITIONS BASED ON CORNERS
//            Debug.LogWarning("Make density cubes trnslation at fdr, and all positions based on that.... also store point posiitons?");
//            Vector3 bdr = new Vector3(w, -w, -w); //BDR
//            Vector3 fdr = new Vector3(w, -w, w);
//            Vector3 fdl = new Vector3(-w,-w,w);
//            Vector3 bdl = new Vector3(-w,-w,-w);
//
//            Vector3 bur = new Vector3(w, w, -w);
//            Vector3 fur = new Vector3(w,w,w);
//            Vector3 ful = new Vector3(-w,w,-w);
//            Vector3 bul = new Vector3(-w, w, -w);
//            
//            return new Vector3[]
//            {
//                c + ((fdr + bdr) / 2f),
//                c + ((fdr + fdl) / 2f),
//                c + ((fdl + bdl) / 2f),
//                c + ((bdl + bdr) / 2f),
//                c + ((fur + bur) / 2f),
//                c + ((fur + ful) / 2f),
//                c + ((ful + bul) / 2f),
//                c + ((bul + bur) / 2f),
//                c + ((bur + bdr) / 2f),
//                c + ((fdr + fur) / 2f),
//                c + ((fur + fdr) / 2f),
//                c + ((bur + bdr) / 2f)
//            };
//            
//        }
//
//        public int[] GetAllDenseIndices(float threshold, int inc)
//        {
//            NativeList<int> indices = new NativeList<int>(Allocator.Temp);
//
//            if (FDR > threshold)
//            {
//                var tri = new NativeArray<int>(3, Allocator.Temp);
//                tri[0] = 9 + inc;
//                tri[1] = 1 + inc;
//                tri[2] = 0 + inc;
//                indices.AddRange(tri);
//            }
//            if (FDL > threshold)
//                         {
//                             var tri = new NativeArray<int>(3, Allocator.Temp);
//                             tri[0] = 10 + inc;
//                             tri[1] = 1 + inc;
//                             tri[2] = 2 + inc;
//                             indices.AddRange(tri);
//                         }
//            if (FUL > threshold)
//            {
//                var tri = new NativeArray<int>(3, Allocator.Temp);
//                tri[0] = 5 + inc;
//                tri[1] = 10 + inc;
//                tri[2] = 6 + inc;
//                indices.AddRange(tri);
//            }
//            if (FUR > threshold)
//                         {
//                             var tri = new NativeArray<int>(3, Allocator.Temp);
//                             tri[0] = 5 + inc;
//                             tri[1] = 4 + inc;
//                             tri[2] = 9 + inc;
//                             indices.AddRange(tri);
//                         }
//            
//            if (BDR > threshold)
//            {
//                var tri = new NativeArray<int>(3, Allocator.Temp);
//                tri[0] = 8 + inc;
//                tri[1] = 3 + inc;
//                tri[2] = 0 + inc;
//                indices.AddRange(tri);
//            }
//            if (BDL > threshold)
//            {
//                var tri = new NativeArray<int>(3, Allocator.Temp);
//                tri[0] = 11 + inc;
//                tri[1] = 2 + inc;
//                tri[2] = 3 + inc;
//                indices.AddRange(tri);
//            }
//            if (BUL > threshold)
//            {
//                var tri = new NativeArray<int>(3, Allocator.Temp);
//                tri[0] = 6 + inc;
//                tri[1] = 11 + inc;
//                tri[2] = 7 + inc;
//                indices.AddRange(tri);
//            }
//            if (BUL > threshold)
//            {
//                var tri = new NativeArray<int>(3, Allocator.Temp);
//                tri[0] = 6 + inc;
//                tri[1] = 11 + inc;
//                tri[2] = 7 + inc;
//                indices.AddRange(tri);
//            }
//            
//            int[] arr = indices.ToArray();
//            indices.Dispose();
//            return arr;
//        }
//        }
//    }
