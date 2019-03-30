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
	// gyro reset control
	private bool gyroReset = true;
	// x, y and z gyro values
	private float baseGyroX;
	private float baseGyroY;
	private float baseGyroZ;
	// selected gameobject
	private GameObject selected;

	private enum Axis { X, Y, Z }
	// intialize the enum
	Axis current = Axis.X;

	private Pose lastPose;

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

			// if the fist pose is detected and was the last pose
			if(thalmicMyo.pose == Pose.Fist && thalmicMyo.pose == lastPose) {
				float movementDegree = 0;

				// get the current values
				if(gyroReset){
					// set the current gyro y value
					baseGyroY = thalmicMyo.gyroscope.y;
					// set the control to false
					gyroReset = false;
					// Debug.Log("Gyro has been reset");
				}// if 
				
				// if there has been no change to the gyro
				if(baseGyroY - thalmicMyo.gyroscope.y == 0.000000) {
						// zero the movement degree
						movementDegree = 0;
						// Debug.Log("Zero out of data");
				} else {
					// accessing the gyro y value
					movementDegree = (thalmicMyo.gyroscope.y - baseGyroY) / 150;	
					// Debug.Log("Movement Degree: " + movementDegree);
				}// else if
				
				// pass to the movement calculation method
				CalculateMovement(movementDegree);
			} else if(thalmicMyo.pose == Pose.WaveIn && thalmicMyo.pose != lastPose) {
				// move down through the enum 
				cycleEnum(0);
				
				Debug.Log(current);

				// reset the gyro control
				gyroReset = true;
			} else if(thalmicMyo.pose == Pose.WaveOut && thalmicMyo.pose != lastPose) {
				// move up through the enum
				cycleEnum(1);

				Debug.Log(current);
				
				// reset the gyro control
				gyroReset = true;
			} else {
				// reset the gyro control
				gyroReset = true;
			}// if/else if/else

			// update the last pose detected
			lastPose = thalmicMyo.pose;
		}// if
	}// Update

	public void SetSelected(GameObject g){
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

	public void CalculateMovement(float movementDegree){
		// set the new translate vector
		Vector3 acc = new Vector3(movementDegree, 0, movementDegree);
		// speed control
		float speed = 0.8f;
		// translate the position of the selected object
		if(current == Axis.X)
        	selected.transform.Translate(acc.x * speed, 0, 0);
		else if(current == Axis.Y)
			selected.transform.Translate(0, acc.x * speed, 0);
		else if(current == Axis.Z)
			selected.transform.Translate(0, 0, acc.x * speed);
	}// MoveObject

	public void cycleEnum(int direction) {
		// 1 for cycle up and 0 for cycle down
		if(direction == 1){
			if(current == Axis.X)
				current = Axis.Y;
			else if(current == Axis.Y)
				current = Axis.Z;
			else if(current == Axis.Z)
				current = Axis.X;
		} else {
			if(current == Axis.X)
				current = Axis.Z;
			else if(current == Axis.Y)
				current = Axis.X;
			else if(current == Axis.Z)
				current = Axis.Y;
		}// if/else
	}// cycleEnum

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

}// SelectMode

// ref from: https://stackoverflow.com/questions/18836484/convert-yaw-pitch-and-roll-to-x-y-z-vector-in-world-coordinates