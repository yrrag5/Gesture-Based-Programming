using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeRunner : MonoBehaviour {
	public static Modes modes;

	// Use this for initialization
	void Start() {
		// gets a handle on the singleton instance
		modes = Modes.getInstance;
		
		// call the mode control function
		ModeControl();
	}// Start
	
	// This function will handle the various state changes of the app
	void ModeControl() {
		// infinite loop
		while(true) {
			// get a handle on the current mode,
			// switch statement to access the various mode functionallity
			switch(modes.mode) {
				case Modes.Mode.Exit:
					// call the application exit function
					Exit();
					break;
				case Modes.Mode.Create:
					// create an instance of the create class here
					// or could use the singleton design pattern here?
					break;
				case Modes.Mode.Select:
					// create an instance of the select class here
					// or could use the singleton design pattern here also?
					break;
				case Modes.Mode.Menu:
					// create an instance of the create class here
					// or could use the singleton design pattern here also?
					// singleton would show UI when accessed and hide on exit
					break;
			}// switch
		}// while
	}// ModeControl

	// Exit the application
	void Exit() {
		Application.Quit();
	}// Exit

}// ModeRunner
