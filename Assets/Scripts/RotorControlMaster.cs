using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorControlMaster : MonoBehaviour {

    /// <summary>
    /// This script helps all the rotors work together, taking in all the RotorControl script objects
    /// from the rotors and making them apply force as needed
    /// </summary>
    [SerializeField]
    RotorControl frontLeftRotor, rearLeftRotor, frontRightRotor, rearRightRotor;

    [SerializeField]
    float baseRotorForceIntensity, baseHorizIntensity, climbModifier = 1;

    const float CLIMB_MOD_MAX = 1.2f;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        AmplifyClimb();
        ApplyRotorForce(-CopterControl.instance.RightJoyVert);
        //HorizontalMovement();
    }

    /// <summary>
    /// Amplifies the power of the engines when under pitch or roll
    /// To allow the copter to more realistically gain altitude while doing so
    /// (Without this, the copter can't gain altitude while under pitch/roll)
    /// </summary>
    void AmplifyClimb()
    {
        if (CopterControl.instance.LeftJoyVert > 0.1f || CopterControl.instance.LeftJoyVert < -0.1f)
        {
            climbModifier = CopterControl.instance.LeftJoyVert * CLIMB_MOD_MAX;
        }

        else if (CopterControl.instance.LeftJoyHoriz > 0.1f || CopterControl.instance.LeftJoyHoriz < -0.1f)
        {
            climbModifier = CopterControl.instance.LeftJoyHoriz * CLIMB_MOD_MAX;
        }

        climbModifier = Mathf.Clamp(climbModifier, 1, CLIMB_MOD_MAX);
    }

    /// <summary>
    /// Fires up all the rotors at once, with the same intensity
    /// </summary>
    /// <param name="baseIntensityInput"></param>
    private void ApplyRotorForce(float baseIntensityInput)
    {
        if (baseIntensityInput > 0) // if left stick vertical stick is turned up, not down or 0
        {
            frontLeftRotor.ApplyRotorForce((baseRotorForceIntensity * climbModifier) * baseIntensityInput);
            frontRightRotor.ApplyRotorForce((baseRotorForceIntensity * climbModifier) * baseIntensityInput);
            rearLeftRotor.ApplyRotorForce((baseRotorForceIntensity * climbModifier) * baseIntensityInput);
            rearRightRotor.ApplyRotorForce((baseRotorForceIntensity * climbModifier) * baseIntensityInput);
        }
    }

}
