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


		}// if
	}// Update

	public void setSelected(GameObject g){
		// set the object as selected
		selected = g;
		// focus on selected object
		FocusSelected();
	}// setSelected

	public void FocusSelected(){
		// getting a handle on the LookAt script
		LookAt lookAt = (LookAt)FindObjectOfType(typeof(LookAt));
		// passing the selected objects transform to the LookAt script
		lookAt.Focus(selected.transform.position);
		
		// Need the camera to be angled down at the object not angled up

	}// focusSelected

	public void CreateMode() {
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
