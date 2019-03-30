using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTest : MonoBehaviour {
	public static Modes currMode;

	public SceneState sceneState;
	public GameObject selected;
	public int arrayLength;
	public int arrayIndex;

	// Use this for initialization
	void Start () {

		currMode = Modes.getInstance;
		currMode.mode = Modes.Mode.Create;
		
		sceneState = (SceneState)FindObjectOfType(typeof(SceneState));
		
		// Get the length of the array
		arrayLength = sceneState.ArrayLength();
		
		if (arrayLength != 0) {
			Debug.Log("length: " + arrayLength);

			// set selected shape to last shape in GameObjects array
			selected = sceneState.getObject(arrayLength - 1);
		}
		else {
			Debug.Log("Object array is empty");
		}

	}// start
	
	// Update is called once per frame
	void Update () {
		// detect Myo gestures (left, right, close, exit).
	}

	void ParseLeft (int arrayIndex) {
		// Get rid of highlight on current GameObject.
		ToggleHighlight(arrayIndex);
		// Select GameObject by decrementing index of array by 1.
		// if index == 0, select the last GameObject in array.
		// Highlight the newly selected shape.
	}

	void ParseRight (int arrayIndex) {
		// Get rid of highlight on current GameObject.
		ToggleHighlight(arrayIndex);
		// Select GameObject by incrementing index of array by 1.
		// if index == array.length - 1, select the first GameObject in array.
		// Highlight the newly selected shape.
	}

	void ReturnToMenu() {
		// Get rid of highlight on current GameObject.
		ToggleHighlight(arrayIndex);
		// Change back to menu mode.
	}

	void SelectHighlighted(int arrayIndex) {
		// Change to Select mode, passing the array index of the current highlighted object.
	}

	void ToggleHighlight(int arrayIndex) {
		// Toggle between highlighted and not.
	}

}
