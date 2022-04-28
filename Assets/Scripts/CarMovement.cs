using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider frontRight;    
    [SerializeField] WheelCollider backLeft;
    [SerializeField] WheelCollider backRight;

    [SerializeField] Transform frontLeftW;
    [SerializeField] Transform frontRightW;
    [SerializeField] Transform backLeftW;
    [SerializeField] Transform backRightW;

    [SerializeField] Transform steering;

    public float acceleration = 500f;
    public float breakingForce = 300f;
    public float maxSteeringAngle = 30f;

    private float currentAcceleration = 0f;
    private float currentBreakForce = 0f;
    private float currentSteeringAngle = 0f;

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

        
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce;
        }
        else
        {
            currentBreakForce = 0f;
        }

        ApplyAcceleration();
        ApplyBrake();
        ApplySteering();
    }

    // Apply Acceleration to all wheels
    private void ApplyAcceleration()
    {
        currentAcceleration = -acceleration * Input.GetAxis("Vertical");
        frontLeft.motorTorque = currentAcceleration;
        frontRight.motorTorque = currentAcceleration;
    }

    // Apply Breaking
    private void ApplyBrake()
    {
        frontRight.brakeTorque = currentBreakForce;
        frontLeft.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;
    }

    // Apply Steering
    private void ApplySteering()
    {
        currentSteeringAngle = maxSteeringAngle * Input.GetAxis("Horizontal");

        frontLeft.steerAngle = currentSteeringAngle;
        frontRight.steerAngle = currentSteeringAngle;

        UpdateWheel(frontLeft, frontLeftW);
        UpdateWheel(frontRight, frontRightW);
        UpdateWheel(backLeft, backLeftW);
        UpdateWheel(backRight, backRightW);

        Vector3 position;
        Quaternion rotation;
        
        frontLeft.GetWorldPose(out position, out rotation);

        rotation.eulerAngles = new Vector3(steering.rotation.eulerAngles.x, steering.rotation.eulerAngles.y, rotation.eulerAngles.y);
        steering.rotation = rotation;
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        Vector3 position;
        Quaternion rotation;

        col.GetWorldPose(out position, out rotation);

        trans.position = position;
        trans.rotation = rotation;
    }
    
}
