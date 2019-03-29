using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTest : MonoBehaviour {
	public static Modes currMode;

	public GameObject selected;

	// Use this for initialization
	void Start () {

		currMode = Modes.getInstance;
		currMode.mode = Modes.Mode.Create;
		// set selected shape to last shape in GameObjects array
		
	}// start
	
	// Update is called once per frame
	void Update () {
		// detect Myo gestures (left, right, close, exit)
		
	}

	void ParseLeft () {
		// Get rid of highlight on current GameObject.
		// Select GameObject by decrementing index of array by 1.
		// if index == 0, select the last GameObject in array.
	}

	void ParseRight () {
		// Get rid of highlight on current GameObject.
		// Select GameObject by incrementing index of array by 1.
		// if index == array.length - 1, select the first GameObject in array.
	}

	void ReturnToMenu() {
		// Change back to menu mode.
	}

	void SelectHighlighted() {
		// Change to Select mode, passing the array index of the current highlighted object.
	}

	void ToggleHighlight() {
		// Toggle between highlighted and not.
	}

}
