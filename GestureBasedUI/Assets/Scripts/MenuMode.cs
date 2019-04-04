using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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
	private Pose lastPose;// Pose object to track last pose
	public Canvas gameUI;// Canvas object

	public Button[] UIButtons;

	public int selectedButton = 0;
	public Button selected;
	public Material shapecolor;
	public Material highlight;

	void Start() {
		ss = SceneState.getInstance;
		GameObject[] buttons = GameObject.FindGameObjectsWithTag("button"); // Find all button objects 
		UIButtons = new Button[buttons.Length];// Setting the size of the array of buttons 
		Debug.Log(buttons.Length);
		for(int i = 0; i < buttons.Length; i++)
			UIButtons[i] = buttons[i].GetComponent<Button>();	
		ToggleHighlight();
	}

	void Update(){
		// if the class is not locked out
		if(!locked){
			//Debug.Log("MenuMode Unlocked");
			// myo = GameObject.FindGameObjectWithTag("myo");
			// Access the ThalmicMyo component attached to the Myo object.
        	ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();
			/* if the fist pose is detected and was the last pose
			else if(thalmicMyo.pose == Pose.CloseFist != lastPose) {
				Close fist will bring up settings menu to display controls of myo gestures
				} */
			// if the fist pose is detected and was the last pose
			if(thalmicMyo.pose == Pose.WaveIn && thalmicMyo.pose != lastPose) {
				// move down through the button array 
				selected = getNextButton(0);
				// Highlights selected button
				ToggleHighlight();
			} else if(thalmicMyo.pose == Pose.WaveOut && thalmicMyo.pose != lastPose) {
				// move up through the button array
				selected = getNextButton(1);
				// Highlights selected button
				ToggleHighlight();
			} else if(thalmicMyo.pose == Pose.FingersSpread && thalmicMyo.pose != lastPose) {
				// ask the user if the would like to exit
				gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("Repeat Finger-Spread gesture to exit application.");
			} else if(lastPose == Pose.FingersSpread) {
				// exit the application
				Exit();
			} else if(lastPose == Pose.DoubleTap) {
				ToggleHighlight();
				// get name of the button to access functionality 
				string bName = UIButtons[selectedButton].ToString();
				// If the button is an object to instanciate, pass it to the select mode
				if(bName.Equals("CubeObject")){
					// Instanciate object at position of danger zone
					GameObject g = (GameObject)Instantiate(Resources.Load("Cube"), GameObject.FindWithTag("SpawnPoint").transform.position, Quaternion.identity);
					// Add that object to the scene state
					ss.AddGameObject(g);
					// Pass selectmode the object and its position in the scene state array
					SelectMode(g,ss.ArrayLength());
				}
				if(bName.Equals("CuboidObject")){
					// Instanciate object at position of danger zone
					GameObject g = (GameObject)Instantiate(Resources.Load("Cuboid"), GameObject.FindWithTag("SpawnPoint").transform.position, Quaternion.identity);
					// Add that object to the scene state
					ss.AddGameObject(g);
					// Pass selectmode the object and its position in the scene state array
					SelectMode(g,ss.ArrayLength());
				}
				if(bName.Equals("CylinderObject")){
					// Instanciate object at position of danger zone
					GameObject g = (GameObject)Instantiate(Resources.Load("Cylinder"), GameObject.FindWithTag("SpawnPoint").transform.position, Quaternion.identity);
					// Add that object to the scene state
					ss.AddGameObject(g);
					// Pass selectmode the object and its position in the scene state array
					SelectMode(g,ss.ArrayLength());
				}
					 
				// If the button is continue, enter create mode 
				if(bName.Equals("Continue")){
					CreateMode();
				}
				// If the button is load, enter the load ui
				if(bName.Equals("LoadScene")){
					LoadUi();
				}

				// If the button is save, save current state
				if(bName.Equals("SaveScene")){
					SaveUi();
				}
				// If the button is exit
				if(bName.Equals("Exit")){
					Exit();					
				}
			}// if/else if

			// update the last pose detected
			lastPose = thalmicMyo.pose;
			//Set transform position of the camera to x = 0 y = 1 z = -10 
		}// if
	}// Update

	public Button getNextButton(int direction){
		// If the direction is 1 change is positive, else negative
		if(direction == 1){
			if(selectedButton == UIButtons.Length)
				selectedButton = 0;
			else
				selectedButton++;		
		}//if
		else{
			if(selectedButton == 0)
				selectedButton = UIButtons.Length;
			else
				selectedButton--;	
		}//else

		return UIButtons[selectedButton];
	}//GetNextButton

	public void ToggleHighlight() {
		// If material is highlighted.
		if (selected.GetComponent<Renderer>().material == highlight)
			selected.GetComponent<Renderer>().material = shapecolor;
		else { // otherwise, store the material and then change to highlighted.
			shapecolor = selected.GetComponent<Renderer>().material;
			selected.GetComponent<Renderer>().material = highlight;
		}
	}
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

	public void SelectMode(GameObject g, int index) {
		// Pass references to select mode
		GameObject.FindObjectOfType<SelectMode>().SetSelected(g,index);
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

	public void SaveUi(){

	}
	public void LoadUi(){

	}
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
