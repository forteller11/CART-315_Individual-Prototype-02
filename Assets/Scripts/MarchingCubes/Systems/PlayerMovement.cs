using UnityEngine;
using Unity;
using Unity.Entities;
using Unity.Mathematics;

namespace MarchingCubes
{
    public class PlayerMovement : ComponentSystem
    {
        float2 _previousMousePosition = float2.zero;
        protected override void OnUpdate()
        {
            //rotate
            float2 currentMousePosition = new float2(Input.mousePosition.x, Input.mousePosition.y);
            float2 deltaMousePosition = _previousMousePosition - currentMousePosition;
            
            
            
            _previousMousePosition = currentMousePosition;
            
            //move
        }
        
        
    }
}