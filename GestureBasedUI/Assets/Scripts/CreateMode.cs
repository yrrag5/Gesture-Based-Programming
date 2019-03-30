using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMode : MonoBehaviour {
	// Global variables.
	public static Modes modes;
	public SceneState sceneState;
	public GameObject selected;
	public int arrayLength;
	public int arrayIndex;

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

			// Set selected shape to last shape in GameObjects array.
			selected = sceneState.getObject(arrayLength - 1);
		}
		else {
			Debug.Log("Object array is empty");
		}
	}

	void Update() {
		// If the class is not locked out.
		if(!locked) {
			Debug.Log("CreateMode Unlocked");
		}// if

		// Detect Myo gestures (left, right, close, exit).

	}// Update

	// ----- MODE FUNCTIONALITY -----
	void ParseLeft (int arrayIndex) {
		// Get rid of highlight on current GameObject.
		ToggleHighlight();
		// Select GameObject by decrementing index of array by 1.
		// if index == 0, select the last GameObject in array.
		if (arrayIndex == 0) 
			selected = sceneState.getObject(arrayIndex - 1);
		else {
			arrayIndex -= 1;
			selected = sceneState.getObject(arrayIndex);
		}

		// Need code for camera.

		// Highlight the newly selected shape.
		ToggleHighlight();		
	}

	void ParseRight (int arrayIndex) {
		// Get rid of highlight on current GameObject.
		ToggleHighlight();

		// Select GameObject by incrementing index of array by 1.
		// if index == array.length - 1, select the first GameObject in array.
		if (arrayIndex == arrayLength - 1) 
			selected = sceneState.getObject(arrayIndex - 1);
		else {
			arrayIndex += 1;
			selected = sceneState.getObject(arrayIndex);
		}

		// Need code for camera.

		// Highlight the newly selected shape.
		ToggleHighlight();	
	}

	void ReturnToMenu() {
		// Get rid of highlight on current GameObject.
		ToggleHighlight();
		// Change back to menu mode.
	}

	void ToggleHighlight() {
		// Toggle between highlighted and not.
		if (selected.GetComponent<Renderer>().material.color == Color.white) 
			selected.GetComponent<Renderer>().material.color = Color.red;
		else
			selected.GetComponent<Renderer>().material.color = Color.white;
	}

	// ----- MODE CHANGING METHODS -----
	public void MenuMode() {
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
		// Change the mode to exit
		modes.mode = Modes.Mode.Select;
		// Open the lock on the runner.
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// Close the lock here.
		this.locked = true;
	}// Exit
	
}// CreateMode
