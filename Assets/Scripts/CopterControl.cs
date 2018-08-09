using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopterControl : MonoBehaviour {

    /// <summary>
    /// This Script handles input from the controller and uses functions from RotorControlMaster to
    /// move the copter
    /// </summary>

    float leftJoyHoriz, leftJoyVert, rightJoyHoriz, rightJoyVert,
        dPadHoriz, dPadVert;

    //[SerializeField]
    //RotorControlMaster rotorCtrlMaster;


    bool L2TriggerAxisInUse = false, R2TriggerAxisInUse = false;

    public static CopterControl instance;

    // Use this for initialization
    void Awake() {
        instance = this;
    }

    private void FixedUpdate()
    {
        //rotorCtrlMaster.ControlRotorForces(leftJoyVert, rightJoyHoriz,
        // rightJoyVert, leftJoyHoriz);
    }

    // Update is called once per frame
    void Update() {

        // *** TODO: Add changing keybinding support at some point ***

        leftJoyHoriz = Input.GetAxisRaw("LeftJoyHoriz");
        leftJoyVert = Input.GetAxis("LeftJoyVert");

        rightJoyHoriz = Input.GetAxis("RightJoyHoriz");
        rightJoyVert = Input.GetAxis("RightJoyVert");




        // TESTING FOR BUTTONS
        if (Input.GetButtonDown("X Button"))
        {
            print("X Button Pressed!");// put functionality here
        }
        if (Input.GetButtonDown("Y Button"))
        {
            print("Y Button Pressed!");// put functionality here
        }
        if (Input.GetButtonDown("A Button"))
        {
            print("A Button Pressed!");// put functionality here
        }
        if (Input.GetButtonDown("B Button"))
        {
            print("B Button Pressed!"); // put functionality here
        }
        if (Input.GetButtonDown("Start Button"))
        {
            print("Start Button Pressed!"); // put functionality here
        }
        if (Input.GetButtonDown("Select Button"))
        {
            print("Select Button Pressed!"); // put functionality here
        }

        // TESTING FOR TRIGGERS
        if (Input.GetButtonDown("L1 Trigger"))
        {
            print("L1 Trigger Pressed!");
        }
        if (Input.GetAxisRaw("L2 Trigger") != 0)
        {
            // L2 and R2 Triggers work like joystick axes, so we need to make them work like buttons
            if (!L2TriggerAxisInUse)
            {
                print("L2 Trigger Pressed!"); // put functionality here
                L2TriggerAxisInUse = true;
            }

        }
        if (Input.GetButtonDown("R1 Trigger"))
        {
            print("R1 Trigger Pressed!");
        }
        if (Input.GetAxisRaw("R2 Trigger") != 0)
        {
            // L2 and R2 Triggers work like joystick axes, so we need to make them work like buttons
            if (!R2TriggerAxisInUse)
            {
                print("R2 Trigger Pressed!"); // put functionality here
                R2TriggerAxisInUse = true;
            }
        }


        if (Input.GetAxisRaw("L2 Trigger") == 0) { L2TriggerAxisInUse = false; }
        if (Input.GetAxisRaw("R2 Trigger") == 0) { R2TriggerAxisInUse = false; }
    }

    public float RightJoyHoriz{ get { return rightJoyHoriz; } }
    public float RightJoyVert { get { return rightJoyVert; } }
    public float LeftJoyHoriz { get { return leftJoyHoriz; } }
    public float LeftJoyVert { get { return leftJoyVert; } }
}
