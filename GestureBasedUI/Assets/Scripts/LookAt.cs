using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
    public static Modes modes;
    public float heightChange;
    public float distanceChange;

    public Transform targetPosition;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    private Rigidbody rigidbody;
    float x = 0.0f;
    float y = 0.0f;

	public void Focus(GameObject target){
        // gets a handle on the singleton instance
		modes = Modes.getInstance;
        
        // if we are in the create/select mode
        if(modes.mode == Modes.Mode.Create){
            // setting the change factors
            heightChange = 1.5f;
            distanceChange = -15f;
        } else if(modes.mode == Modes.Mode.Select) {
            // setting the change factors
            heightChange = 1.5f;
            distanceChange = -10f;
        }// else if

        // move to the target object
        this.transform.position = new Vector3(target.transform.position.x - distanceChange, target.transform.position.y, target.transform.position.z);
        // move above the object
        this.transform.Translate(this.transform.up * heightChange);
        // look at the object
        transform.LookAt(target.transform);

        // set the target position to rotate around
        targetPosition = target.transform;
        OrbitObject();
    }// Focus

    // adapted from below sources: 
    // http://wiki.unity3d.com/index.php?title=MouseOrbitImproved#Code_C.23
    // https://forum.unity.com/threads/rotate-the-camera-around-the-object.47353/
    public void OrbitObject() {
        // get the change in x value from the mouse x axis
        x += Input.GetAxis("Mouse X") * xSpeed * distanceChange * 0.03f;
        // rotate around the target object
        transform.RotateAround(targetPosition.position, Vector3.up, x);
    }// OrbitObject

}// LookAt
