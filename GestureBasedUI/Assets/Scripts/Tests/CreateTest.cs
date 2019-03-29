using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTest : MonoBehaviour {
	public static Modes currMode;
<<<<<<< HEAD

	public GameObject selected;

	// Use this for initialization
	void Start () {

		currMode = Modes.getInstance;
		currMode.mode = Modes.Mode.Create;
		// set selected shape to last shape in GameObjects array
		selected = ArrayList.IndexOf();
=======

	// Use this for initialization
	void Start () {
		currMode = Modes.getInstance;
		currMode.mode = Modes.Mode.Create;
>>>>>>> 8ad06d9a0b251ab568a6da928dbce27580dd3190
	}// start
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
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
=======

		currMode.mode = Modes.Mode.Create;

		if (Input.GetMouseButtonDown (0)) {
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.transform.position = new Vector3 (0f, 2f, -7f);
			cube.AddComponent<Rigidbody>();
		}
		if (Input.GetMouseButtonDown (1)) {
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
			cube.transform.position = new Vector3 (0f, 2f, -7f);
			cube.AddComponent<Rigidbody>();
		}
		
	}

	void Create (string prefabName) {
		// This method recieves a string and creates the corresponding prefab. 
		// 
		
>>>>>>> 8ad06d9a0b251ab568a6da928dbce27580dd3190
	}

	void ToggleHighlight() {
		// Toggle between highlighted and not.
	}

}
