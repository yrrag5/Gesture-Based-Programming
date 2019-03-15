using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTest : MonoBehaviour {
	public static Modes currMode;

	// Use this for initialization
	void Start () {
		currMode = Modes.getInstance;
		currMode.mode = Modes.Mode.Create;
	}// start
	
	// Update is called once per frame
	void Update () {

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
		
	}
}
