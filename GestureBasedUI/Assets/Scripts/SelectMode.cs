using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class SelectMode : MonoBehaviour {
    public GameObject myo = null;// Myo game object to connect with
	private bool locked = true;// lock
	public bool Locked {
		get { return locked; }
		set { locked = value; }
	}// lock accesssor
	private bool gyroReset = true;// gyro reset control
	private float baseGyroY;// Y Gyro value placeholder
	private GameObject selected;// selected gameobject
	private int selectedIndex;// index of selected
	private enum Axis { X, Y, Z }// Axis enum
	Axis current = Axis.X;// intialize the enum
	private Pose lastPose;// Pose object to track last pose
	public Canvas gameUI;// Canvas object

	void Update(){
		// if the class is not locked out
		if(!locked){
			// Access the ThalmicMyo component attached to the Myo object.
        	ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

			// if the fist pose is detected and was the last pose
			if(thalmicMyo.pose == Pose.Fist && thalmicMyo.pose == lastPose) {
				// pass the myo and the gyroReset to the movement calculation method
				CalculateMovement(thalmicMyo, gyroReset);
			} else if(thalmicMyo.pose == Pose.WaveIn && thalmicMyo.pose != lastPose) {
				// move down through the enum 
				cycleEnum(0);
			} else if(thalmicMyo.pose == Pose.WaveOut && thalmicMyo.pose != lastPose) {
				// move up through the enum
				cycleEnum(1);
			} else if(thalmicMyo.pose == Pose.FingersSpread && thalmicMyo.pose != lastPose) {
				// ask the user if the would like to exit
				gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("Repeat Finger-Spread gesture to exit Select Mode.");
			} else if(lastPose == Pose.FingersSpread) {
				// exit Select Mode
				CreateMode();
			} else if(thalmicMyo.pose == Pose.DoubleTap && thalmicMyo.pose != lastPose) {
				// ask the user if the would like to delete the object
				gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("Repeat Double-Tap gesture to Delete selected object.");
			} else if(lastPose == Pose.DoubleTap) {
				DeleteSelected();
			}// if/else if

			if(thalmicMyo.pose != Pose.Fist) gyroReset = true;// reset the gyro control

			// update the last pose detected
			lastPose = thalmicMyo.pose;
		}// if
	}// Update

	public void SetSelected(GameObject g, int index){
		// enable the axis text UI
		gameUI.gameObject.GetComponent<UpdateGameUI>().ToggleAxisText(true);
		// set the object as selected
		selected = g;
		// set the selected index
		selectedIndex = index;
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
		if(selectedRB.isKinematic) selectedRB.isKinematic = false;
		else selectedRB.isKinematic = true;
	}// ToggleSelectedRigidbody

	public void CalculateMovement(ThalmicMyo tm, bool gyroReset) {
		float movementDegree = 0;
		// get the current gyro Y value
		if(gyroReset){
			// set the current gyro y value
			baseGyroY = tm.gyroscope.y;
			// set the control to false
			gyroReset = false;
		}// if 
		
		// if there has been no change to the gyro zero the degree of movement, otherwise
		if(baseGyroY - tm.gyroscope.y == 0.000000) movementDegree = 0;// zero the movement degree
		else movementDegree = (tm.gyroscope.y - baseGyroY) / 150;// accessing the gyro y value /150	

		// set the new translate vector
		Vector3 acc = new Vector3(movementDegree, 0, movementDegree);
		// speed control
		float speed = 0.8f;
		// translate the position of the selected object
		if(current == Axis.X) selected.transform.Translate(acc.x * speed, 0, 0);
		else if(current == Axis.Y) selected.transform.Translate(0, acc.x * speed, 0);
		else if(current == Axis.Z) selected.transform.Translate(0, 0, acc.x * speed);
	}// MoveObject

	public void cycleEnum(int direction) {
		// 1 for cycle up and 0 for cycle down
		if(direction == 1){
			if(current == Axis.X) current = Axis.Y;
			else if(current == Axis.Y) current = Axis.Z;
			else if(current == Axis.Z) current = Axis.X;
		} else {
			if(current == Axis.X) current = Axis.Z;
			else if(current == Axis.Y) current = Axis.X;
			else if(current == Axis.Z) current = Axis.Y;
		}// if/else
		// update the axis text
		gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateAxisText(current.ToString());		
	}// cycleEnum

	public void DeleteSelected(){
		// get a handle on the scene state instance
		SceneState ss = SceneState.getInstance;
		// remove the selected gameobject
		ss.RemoveGameObject(selectedIndex);
		// exit Select Mode
		CreateMode();
	}// DeleteSelected

	public void CreateMode() {
		// disable the axis text UI
		gameUI.gameObject.GetComponent<UpdateGameUI>().ToggleAxisText(false);
		// enable the rigidbody component
		ToggleSelectedRigidbody();
		// gets a handle on the singleton instance
		Modes modes = Modes.getInstance;
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