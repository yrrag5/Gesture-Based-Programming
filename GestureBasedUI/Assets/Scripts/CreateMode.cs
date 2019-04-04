using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class CreateMode : MonoBehaviour {

	// Global variables.
	public static Modes modes;
	private SceneState sceneState;
	private GameObject selected;
	public int arrayLength;
	public int arrayIndex;
	public Material shapecolor;
	public Material highlight;
	public Canvas gameUI;


	// Myo variables.
	public GameObject myo = null;
	private Pose lastPose = Pose.Unknown;

	// Lock.
	private bool locked = true;

	public bool Locked {
		get { return locked; }
		set { locked = value; }
	}// Lock accesssor.

	void Start() {
		sceneState = (SceneState)FindObjectOfType(typeof(SceneState));

		// Get the length of the array.
		arrayLength = sceneState.ArrayLength();
		
		if (arrayLength != 0) {
			Debug.Log("length: " + arrayLength);

			// Set selected shape to last object in GameObjects array.
			selected = sceneState.getObject(arrayLength - 1);
			// Set array index to last object in array.
			arrayIndex = arrayLength - 1;
		}
		else {
			Debug.Log("Object array is empty");
		}
	}

	void Update() {
		// If the class is not locked out.
		if(!locked) {
			Debug.Log("CreateMode Unlocked");

			// Detect Myo gestures (left, right, close, exit).
			// Access the ThalmicMyo component attached to the Myo object.
       		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

			// Update references when the pose becomes fingers spread or the q key is pressed.
			// if pose == whatever && != lastpose
 			if(thalmicMyo.pose == Pose.FingersSpread && thalmicMyo.pose != lastPose) {
				// ask the user if the would like to exit
	//			gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("Repeat Finger-Spread gesture to exit to Menu.");
			}
			else if (lastPose == Pose.FingersSpread) {
				Debug.Log("create mode - Finger Spread");
				MenuMode();
			}
			else if (thalmicMyo.pose == Pose.WaveIn && thalmicMyo.pose != lastPose) {
				Debug.Log("create mode - Wave in");
				ParseLeft(arrayIndex);
			}
			else if (thalmicMyo.pose == Pose.WaveOut && thalmicMyo.pose != lastPose) {
				Debug.Log("create mode - Wave out");
				ParseRight(arrayIndex);
			}
			else if (thalmicMyo.pose == Pose.DoubleTap && thalmicMyo.pose != lastPose) {
				Debug.Log("create mode - Double Tap");
				SelectMode();
			}

			lastPose = thalmicMyo.pose;

		}// if

	}// Update

	// ----- MODE FUNCTIONALITY -----
	void ParseLeft (int arrayIndex) {
		// Get rid of highlight on current GameObject.
		ToggleHighlight();
		// Select GameObject by decrementing index of array by 1.
		// if index == 0, select the last GameObject in array.
		if (arrayIndex == 0) {
			arrayIndex = arrayLength - 1;
			selected = sceneState.getObject(arrayIndex);
		}
		else {
			arrayIndex -= 1;
			selected = sceneState.getObject(arrayIndex);
		}

		// Focus camera on object.
		FocusSelected();
		// Highlight the newly selected shape.
		ToggleHighlight();		
	}

	void ParseRight (int arrayIndex) {
		// Get rid of highlight on current GameObject.
		ToggleHighlight();

		// Select GameObject by incrementing index of array by 1.
		// if index == array.length - 1, select the first GameObject in array.
		if (arrayIndex == arrayLength - 1) {
			arrayIndex = 0;
			selected = sceneState.getObject(arrayIndex);
		}
		else {
			arrayIndex += 1;
			selected = sceneState.getObject(arrayIndex);
		}

		// Focus camera on object.
		FocusSelected();
		// Highlight the newly selected shape.
		ToggleHighlight();
	}

	// Focus camera on selected object.
	public void FocusSelected(){
		// getting a handle on the LookAt script
		LookAt lookAt = (LookAt)FindObjectOfType(typeof(LookAt));
		// passing the selected objects transform to the LookAt script
		lookAt.Focus(selected.transform.position);
	}

	void ToggleHighlight() {
		// If material is highlighted.
		if (selected.GetComponent<Renderer>().material == highlight)
			selected.GetComponent<Renderer>().material = shapecolor;
		else { // otherwise, store the material and then change to highlighted.
			shapecolor = selected.GetComponent<Renderer>().material;
			selected.GetComponent<Renderer>().material = highlight;
		}
	}

	// ----- MODE CHANGING METHODS -----
	public void MenuMode() {
		// Get rid of highlight on current GameObject.
		ToggleHighlight();
		// Change back to menu mode.

		// Gets a handle on the singleton instance.
		modes = Modes.getInstance;
		// Change the mode.
		modes.mode = Modes.Mode.Menu;
		// Open the lock on the runner.
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// Close the lock here.
		this.locked = true;
	}// CreateMode

	public void SelectMode() {
		// Pass selected game object to SelectMode script.
	 	SelectMode sm = GameObject.FindObjectOfType<SelectMode>();
	  	sm.setSelected(selected);

		// Gets a handle on the singleton instance.
		modes = Modes.getInstance;
		// Change the mode.
		modes.mode = Modes.Mode.Select;
		// Open the lock on the runner.
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// Close the lock.
		this.locked = true;
	}// Exit
	
}// CreateMode
