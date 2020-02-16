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
            //rotate
            float2 currentMousePosition = new float2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
            float2 deltaMousePosition = _previousMousePosition - currentMousePosition;

            float2 inputAngular = _input.PlayerMovement.Rotate.ReadValue<Vector2>();


            float2 inputLinear = _input.PlayerMovement.Translate.ReadValue<Vector2>();
            float3 linearVelocityAbsolute = new float3(inputLinear.x, 0, inputLinear.y);
            var t = Time.DeltaTime;
            
            //Debug.Log($"linear: {inputLinear}");
            //Debug.Log($"angular: {inputAngular}");
            Entities.WithAll<Input>().ForEach((ref PhysicsVelocity velocity, ref Input sensitivity, ref Rotation rotation, ref Translation translation) =>
            { 
                float3 upVectorAbsolute = new float3(0,1,0);
                   
                
                //rotate around upvectorabs
                //then rotate around right vector abs
                
                float2 angularToAdd = inputAngular * sensitivity.Angular;
                float3 forwardVectorAbs = new float3(0,0,1);
                float3 rightVectorAbsolute = new float3(1,0,0);
                float3 upVectorRelative = math.mul(rotation.Value, upVectorAbsolute);

                var angularForce = new float3(inputAngular.x * sensitivity.Angular.x,
                    inputAngular.y * sensitivity.Angular.y, 0f);
                var r1 = Quaternion.AngleAxis(angularForce.x, upVectorRelative);
                Debug.DrawLine(translation.Value, translation.Value + upVectorRelative, Color.green);
                Debug.DrawLine(translation.Value, translation.Value + upVectorAbsolute, new Color(0,1,0,0.2f));
                rotation.Value = math.mul(rotation.Value, r1);
                
                float3 rightVectorRelative = math.mul(rotation.Value, rightVectorAbsolute);
//                Debug.DrawLine(translation.Value, translation.Value + rightVectorRelative, new Color(1,0,0,1f));
                var r2 = Quaternion.AngleAxis(angularForce.y, rightVectorRelative);
//                //_forwardCache
                //rotation.Value = math.mul(rotation.Value, r2);

               // var rotMat = math.mul(r2, r1);
                //rotation.Value = math.mul(rotation.Value, rotMat);
//

                //Quaternion.LookRotation()
                

      
                //movement controller
                
                var rotMat3 = new float3x3(rotation.Value);

              
                float3 forwardVectorRelative = math.mul(rotMat3, forwardVectorAbs);
                //float3 rightVectorRelative = math.mul(rotMat3, rightVectorAbsolute);
                float3 noVerticalMovement = new float3(1,0,1);
                
                
                Debug.DrawLine(translation.Value, translation.Value + forwardVectorRelative, Color.blue);

                velocity.Linear += forwardVectorRelative   * noVerticalMovement * sensitivity.Linear.y * inputLinear.y * t;
                velocity.Linear += rightVectorRelative * noVerticalMovement * sensitivity.Linear.x * inputLinear.x * t;
       


            });
            
            _previousMousePosition = currentMousePosition;
            
            //move
        }
        
        
    }
}