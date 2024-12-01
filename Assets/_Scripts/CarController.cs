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

    public GameObject frontLeftWheelModel;
    public GameObject frontRightWheelModel;
    public GameObject rearLeftWheelModel;
    public GameObject rearRightWheelModel;
    public float brakeForce;



    public float motorForce;
    public float steeringAngle;

    private PlayerControls controls;
    private float steeringInput;
    private float steeringBackInput;
    private float driveInput;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Steering.performed += ctx => steeringInput = ctx.ReadValue<float>();
        controls.Gameplay.Steering.canceled += ctx => steeringInput = 0;
        controls.Gameplay.SteeringBack.performed += ctx => steeringBackInput = ctx.ReadValue<float>();
        controls.Gameplay.SteeringBack.canceled += ctx => steeringBackInput = 0;
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
        UpdateWheelPoses();
    }

    private void ApplySteering()
    {
        float currentSteeringAngle = steeringInput * steeringAngle;
        frontLeftWheel.steerAngle = currentSteeringAngle;
        frontRightWheel.steerAngle = currentSteeringAngle;
        float currentSteeringBackAngle = steeringBackInput * steeringAngle;
        rearLeftWheel.steerAngle = currentSteeringBackAngle;
        rearRightWheel.steerAngle = currentSteeringBackAngle;
    }

    private void ApplyMotorForce()
    {
        float currentMotorForce = - driveInput * motorForce;
        // Check if the car's rpm and the drive input have opposite signs

        if (Mathf.Abs(frontRightWheel.rpm) > 0.1f && Mathf.Sign(frontRightWheel.rpm) == Mathf.Sign(driveInput))
        {
            ApplyBrakeForce(brakeForce);
        }
        else
        {
            ReleaseBrakes();
            rearLeftWheel.motorTorque = currentMotorForce;
            rearRightWheel.motorTorque = currentMotorForce;
            frontLeftWheel.motorTorque = currentMotorForce;
            frontRightWheel.motorTorque = currentMotorForce;
        }
    }
    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheel, frontLeftWheelModel);
        UpdateWheelPose(frontRightWheel, frontRightWheelModel);
        UpdateWheelPose(rearLeftWheel, rearLeftWheelModel);
        UpdateWheelPose(rearRightWheel, rearRightWheelModel);
    }

    private void UpdateWheelPose(WheelCollider collider, GameObject wheelModel)
    {
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        collider.GetWorldPose(out position, out rotation);
        
        wheelModel.transform.position = position;
        wheelModel.transform.rotation = rotation;
    }
    private void ApplyBrakeForce(float force)
    {
        frontLeftWheel.brakeTorque = force;
        frontRightWheel.brakeTorque = force;
        rearLeftWheel.brakeTorque = force;
        rearRightWheel.brakeTorque = force;
    }

    private void ReleaseBrakes()
    {
        frontLeftWheel.brakeTorque = 0;
        frontRightWheel.brakeTorque = 0;
        rearLeftWheel.brakeTorque = 0;
        rearRightWheel.brakeTorque = 0;
    }



}
