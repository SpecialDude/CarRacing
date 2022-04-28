using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private bool isBreaking;
    private float currentBreakForce;
    private float cuurentSteeringAngle;

    [SerializeField]
    private float motorForce;
    [SerializeField]
    private float breakForce;
    [SerializeField]
    private float maxSteeringAngle;

    [SerializeField]
    private WheelCollider frontLeftWheelCollider;
    [SerializeField]
    private WheelCollider frontRightWheelCollider;
    [SerializeField]
    private WheelCollider rearLeftWheelCollider;
    [SerializeField]
    private WheelCollider rearRightWheelCollider;

    [SerializeField]
    private Transform frontLeftWheelTransform;
    [SerializeField]
    private Transform frontRightWheelTranform;
    [SerializeField]
    private Transform rearLeftWheelTransform;
    [SerializeField]
    private Transform rearRightWheelTransform;

    private int lastCheckpoint = -1;

    public int LastCheckpoint
    {
        get
        {
            return lastCheckpoint;
        }
        set
        {
            lastCheckpoint = value;
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTranform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose( out pos, out rot);

        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void HandleSteering()
    {
        cuurentSteeringAngle = maxSteeringAngle * horizontalInput;

        frontLeftWheelCollider.steerAngle = cuurentSteeringAngle;
        frontRightWheelCollider.steerAngle = cuurentSteeringAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;

        if (isBreaking) ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }
}
