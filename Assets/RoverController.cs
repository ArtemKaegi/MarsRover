using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RoverController : MonoBehaviour
{
    private float hInput;
    private float vInput;
    private bool isBreaking;
    private float steerAngle;
    private float breakForce;
    public float maxSteeringAngle = 15f;

    [SerializeField] private float horsePower;
    [SerializeField] private float maxRpm;

    private WheelCollider[] WheelColliders;
    [SerializeField] private WheelCollider FRWheelCollider;
    [SerializeField] private WheelCollider FLWheelCollider;
    [SerializeField] private WheelCollider CRWheelCollider;
    [SerializeField] private WheelCollider CLWheelCollider;
    [SerializeField] private WheelCollider BRWheelCollider;
    [SerializeField] private WheelCollider BLWheelCollider;

    [SerializeField] private Transform FRWheel;
    [SerializeField] private Transform FLWheel;
    [SerializeField] private Transform CRWheel;
    [SerializeField] private Transform CLWheel;
    [SerializeField] private Transform BRWheel;
    [SerializeField] private Transform BLWheel;

    [SerializeField] private TrailRenderer FRTrail;
    [SerializeField] private TrailRenderer FLTrail;
    [SerializeField] private TrailRenderer CRTrail;
    [SerializeField] private TrailRenderer CLTrail;
    [SerializeField] private TrailRenderer BRTrail;
    [SerializeField] private TrailRenderer BLTrail;


    void Start()
    {
        GetInput();
        WheelColliders = new[]
            {FRWheelCollider, FLWheelCollider, CRWheelCollider, CLWheelCollider, BRWheelCollider, BLWheelCollider};
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * hInput;
        FLWheelCollider.steerAngle = steerAngle;
        FRWheelCollider.steerAngle = steerAngle;
        BLWheelCollider.steerAngle = -steerAngle;
        BRWheelCollider.steerAngle = -steerAngle;
    }

    private void HandleMotor()
    {
        foreach (var wheel in WheelColliders)
        {
            if (wheel.rpm < maxRpm && vInput > 0)
            {
                wheel.motorTorque = horsePower * vInput;
            } 
            else if (wheel.rpm > -maxRpm && vInput < 0)
            {
                wheel.motorTorque = horsePower * vInput;
            }
            else
            {
                wheel.motorTorque = 0;
            }
        }
        breakForce = isBreaking ? 3000f : 0f;
        FRWheelCollider.brakeTorque = breakForce;
        FLWheelCollider.brakeTorque = breakForce;
        BRWheelCollider.brakeTorque = breakForce;
        BLWheelCollider.brakeTorque = breakForce;
    }
    
    private void UpdateWheels()
    {
        UpdateWheelPos(FRWheelCollider, FRWheel);
        UpdateWheelPos(FLWheelCollider, FLWheel);
        UpdateWheelPos(CLWheelCollider, CLWheel);
        UpdateWheelPos(CRWheelCollider, CRWheel);
        UpdateWheelPos(BLWheelCollider, BLWheel);
        UpdateWheelPos(BRWheelCollider, BRWheel);
        checkTrail(FRWheelCollider, FRTrail);
        checkTrail(FLWheelCollider, FLTrail);
        checkTrail(CRWheelCollider, CRTrail);
        checkTrail(CLWheelCollider, CLTrail);
        checkTrail(BRWheelCollider, BRTrail);
        checkTrail(BLWheelCollider, BLTrail);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

    private void checkTrail(WheelCollider wheel, TrailRenderer trail)
    {
        if (wheel.isGrounded)
        {
            trail.emitting = true;
        }
        else
        {
            trail.emitting = false;
        }
    }
}