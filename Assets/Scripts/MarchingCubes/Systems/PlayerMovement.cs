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

            var inputAngular = _input.PlayerMovement.Rotate.ReadValue<Vector2>();
            float3 angularToAdd = new float3(-inputAngular.y, -inputAngular.x, 0);
            
            
            var inputLinear = _input.PlayerMovement.Translate.ReadValue<Vector2>();
            float3 linearVelocityAbsolute = new float3(inputLinear.x, 0, inputLinear.y);
            
            Debug.Log($"--------------------");
            Debug.Log($"linear: {inputLinear}");
            Debug.Log($"angular: {inputAngular}");
            Entities.WithAll<Input>().ForEach((ref PhysicsVelocity velocity, ref Input input, ref Rotation rotation, ref Translation translation) =>
            { 
    
                velocity.Angular += angularToAdd * input.AngularSensitivty;

                //var forward = Quaternion.LookRotation(Quaternion.ToEulerAngles(rotation.Value));
                //float3 linearVelocityRelative = forward * inputLinear * linearVelocityAbsolute;
             
                
                //movement controller
                float3 forwardVectorAbs = new float3(0,0,1);
                float3 rightVectorAbs = new float3(1,0,0);
                
                var rotMat = new float3x3(rotation.Value);
                
                float3 forwardVectorRelative = math.mul(rotMat, forwardVectorAbs);
                float3 rightVectorRelative = math.mul(rotMat, rightVectorAbs);
                float3 noVerticalMovement = new float3(1,0,1);
                
                
                Debug.DrawLine(translation.Value, translation.Value + forwardVectorRelative*10000, Color.yellow);
                Debug.DrawLine(translation.Value, translation.Value + rightVectorRelative*100, Color.red);
    
                velocity.Linear += forwardVectorRelative   * noVerticalMovement * input.LinearSensitivity.z * inputLinear.y;
                velocity.Linear += rightVectorRelative * noVerticalMovement * input.LinearSensitivity.x * inputLinear.x;
       


            });
            
            _previousMousePosition = currentMousePosition;
            
            //move
        }
        
        
    }
}