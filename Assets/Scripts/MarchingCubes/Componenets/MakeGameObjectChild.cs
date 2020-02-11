using Unity.Entities;
using UnityEngine;

namespace MarchingCubes
{
    [GenerateAuthoringComponent]
    public struct MakeGameObjectChild : ISharedComponentData
    {
        public GameObject Value;
    }
}