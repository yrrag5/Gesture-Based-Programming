using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeRunner : MonoBehaviour {
	public static Modes modes;
	public GameObject UI;
	// lock
	private bool locked = false;
	public bool Locked {
		get { return locked; }
		set { locked = value; }
	}// locked accessor

	// Use this for initialization
	void Start() {
		// gets a handle on the singleton instance
		modes = Modes.getInstance;
	}// Start
	
	// This function will handle the various state changes of the app
	void Update() {
		// if the lock has not been activated
		if(!locked){
			// lock the statement block
			locked = true;

			// get a handle on the current mode,
			// switch statement to access the various mode functionallity
			switch(modes.mode) {
				case Modes.Mode.Exit:
					// call the application exit function
					Exit();
					break;
				case Modes.Mode.Create:
					// hide the user interface
					UI.SetActive(false);
					// create an instance of the create class here
					// or could use the singleton design pattern here?
					CreateMode cm = GameObject.FindObjectOfType<CreateMode>();
					cm.Locked = false;
					// sceneState instance
					SceneState ss = SceneState.getInstance;
					// set selected if possible
					if(ss.ArrayLength() > 0)cm.selected = ss.getObject(ss.ArrayLength()-1);
					break;
				case Modes.Mode.Select:
					// hide the user interface
					UI.SetActive(false);
					// create an instance of the select class here
					// or could use the singleton design pattern here also?
					SelectMode sm = GameObject.FindObjectOfType<SelectMode>();
					sm.Locked = false;
					break;
				case Modes.Mode.Menu:
					// show the user interface
					UI.SetActive(true);
					// create an instance of the create class here
					// or could use the singleton design pattern here also?
					// singleton would show UI when accessed and hide on exit
					MenuMode mm = GameObject.FindObjectOfType<MenuMode>();
					mm.Locked = false;
					break;
				default:
					locked = false;
					break;
			}// switch
		}// if
	}// Update

	// Exit the application
	void Exit() {
		Application.Quit();
	}// Exit

}// ModeRunner
