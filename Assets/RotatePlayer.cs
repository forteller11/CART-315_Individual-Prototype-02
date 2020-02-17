using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private PlayerControls _input;
    public float2 AngularSensitivty = new float2(5, 5);
    public float2 LinearSensitivty = new float2(5,5);
    public float2 _rotationEuler;
    
     void Awake() => _input = new PlayerControls();
    protected void OnEnable() => _input.Enable();
    protected void OnDisable() =>_input.Disable();
    
    void Update()
    {
        float2 inputAngular = (float2) _input.PlayerMovement.Rotate.ReadValue<Vector2>() * AngularSensitivty;
//        float2 inputLinear = (float2) _input.PlayerMovement.Rotate.ReadValue<Vector2>() * LinearSensitivty;
        
        _rotationEuler += inputAngular * Time.deltaTime;
        transform.rotation = quaternion.identity;
        transform.Rotate(transform.up,_rotationEuler.x,Space.World);
        transform.Rotate(transform.right,-_rotationEuler.y,Space.World);
        
//        transform.position += transform.forward * LinearSensitivty.y * Time.deltaTime * inputLinear.y;
//        transform.position += transform.right * LinearSensitivty.x * Time.deltaTime * inputLinear.x;

    }
}
