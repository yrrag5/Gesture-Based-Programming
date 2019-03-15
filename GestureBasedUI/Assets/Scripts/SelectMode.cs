using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMode : MonoBehaviour {
	public static Modes modes;
	// lock
	private bool locked = true;
	// selected gameobject placeholder
	private GameObject selected;

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

	public void setSelected(GameObject g){
		// set the object as selected
		selected = g;
		// focus on selected object
		FocusSelected();
	}// setSelected

	public void FocusSelected(){
		// getting a handle on the LookAt script
		LookAt lookAt = (LookAt)FindObjectOfType(typeof(LookAt));
		// passing the selected objects transform to the LookAt script
		lookAt.Focus(selected.transform.position);
	}// focusSelected

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
