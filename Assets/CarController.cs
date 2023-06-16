using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public float motorForce;
    public float steeringAngle;

    private PlayerControls controls;
    private float steeringInput;
    private float driveInput;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Steering.performed += ctx => steeringInput = ctx.ReadValue<float>();
        controls.Gameplay.Steering.canceled += ctx => steeringInput = 0;
        controls.Gameplay.Jump.performed += ctx => Debug.Log("Jump");
        controls.Gameplay.Drive.performed += ctx => driveInput = ctx.ReadValue<float>();
        controls.Gameplay.Drive.canceled += ctx => driveInput = 0;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void FixedUpdate()
    {
        ApplySteering();
        ApplyMotorForce();
    }

    private void ApplySteering()
    {
        float currentSteeringAngle = steeringInput * steeringAngle;
        Debug.Log(currentSteeringAngle);
        frontLeftWheel.steerAngle = currentSteeringAngle;
        frontRightWheel.steerAngle = currentSteeringAngle;
    }

    private void ApplyMotorForce()
    {
        float currentMotorForce = driveInput * motorForce;
        Debug.Log(currentMotorForce);
        rearLeftWheel.motorTorque = currentMotorForce;
        rearRightWheel.motorTorque = currentMotorForce;
    }
}
