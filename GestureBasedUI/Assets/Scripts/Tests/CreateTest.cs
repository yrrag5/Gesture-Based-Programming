using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTest : MonoBehaviour {
	public static Modes modes;
	public static Modes modes2;
	public bool create;

	// Use this for initialization
	void Start () {
		modes = Modes.getInstance;
		modes.mode = Modes.Mode.Create;
	}// start
	
	// Update is called once per frame
	void Update () {
		modes.mode = Modes.Mode.Create;
	}
}
