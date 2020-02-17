using UnityEngine;
using Unity;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using UnityEngine.InputSystem;

namespace MarchingCubes
{
    public class PlayerMovement : ComponentSystem
    {
        float2 _previousMousePosition = float2.zero;
        private PlayerControls _input;
        private float3 _forwardCache = new float3(0, 1, 1);
        private float3 _upCache = new float3(0,1,0);

        protected override void OnCreate() => _input = new PlayerControls();
        protected override void OnStartRunning() => _input.Enable();
        protected override void OnStopRunning() =>_input.Disable();

        protected override void OnUpdate()
        {

            


            float2 inputLinear = _input.PlayerMovement.Translate.ReadValue<Vector2>();
            float3 linearVelocityAbsolute = new float3(inputLinear.x, 0, inputLinear.y);
            var t = Time.DeltaTime;
            
            Entities.WithAll<Input>().ForEach((ref PhysicsVelocity velocity, ref Input sensitivity, ref Rotation rotation, ref Translation translation) =>
            { 
                float3 upVectorAbsolute = new float3(0,1,0);
                   
                
                //rotate around upvectorabs
                //then rotate around right vector abs
                
                float3 forwardVectorAbsolute = new float3(0,0,1);
                float3 rightVectorAbsolute = new float3(1,0,0);

                var rotMat3 = new float3x3(rotation.Value);
                
                float3 forwardVectorRelative = math.mul(rotMat3, forwardVectorAbsolute);
                float3 rightVectorRelative = math.mul(rotMat3, rightVectorAbsolute);
                
                float3 noVerticalMovement = new float3(1,0,1);

                Debug.DrawLine(translation.Value, translation.Value + forwardVectorRelative, Color.blue);

                velocity.Linear += forwardVectorRelative   * noVerticalMovement * sensitivity.Linear.y * inputLinear.y * t;
                velocity.Linear += rightVectorRelative * noVerticalMovement * sensitivity.Linear.x * inputLinear.x * t;

            });

            //move
        }
        
        
    }
}