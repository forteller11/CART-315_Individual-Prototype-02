using Unity.Entities;
using UnityEngine;

namespace MarchingCubes
{
    [GenerateAuthoringComponent]
    public struct MakeGameObjectChild : IComponentData
    {
        public GameObject Value;
    }
}