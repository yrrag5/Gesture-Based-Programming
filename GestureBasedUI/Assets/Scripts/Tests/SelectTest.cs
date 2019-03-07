using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTest : MonoBehaviour {
	public static Modes modes;

	// Use this for initialization
	void Start () {
		// gets a handle on the singleton instance
		modes = Modes.getInstance;
		// change the mode
		modes.mode = Modes.Mode.Select;
	}
	
	// Update is called once per frame
	void Update () {
		// change the mode
		modes.mode = Modes.Mode.Select;
	}
}
