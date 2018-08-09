using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    Transform target;

    Vector3 offsetVector;

    Quaternion rotationToApply;

    float zOffset;
    float yOffset;

    float desiredAngle;

	void Start () {
        // start by making a vector representing the camera's position
        // relative to the target (X = 0 because the camera should always be centered)
        zOffset =  target.position.z - transform.position.z;
        yOffset = target.position.y - transform.position.y;
        offsetVector = new Vector3(0, yOffset, zOffset);
    }
	
	void LateUpdate () {
        // make a quaternion of the rotation to apply to the camera's offset
        desiredAngle = target.transform.eulerAngles.y;
        rotationToApply = Quaternion.Euler(0, desiredAngle, 0);

        // rotate the offset vector by the target's rotation, then move the camera
        transform.position = target.position - (rotationToApply * offsetVector);
        transform.LookAt(target); // and point it at the target
    }
}
