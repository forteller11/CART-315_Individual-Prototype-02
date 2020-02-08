using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace MarchingCubes
{
    [BurstCompile]
    [InternalBufferCapacity(POINTS_IN_CHUNKS)]
    
    static ChunkSpawner POINTS_IN_CHUNKS = 8;
    public struct MarchingChunk : IDynamicBufferContainer
    {
        public Type ElementType { get; }
        public NativeList<MarchingPoint> Points;
    }
}


//points as entities with chunkPos and values

//chunks as entities with dynamic buffers with point refs, 
//meshes
//dirty flags

//system to initial opints into chunks
//system to draw chunks in world space

//system to raycast chunks, then change values accordingly

//system marching cube to create mesh from chunks

//input

//chunk to physics

//build points in model space

//assign points to chunk....

//build debugger to draw in world space

//chunks store refs using idynamicbuffer

///orr... just add different tags dynamically using hashmap?
///
/// or store via hashmap