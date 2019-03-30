using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class SelectMode : MonoBehaviour {
	// Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;
	// A rotation that compensates for the Myo armband's orientation parallel to the ground, i.e. yaw.
    // Once set, the direction the Myo armband is facing becomes "forward" within the program.
    // Set by making the fingers spread pose or pressing "r".
    private Quaternion _antiYaw = Quaternion.identity;
	// A reference angle representing how the armband is rotated about the wearer's arm, i.e. roll.
    // Set by making the fingers spread pose or pressing "r".
    private float _referenceRoll = 0.0f;

    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

	public static Modes modes;
	// lock
	private bool locked = true;
	// selected gameobject placeholder
	private GameObject selected;

	public bool Locked {
		get { return locked; }
		set { locked = value; }
	}// lock accesssor

	void Update(){
		// if the class is not locked out
		if(!locked){
			Debug.Log("SelectMode Unlocked");
			// Access the ThalmicMyo component attached to the Myo object.
        	ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

			if(thalmicMyo.pose == Pose.Fist) {
				/*
				// testing
				float x = thalmicMyo.transform.position.x;
				float y = thalmicMyo.transform.position.y; 
				float z = thalmicMyo.transform.position.z;
				*/

				// use acc to move
				// float x = thalmicMyo.accelerometer.x / 10;
				// float y = thalmicMyo.accelerometer.y / 10;
				// float z = thalmicMyo.accelerometer.z / 10;
				
				float x = thalmicMyo.gyroscope.x /100;
				float y = thalmicMyo.gyroscope.y /100;
				float z = thalmicMyo.gyroscope.z /100;

				// print out		
				Debug.Log("X: " + x + ", Y: " + y + ", Z: " + z);

				Vector3 acc = new Vector3(x, y, z);
				float speed = 1f;
         		selected.transform.Translate(acc.x * speed, 0, acc.y * speed);
			}// if
			
		}// if
	}// Update

	public void setSelected(GameObject g){
		// set the object as selected
		selected = g;
		// disable the rigidbody component
		ToggleSelectedRigidbody();
		// focus on selected object
		FocusSelected();
	}// setSelected

	public void FocusSelected(){
		// getting a handle on the LookAt script
		LookAt lookAt = (LookAt)FindObjectOfType(typeof(LookAt));
		// passing the selected objects transform to the LookAt script
		lookAt.Focus(selected.transform.position);
	}// focusSelected

	public void ToggleSelectedRigidbody() {
		// get a handle on the rigidbody component
		Rigidbody selectedRB = selected.GetComponent<Rigidbody>();
		
		// toggle the rigidbody component
		if(selectedRB.isKinematic)
			selectedRB.isKinematic = false;
		else
			selectedRB.isKinematic = true;
	}// ToggleSelectedRigidbody

	
	private Vector3 motion;
	// adapted from: https://stackoverflow.com/questions/18836484/convert-yaw-pitch-and-roll-to-x-y-z-vector-in-world-coordinates
	public void CalculateMovement(){
		
	}// MoveObject

	public void CreateMode() {
		// enable the rigidbody component
		ToggleSelectedRigidbody();
		// gets a handle on the singleton instance
		modes = Modes.getInstance;
		// change the mode to exit
		modes.mode = Modes.Mode.Create;
		// open the lock on the runner
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// close the lock here
		this.locked = true;
	}// Exit

		
	// Compute the angle of rotation clockwise about the forward axis relative to the provided zero roll direction.
	// As the armband is rotated about the forward axis this value will change, regardless of which way the
	// forward vector of the Myo is pointing. The returned value will be between -180 and 180 degrees.
	float rollFromZero (Vector3 zeroRoll, Vector3 forward, Vector3 up)
	{
		// The cosine of the angle between the up vector and the zero roll vector. Since both are
		// orthogonal to the forward vector, this tells us how far the Myo has been turned around the
		// forward axis relative to the zero roll vector, but we need to determine separately whether the
		// Myo has been rolled clockwise or counterclockwise.
		float cosine = Vector3.Dot (up, zeroRoll);

		// To determine the sign of the roll, we take the cross product of the up vector and the zero
		// roll vector. This cross product will either be the same or opposite direction as the forward
		// vector depending on whether up is clockwise or counter-clockwise from zero roll.
		// Thus the sign of the dot product of forward and it yields the sign of our roll value.
		Vector3 cp = Vector3.Cross (up, zeroRoll);
		float directionCosine = Vector3.Dot (forward, cp);
		float sign = directionCosine < 0.0f ? 1.0f : -1.0f;

		// Return the angle of roll (in degrees) from the cosine and the sign.
		return sign * Mathf.Rad2Deg * Mathf.Acos (cosine);
	}

	// Compute a vector that points perpendicular to the forward direction,
	// minimizing angular distance from world up (positive Y axis).
	// This represents the direction of no rotation about its forward axis.
	Vector3 computeZeroRollVector (Vector3 forward)
	{
		Vector3 antigravity = Vector3.up;
		Vector3 m = Vector3.Cross (myo.transform.forward, antigravity);
		Vector3 roll = Vector3.Cross (m, myo.transform.forward);

		return roll.normalized;
	}

	// Adjust the provided angle to be within a -180 to 180.
	float normalizeAngle (float angle)
	{
		if (angle > 180.0f) {
			return angle - 360.0f;
		}
		if (angle < -180.0f) {
			return angle + 360.0f;
		}
		return angle;
	}


}// SelectMode
