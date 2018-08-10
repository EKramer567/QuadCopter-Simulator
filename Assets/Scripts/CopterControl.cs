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
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) { leftJoyVert = Input.GetAxisRaw("W/S"); Debug.Log("W/S Pressed: " + leftJoyVert); }
        else
        {
            leftJoyVert = Input.GetAxis("LeftJoyVert");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) { leftJoyHoriz = Input.GetAxisRaw("A/D"); Debug.Log("A/D Pressed: " + leftJoyHoriz); }
        else
        {
            leftJoyHoriz = Input.GetAxisRaw("LeftJoyHoriz");
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) { rightJoyVert = Input.GetAxisRaw("Up/Down"); Debug.Log("U/D Pressed: " + rightJoyVert); }
        else
        {
            rightJoyVert = Input.GetAxis("RightJoyVert");
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) { rightJoyHoriz = Input.GetAxisRaw("Left/Right"); Debug.Log("L/R Pressed: " + rightJoyHoriz); }
        else
        {
            rightJoyHoriz = Input.GetAxis("RightJoyHoriz");
        }
        
        


        if (Input.GetAxisRaw("L2 Trigger") == 0) { L2TriggerAxisInUse = false; }
        if (Input.GetAxisRaw("R2 Trigger") == 0) { R2TriggerAxisInUse = false; }
    }

    public float RightJoyHoriz{ get { return rightJoyHoriz; } }
    public float RightJoyVert { get { return rightJoyVert; } }
    public float LeftJoyHoriz { get { return leftJoyHoriz; } }
    public float LeftJoyVert { get { return leftJoyVert; } }

}
