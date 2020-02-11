using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

public class PlayerMovementMono : MonoBehaviour
{
    float2 _previousMousePosition = float2.zero;

    private PlayerControls _input;

    private void Awake()
    {
        _input = new PlayerControls();
    }

    void Update()
    {
        var inputAngular = _input.PlayerMovement.Rotate.ReadValue<Vector2>();
        float3 angularToAdd = new float3(inputAngular.x, inputAngular.y, 0);
            
        var inputLinear = _input.PlayerMovement.Translate.ReadValue<Vector2>();
        float3 linearVelocityAbsolute = new float3(inputLinear.x, 0, inputLinear.y);
            
        Debug.Log($"--------------------");
        Debug.Log($"linear: {inputLinear}");
        Debug.Log($"angular: {inputAngular}");

            
        //move
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}
