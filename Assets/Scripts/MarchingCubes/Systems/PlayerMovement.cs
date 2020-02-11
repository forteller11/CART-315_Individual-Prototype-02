using UnityEngine;
using Unity;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;

namespace MarchingCubes
{
    public class PlayerMovement : ComponentSystem
    {
        float2 _previousMousePosition = float2.zero;
        public float MovementSensitivity = 0.1f;
        public float RotationSensitivity = 0.1f;
        protected override void OnUpdate()
        {
            //rotate
            float2 currentMousePosition = new float2(Input.mousePosition.x, Input.mousePosition.y);
            float2 deltaMousePosition = _previousMousePosition - currentMousePosition;
            float3 angularVelocityToAdd = new float3(
                                              deltaMousePosition.y,
                                              0f,
                                              deltaMousePosition.x)
                                          * RotationSensitivity;

            float3 linearVelocityToAdd = new float3(
                                       Input.GetKeyDown(KeyCode.D).ToInt() - Input.GetKeyDown(KeyCode.A).ToInt(),
                                       0f,
                                       Input.GetKeyDown(KeyCode.W).ToInt() - Input.GetKeyDown(KeyCode.S).ToInt()
                                   ) * MovementSensitivity;
            
            
            Entities.WithAll<Tag_Player>().ForEach((ref PhysicsVelocity velocity) =>
            {
                velocity.Linear += linearVelocityToAdd;
                velocity.Angular += angularVelocityToAdd;
            });
            
            _previousMousePosition = currentMousePosition;
            
            //move
        }
        
        
    }
}