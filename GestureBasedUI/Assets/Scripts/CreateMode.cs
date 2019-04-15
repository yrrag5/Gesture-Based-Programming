using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class CreateMode : MonoBehaviour {
	public static Modes modes;
	private SceneState sceneState;
	public GameObject selected;
	public int arrayLength;
	public int arrayIndex;
	private Material shapecolor;
	public Material highlight;
	public Canvas gameUI;
	public GameObject myo = null;
	private Pose lastPose = Pose.Unknown;
	private bool isExiting = false;
	private bool allowAccess = false;
	private int consecutive = 0;

	// Lock.
	private bool locked = true;

	public bool Locked {
		get { return locked; }
		set { locked = value; }
	}// Lock accesssor.

	void Start() {
		sceneState = (SceneState)FindObjectOfType(typeof(SceneState));

		// Get the length of the array.
		arrayLength = sceneState.ArrayLength();
		
		if (arrayLength != 0) {
			// Set selected shape to last object in GameObjects array.
			selected = sceneState.getObject(arrayLength - 1);
			// Set array index to last object in array.
			arrayIndex = arrayLength - 1;
		} else {
			arrayIndex = 1;
		}// if/else
	}// Start

	void Update() {
		// If the class is not locked out.
		if(!locked) {
			// Access the ThalmicMyo component attached to the Myo object.
			ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

			if(selected == null && sceneState.ArrayLength() > 0)
				selected = sceneState.getObject(0);
			else if(sceneState.ArrayLength() == 0) {
				gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("No objects! Please create objects.");
				if(thalmicMyo.pose == Pose.FingersSpread)
					MenuMode();
			}// if/else if
			if(selected != null && sceneState.ArrayLength() >= 0) {
				
				arrayLength = sceneState.ArrayLength();
				FocusSelected();

				if(isExiting) {
					if(thalmicMyo.pose == Pose.FingersSpread) {
						consecutive++;// increment the counter
					} else if(consecutive > 30) {
						// Get rid of highlight on current GameObject.
						OriginalMaterial();
						isExiting = false;
						MenuMode();// exit to menu mode
					} else if(thalmicMyo.pose != Pose.Rest) {
						isExiting = false;// stop exiting
						consecutive = 0;// reset counter
					}// if..else if
				} else if(allowAccess) {
					// Update references when the pose becomes fingers spread or the q key is pressed.
					// if pose == whatever && != lastpose
					if (thalmicMyo.pose == Pose.FingersSpread && isExiting) {
						gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");
						MenuMode();
					} else if(thalmicMyo.pose == Pose.FingersSpread) {
						isExiting = true;
						gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");
						// Ask the user if they want to exit to menu.
						gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("Repeat Finger-Spread gesture to exit to Menu.");
					} else if (thalmicMyo.pose == Pose.WaveIn && thalmicMyo.pose != lastPose) {
						gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");
						ParseLeft();
					} else if (thalmicMyo.pose == Pose.WaveOut && thalmicMyo.pose != lastPose) {
						gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");
						ParseRight();
					} else if (thalmicMyo.pose == Pose.DoubleTap && thalmicMyo.pose != lastPose) {
						gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("Double-tap to enter select mode.");
					} else if (thalmicMyo.pose == Pose.DoubleTap) {
						gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");
						SelectMode();
					}// if...else if

					if(thalmicMyo.pose != Pose.FingersSpread) isExiting = false;

					lastPose = thalmicMyo.pose;
				}// if	

				if(thalmicMyo.pose == Pose.Rest && allowAccess == false) { 
					allowAccess = true;
					// HighlightMaterial();
				}// if	
			}// if

		} else allowAccess = false;	
	}// Update

	void ParseLeft() {
		if(sceneState.ArrayLength() > 1) {
			// Get rid of highlight on current GameObject.
			OriginalMaterial();
			// Select GameObject by decrementing index of array by 1.
			// if index == 0, select the last GameObject in array.
			if (arrayIndex == 0) {
				arrayIndex = arrayLength - 1;
				selected = sceneState.getObject(arrayIndex);
			} else {
				arrayIndex -= 1;
				selected = sceneState.getObject(arrayIndex);
			}// if/else

			if(selected != null) {
				// Focus camera on object.
				FocusSelected();
				// Highlight the newly selected shape.
				HighlightMaterial();
			}// if
		}// if
	}// ParseLeft

	void ParseRight() {
		if(sceneState.ArrayLength() > 1) {
			// Get rid of highlight on current GameObject.
			OriginalMaterial();

			// Select GameObject by incrementing index of array by 1.
			// if index == array.length - 1, select the first GameObject in array.
			if (arrayIndex == arrayLength - 1) {
				arrayIndex = 0;
				selected = sceneState.getObject(arrayIndex);
			} else {
				arrayIndex = arrayIndex + 1;
				selected = sceneState.getObject(arrayIndex);
			}// if/else

			if(selected != null) {
				// Focus camera on object.
				FocusSelected();
				// Highlight the newly selected shape.
				HighlightMaterial();
			}// if
		}// if
	}// ParseRight

	// Focus camera on selected object.
	public void FocusSelected(){
		// getting a handle on the LookAt script
		LookAt lookAt = (LookAt)FindObjectOfType(typeof(LookAt));
		// passing the selected objects transform to the LookAt script
		lookAt.Focus(selected);
	}// FocusSelected

	public void HighlightMaterial() {
		shapecolor = selected.GetComponent<Renderer>().material;
		selected.GetComponent<Renderer>().material = highlight;
	}// HighlightMaterial

	public void OriginalMaterial() {
		selected.GetComponent<Renderer>().material = shapecolor;
	}// OriginalMaterials

	public void MenuMode() {
		if(selected != null) {
			// Get rid of highlight on current GameObject.
			OriginalMaterial();
		}// if
		allowAccess = false;
		gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");
		// Gets a handle on the singleton instance.
		modes = Modes.getInstance;
		// Change the mode.
		modes.mode = Modes.Mode.Menu;
		// Open the lock on the runner.
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// Close the lock here.
		this.locked = true;
	}// CreateMode

	public void SelectMode() {
		// Get rid of highlight on current GameObject.
		OriginalMaterial();
		allowAccess = false;
		gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");
		// Pass selected game object to SelectMode script.
	 	SelectMode sm = GameObject.FindObjectOfType<SelectMode>();
	  	sm.SetSelected(selected, arrayIndex);

		// Gets a handle on the singleton instance.
		modes = Modes.getInstance;
		// Change the mode.
		modes.mode = Modes.Mode.Select;
		// Open the lock on the runner.
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		mr.Locked = false;
		// Close the lock.
		this.locked = true;
	}// Exit
	
}// CreateMode
