using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMode : MonoBehaviour {
	public static Modes modes;
	// lock
	private bool locked = true;
	public bool Locked {
		get { return locked; }
		set { locked = value; }
	}// lock accesssor

	void Update(){
		// if the class is not locked out
		if(!locked){
			Debug.Log("MenuMode Unlocked");
            //Set transform position of the camera to x = 0 y = 1 z = -10 
			


		}// if
	}// Update

	public void CreateMode() {
        // gets a handle on the singleton instance
        modes = Modes.getInstance;
        // change the mode
        modes.mode = Modes.Mode.Create;
		// open the lock on the runner
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// close the lock here
		this.locked = true;
	}// CreateMode

	public void SelectMode() {
        // gets a handle on the singleton instance
        modes = Modes.getInstance;
        // change the mode
        modes.mode = Modes.Mode.Select;
		// open the lock on the runner
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// close the lock here
		this.locked = true;
	}// CreateMode

	public void Exit() {
        // gets a handle on the singleton instance
        modes = Modes.getInstance;
        // change the mode to exit
        modes.mode = Modes.Mode.Exit;
		// open the lock on the runner
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// close the lock here
		this.locked = true;
	}// Exit

}// MenuMode
