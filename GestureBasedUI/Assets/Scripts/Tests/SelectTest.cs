using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTest : MonoBehaviour {
	public int index;
	public static Modes modes;
	SceneState sceneState;
	SelectMode selectMode;

	// Use this for initialization
	void Start () {
		index = 0;
		// gets a handle on the singleton instance
		modes = Modes.getInstance;
		// change the mode
		modes.mode = Modes.Mode.Select;
		
		// get a handle on the selectMode
		sceneState = (SceneState)FindObjectOfType(typeof(SceneState));
	}// Start
	
	// Update is called once per frame
	void Update () {
		// change the mode
		modes.mode = Modes.Mode.Select;

		if (Input.GetKeyDown ("c")) {
            // cycle through the objects
		CycleIndex();
        }

		if(sceneState.ArrayLength() > 0){
			// get a handle on the sceneState
			selectMode = (SelectMode)FindObjectOfType(typeof(SelectMode));
			// pass an object from the sceneState to selectMode
			selectMode.SetSelected(sceneState.getObject(index), index);
		}// if
	}// Update

	void CycleIndex(){
		if(sceneState.ArrayLength()-1 < index || index < 0)
			index = 0;
		else
			index = index +1;
	}
}// SelectTest
