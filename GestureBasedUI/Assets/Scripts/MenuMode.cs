using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class MenuMode : MonoBehaviour {
	public GameObject myo = null;// Myo game object to connect with
	public static Modes modes;
	// lock
	private bool locked = true;
	public bool Locked {
		get { return locked; }
		set { locked = value; }
	}// lock accesssor
	SceneState ss;
	private Pose lastPose; // Pose object to track last pose
	public Canvas gameUI;  // Canvas object
	public GameObject loadScreen; // object
	public GameObject UIScreen; // object
	public Button[] UIButtons;

	public int selectedButton = 0;
	private Button selected;
	private Material shapecolor;
	public Material highlight;
	[SerializeField]
	private GameObject cube;
	[SerializeField]
	private GameObject cuboid;
	[SerializeField]
	private GameObject cylinder;
	private bool isExiting = false;
	private bool allowAccess = false;
	private int consecutive = 0;

	void Start() {
		ss = SceneState.getInstance;
		// Setting the size of the array of buttons
		UIButtons = new Button[7];
		// getting a handle on the buttons
		for(int i = 0; i < UIButtons.Length; i++)
			UIButtons[i] = GameObject.FindGameObjectWithTag("button" + i).GetComponent<Button>();
		selected = UIButtons[0];
	}// Start

	void Update() {
		// if the class is not locked out
		if(!locked) {
			// myo = GameObject.FindGameObjectWithTag("myo");
			// Access the ThalmicMyo component attached to the Myo object.
        	ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
			if(isExiting) {
				if(thalmicMyo.pose == Pose.FingersSpread)
					consecutive++;// increment the counter
				else if(consecutive > 30) {
					// vibrate the Myo
					thalmicMyo.Vibrate(VibrationType.Short);
					Exit();// exit the app
				} else if(thalmicMyo.pose != Pose.Rest) {
					isExiting = false;// stop exiting
					consecutive = 0;// reset counter
				}// if..else if
			} else if(allowAccess) {
				if(thalmicMyo.pose == Pose.WaveIn && thalmicMyo.pose != lastPose) {
					// Highlights selected button
					HighlightMaterial();
					// move down through the button array 
					selected = getNextButton(0);
					// Highlights selected button
					HighlightMaterial();
				} else if(thalmicMyo.pose == Pose.WaveOut && thalmicMyo.pose != lastPose) {
					// Highlights selected button
					HighlightMaterial();
					// move up through the button array
					selected = getNextButton(1);
					// Highlights selected button
					HighlightMaterial();
				} else if(thalmicMyo.pose == Pose.FingersSpread && isExiting == false) {
					isExiting = true;
					// ask the user if the would like to exit
					gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("Repeat Finger-Spread gesture to exit application.");
					// vibrate the Myo
					thalmicMyo.Vibrate(VibrationType.Short);
				} else if(thalmicMyo.pose == Pose.DoubleTap && lastPose != Pose.DoubleTap) {
					// get name of the button to access functionality 
					string bName = UIButtons[selectedButton].ToString();
					
					// If the button is an object to instanciate, pass it to the select mode
					if(bName.Equals("CubeObject (UnityEngine.UI.Button)")) {
						CreateCube();
					}// if
					if(bName.Equals("CuboidObject (UnityEngine.UI.Button)")) {
						CreateCuboid();
					}// if
					if(bName.Equals("CylinderObject (UnityEngine.UI.Button)")) {
						CreateCylinder();
					}// if
						
					// If the button is continue, enter create mode 
					if(bName.Equals("Continue (UnityEngine.UI.Button)")) {
						CreateMode();
					}// if
					// If the button is load, enter the load ui
					if(bName.Equals("LoadScene (UnityEngine.UI.Button)")) {
						ShowLoadUI();
					}// if

					// If the button is save, save current state
					if(bName.Equals("SaveScene (UnityEngine.UI.Button)")) {
						Save();
					}// if
					// If the button is exit
					if(bName.Equals("Exit (UnityEngine.UI.Button)")) {
						Exit();					
					}// if
				}// if...else if

				// reset the message at rest				
				if(thalmicMyo.pose == Pose.Rest) gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");

				// update the last pose detected
				lastPose = thalmicMyo.pose;
			}// if

			if(thalmicMyo.pose == Pose.Rest && allowAccess == false && UIScreen.activeSelf) { 
				Debug.Log("UI enabled");
				allowAccess = true;
				HighlightMaterial();
			}// if
		} else allowAccess = false;
	}// Update

	public Button getNextButton(int direction){
		// If the direction is 1 change is positive, else negative
		if(direction == 1) {
			if(selectedButton == UIButtons.Length -1)
				selectedButton = 0;
			else
				selectedButton++;		
		} else {
			if(selectedButton == 0)
				selectedButton = UIButtons.Length -1;
			else
				selectedButton--;	
		}// if/else

		return UIButtons[selectedButton];
	}//GetNextButton

	
	public void CreateCylinder() {
		// Instanciate object at position of danger zone
		GameObject g = (GameObject)Instantiate(cylinder, GameObject.FindWithTag("SpawnPoint").transform.position, Quaternion.identity);
		// set the name of the object, remove (clone)
		g.name = cylinder.name;
		// Add that object to the scene state
		ss.AddGameObject(g);
		// Pass selectmode the object and its position in the scene state array
		SelectMode(g,ss.ArrayLength());
	}// CreateCylinder

	public void CreateCube() {
		// Instanciate object at position of danger zone
		GameObject g = (GameObject)Instantiate(cube, GameObject.FindWithTag("SpawnPoint").transform.position, Quaternion.identity);
		// set the name of the object, remove (clone)
		g.name = cube.name;
		// Add that object to the scene state
		ss.AddGameObject(g);
		// Pass selectmode the object and its position in the scene state array
		SelectMode(g,ss.ArrayLength());
	}// CreateCube

	public void CreateCuboid() {
		// Instanciate object at position of danger zone
		GameObject g = (GameObject)Instantiate(cuboid, GameObject.FindWithTag("SpawnPoint").transform.position, Quaternion.identity);
		// set the name of the object, remove (clone)
		g.name = cuboid.name;
		// Add that object to the scene state
		ss.AddGameObject(g);
		// Pass selectmode the object and its position in the scene state array
		SelectMode(g,ss.ArrayLength());
	}// CreateCuboid

	public void HighlightMaterial() {
		// If material is highlighted.
		if (selected.GetComponent<Image>().material == highlight)
			selected.GetComponent<Image>().material = shapecolor;
		else { // otherwise, store the material and then change to highlighted.
			shapecolor = selected.GetComponent<Image>().material;
			selected.GetComponent<Image>().material = highlight;
		}// if/else
	}// HighlightMaterial

	public void Save() {
		ss.SaveState();
	}// Save

	public void ShowLoadUI() {
		allowAccess = false;
		loadScreen.SetActive(true);
		UIScreen.SetActive(false);
	}// ShowLoadUI

	public void HideLoadUI() {
		allowAccess = true;
		loadScreen.SetActive(false);
		UIScreen.SetActive(true);
	}// HideLoadUI

	public void CreateMode() {
		allowAccess = false;
		HighlightMaterial();
		gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");
        // gets a handle on the singleton instance
        modes = Modes.getInstance;
        // change the mode
        modes.mode = Modes.Mode.Create;
		// open the lock on the runner
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		// close the lock here
		this.locked = true;
		mr.Locked = false;
	}// CreateMode

	public void SelectMode(GameObject g, int index) {
		allowAccess = false;
		HighlightMaterial();
		gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("");
		// Pass references to select mode
		GameObject.FindObjectOfType<SelectMode>().SetSelected(g,index);
        // gets a handle on the singleton instance
        modes = Modes.getInstance;
        // change the mode
        modes.mode = Modes.Mode.Select;
		// open the lock on the runner
		ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
		// close the lock here
		this.locked = true;
		mr.Locked = false;
	}// CreateMode

	public void Exit() {
		Debug.Log("exited");
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
