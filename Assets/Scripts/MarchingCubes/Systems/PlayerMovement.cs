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

        protected override void OnCreate() => _input = new PlayerControls();
        protected override void OnStartRunning() => _input.Enable();
        protected override void OnStopRunning() =>_input.Disable();

        protected override void OnUpdate()
        {
            //rotate
            float2 currentMousePosition = new float2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
            float2 deltaMousePosition = _previousMousePosition - currentMousePosition;

            float2 inputAngular = _input.PlayerMovement.Rotate.ReadValue<Vector2>();


            float2 inputLinear = _input.PlayerMovement.Translate.ReadValue<Vector2>();
            float3 linearVelocityAbsolute = new float3(inputLinear.x, 0, inputLinear.y);
            var t = Time.DeltaTime;
            
            Debug.Log($"--------------------");
            //Debug.Log($"linear: {inputLinear}");
            //Debug.Log($"angular: {inputAngular}");
            Entities.WithAll<Input>().ForEach((ref PhysicsVelocity velocity, ref Input input, ref Rotation rotation, ref Translation translation) =>
            { 
                float3 upVectorAbs = new float3(0,1,0);
                   
                
                //rotate around upvectorabs
                //then rotate around right vector abs
                
                float2 angularToAdd = inputAngular * input.Angular;
                float3 forwardVectorAbs = new float3(0,0,1);
                float3 rightVectorAbs = new float3(1,0,0);
                
                var rotMat1 = new float3x3(rotation.Value);
                var r1 = Quaternion.AngleAxis(inputAngular.x, math.mul(rotMat1, upVectorAbs));
                rotation.Value = math.mul(rotation.Value, r1);
                
                var rotMat2 = new float3x3(rotation.Value);
                var r2 = Quaternion.AngleAxis(inputAngular.y, math.mul(rotMat2, forwardVectorAbs));
                rotation.Value = math.mul(rotation.Value, r2);
                

      
                //movement controller
                
                var rotMat3 = new float3x3(rotation.Value);

              
                float3 forwardVectorRelative = math.mul(rotMat3, forwardVectorAbs);
                float3 rightVectorRelative = math.mul(rotMat3, rightVectorAbs);
                float3 noVerticalMovement = new float3(1,0,1);
                
                
                Debug.DrawLine(translation.Value, translation.Value + forwardVectorRelative*10000, Color.yellow);
                Debug.DrawLine(translation.Value, translation.Value + rightVectorRelative*100, Color.red);
    
                velocity.Linear += forwardVectorRelative   * noVerticalMovement * input.Linear.y * inputLinear.y * t;
                velocity.Linear += rightVectorRelative * noVerticalMovement * input.Linear.x * inputLinear.x * t;
       


            });
            
            _previousMousePosition = currentMousePosition;
            
            //move
        }
        
        
    }
}