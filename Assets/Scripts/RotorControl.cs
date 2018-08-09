using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorControl : MonoBehaviour {

    /// <summary>
    /// This script is attached to every rotor individually, applying upward force.
    /// RotorControlMaster controls the intensity of the forces applied in order
    /// to make turning/moving behavior
    /// </summary>


    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	

    public void ApplyRotorForce(float forceIntensity)
    {
        //if (Input.GetKey(KeyCode.Space))
        rb.AddForce(gameObject.transform.up * forceIntensity, ForceMode.Force);
        
    }

    //public void ChangeColor(string color) { }  // ** TODO: figure out a good way to do this
}
