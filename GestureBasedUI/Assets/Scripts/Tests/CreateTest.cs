using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTest : MonoBehaviour {
	public static Modes currMode;

	public SceneState sceneState;
	public GameObject selected;
	public int arrayLength;
	public int arrayIndex;

	// Use this for initialization.
	void Start () {
		currMode = Modes.getInstance;
		currMode.mode = Modes.Mode.Create;
		
		sceneState = (SceneState)FindObjectOfType(typeof(SceneState));

		// Get the length of the array.
		arrayLength = sceneState.ArrayLength();
		
		if (arrayLength != 0) {
			Debug.Log("length: " + arrayLength);
			// Set selected shape to last shape in GameObjects array.
			selected = sceneState.getObject(arrayLength - 1);
		} else {
			Debug.Log("Object array is empty");
		}// if/else
	}// start

	void ParseLeft (int arrayIndex) {
		// Get rid of highlight on current GameObject.
		HighlightMaterial();
		// Select GameObject by decrementing index of array by 1.
		// if index == 0, select the last GameObject in array.
		if (arrayIndex == 0) 
			selected = sceneState.getObject(arrayIndex - 1);
		else {
			arrayIndex -= 1;
			selected = sceneState.getObject(arrayIndex);
		}// if/else

		// Highlight the newly selected shape.
		HighlightMaterial();		
	}// ParseLeft

	void ParseRight (int arrayIndex) {
		// Get rid of highlight on current GameObject.
		HighlightMaterial();

		// Select GameObject by incrementing index of array by 1.
		// if index == array.length - 1, select the first GameObject in array.
		if (arrayIndex == arrayLength - 1) 
			selected = sceneState.getObject(arrayIndex - 1);
		else {
			arrayIndex += 1;
			selected = sceneState.getObject(arrayIndex);
		}// if/else

		// Highlight the newly selected shape.
		HighlightMaterial();	
	}// ParseRight

	void ReturnToMenu() {
		// Get rid of highlight on current GameObject.
		HighlightMaterial();
	}// ReturnToMenu

	void HighlightMaterial() {
		// Toggle between highlighted and not.
		if (selected.GetComponent<Renderer>().material.color == Color.white) 
			selected.GetComponent<Renderer>().material.color = Color.red;
		else
			selected.GetComponent<Renderer>().material.color = Color.white;
	}// HighlightMaterial
	
}// CreateTest