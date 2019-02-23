using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModesTest : MonoBehaviour {
	public static Modes modes;
	public static Modes modes2;
	public bool testing;

	// Use this for initialization
	void Start () {
		modes = Modes.getInstance;
		testing = true;
	}// start
	
	// Update is called once per frame
	void Update () {
		if(testing){
			// check if it has been set initially
			if(modes.mode == Modes.Mode.Menu) {
				modes.mode = Modes.Mode.Create;
				// check if it has been set correctly
				if(modes.mode == Modes.Mode.Create) {
					modes.mode = Modes.Mode.Select;
					if(modes.mode == Modes.Mode.Select) {
						// Debug.Log("TEST OK: Modes Test cases Passed.");
						// testing = false;
					} else {
						Debug.Log("TEST FAIL: Value did not change from 'Create' to 'Select'.");
					}// if/else
				} else {
					Debug.Log("TEST FAIL: Value did not change from 'Menu' to 'Create'.");
				}// if/else
			} else {
				Debug.Log("TEST FAIL: Mode value not set initially.");
			}// if/else

			// checking if singleton is correctly working
			modes2 = Modes.getInstance;
			if(modes2.mode == Modes.Mode.Select) {
				Debug.Log("TEST OK: Modes Test cases Passed.");
				testing = false;
			}// if
			
		}// if
	}// Update
}// ModesTest
