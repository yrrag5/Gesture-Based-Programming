using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMode : MonoBehaviour {
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
			Debug.Log("SelectMode Unlocked");



		}// if
	}// Update

	public void CreateMode() {
		// change the mode to exit
		modes.mode = Modes.Mode.Create;
		// open the lock on the runner
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// close the lock here
		this.locked = true;
	}// Exit

}// SelectMode
