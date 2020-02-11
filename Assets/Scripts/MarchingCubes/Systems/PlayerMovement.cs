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
            Entities.WithAll<Input>().ForEach((ref PhysicsVelocity velocity, ref Input input, ref Rotation rotation) =>
            { 
    
                velocity.Angular += angularToAdd * input.AngularSensitivty;

                //var forward = Quaternion.LookRotation(Quaternion.ToEulerAngles(rotation.Value));
                //float3 linearVelocityRelative = forward * inputLinear * linearVelocityAbsolute;
                velocity.Linear += linearVelocityAbsolute * input.LinearSensitivity;


                //rotation.Value.
                   // quaternion quarternion;
                //quarternion.value.
                    //Unity.Transforms.

                    
            });
            
            _previousMousePosition = currentMousePosition;
            
            //move
        }
        
        
    }
}