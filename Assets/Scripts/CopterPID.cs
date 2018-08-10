using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopterPID : MonoBehaviour {

    [SerializeField]
    float rotationSpeed, maxPitchAndRollAngleFromInput;

    Rigidbody rb;

    float totalPitchCorrection, totalRollCorrection;
    float pitchError, rollError;
    float wishedPitch, wishedRoll;

    [SerializeField]
    float P = 0.07f, I = 0.07f, D = 0.07f; // these default values work well so far

    float previousPitchError, previousRollError, 
        currentPitchError, currentRollError,
        averagePitchError, averageRollError;

    bool pidInitialized = false;

    Vector3 rotateVector;

    Vector3 correctionVector;

    Vector3 wishedAnglesVector;

    void Start () {
        pidInitialized = false;
        previousPitchError = 0;
        previousRollError = 0;
        currentPitchError = 0;
        currentRollError = 0;
        averagePitchError = 0;
        averageRollError = 0;

        rb = GetComponent<Rigidbody>();

        correctionVector = new Vector3();

        wishedAnglesVector = Vector3.up;
    }
	
	void Update () {
        PitchAndRollControl();
    }

    private void FixedUpdate()
    {
        RotateHorizontal(CopterControl.instance.RightJoyHoriz);

        // to make it more realistic, only balance the copter if the rotors are engaged
        if (CopterControl.instance.RightJoyVert > 0.1f ||
            CopterControl.instance.RightJoyVert < -0.1f)
        {
            BalanceCopter();
        }
    }

    /// <summary>
    /// Allows the copter to rotate around its Y Axis (changes the yaw)
    /// </summary>
    /// <param name="rotationAxisInput">Input given from the player (the joystick on the controller)</param>
    private void RotateHorizontal(float rotationAxisInput)
    {
        if (rotationAxisInput != 0)
        {
            rotateVector.y = rotationAxisInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(rotateVector);
        }
        else
        {
            rotateVector.y = 0;
        }
    }

    /// <summary>
    /// Takes player input from the controller joystick and gives values to 
    /// wishedPitch and wishedRoll
    /// </summary>
    private void PitchAndRollControl()
    {
        wishedPitch = -CopterControl.instance.LeftJoyVert * maxPitchAndRollAngleFromInput; // must be negative for non-inverted control
        wishedRoll = CopterControl.instance.LeftJoyHoriz * maxPitchAndRollAngleFromInput;
    }

    /// <summary>
    /// Uses PID correction to keep the copter level, changes its pitch and roll (wishedPitch/wishedRoll)
    /// according to player input (from the controller joystick)
    /// </summary>
    private void BalanceCopter()
    {
        // calculate the direction vector of the player's wished pitch/roll (direction of "level")
        wishedAnglesVector = transform.TransformDirection(Quaternion.Euler(wishedPitch, 0, wishedRoll) * (Vector3.up));

        // calculate error by taking the angle between the Z and X axes and the wished angle
        pitchError = Vector3.Angle(Vector3.forward, wishedAnglesVector) - 90;
        rollError = Vector3.Angle(Vector3.right, wishedAnglesVector) - 90;

        /* //*** Experimental Zone ***
        Vector3 relativeForward = Quaternion.FromToRotation(Vector3.left, -transform.right) * Vector3.forward;
        Vector3 relativeRight = Quaternion.FromToRotation(Vector3.forward, transform.forward) * Vector3.right;

        pitchError = Vector3.Angle(relativeForward, wishedAnglesVector) - 90;
        rollError = Vector3.Angle(relativeRight, wishedAnglesVector) - 90;

        Debug.Log("PitchError: " + pitchError);*/

        // get values ready for PID calculations
        if (!pidInitialized)
        {
            previousPitchError = pitchError;
            previousRollError = rollError;
            pidInitialized = true;
        }
        averagePitchError = (previousPitchError + pitchError) / 2;
        averageRollError = (previousRollError + rollError) / 2;

        // calculating total corrections using PID
        totalPitchCorrection = (pitchError * P) + (averagePitchError * I) + ((pitchError - previousPitchError) * D);
        totalRollCorrection = (rollError * P) + (averageRollError * I) + ((rollError - previousRollError) * D);
        
        // getting previous values ready for next loop
        previousPitchError = pitchError;
        previousRollError = rollError;

        // perform rotation on the quadcopter to correct toward "level"
        correctionVector.x = totalPitchCorrection;
        correctionVector.z = -totalRollCorrection; // must be negative to balance correctly
        transform.Rotate(correctionVector, Space.Self);

        // used to smoothely dampen rotational inertia
        rb.angularVelocity *= 0.5f; 
    }

    
}
