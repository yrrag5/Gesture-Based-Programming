using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modes : MonoBehaviour {
	// singleton design pattern  
    private static Modes instance = null; 
	// enum
	public enum Mode {Menu, Create, Select, Exit}
	public Mode mode;

	void Awake() {
		if(instance != null && instance != this) {
			Destroy(this.gameObject);
		}// if
		DontDestroyOnLoad(this.gameObject);
	}// Awake

	private Modes() {  
		// set the initial mode
		this.mode = Mode.Menu;
	}// Constructor 

    public static Modes getInstance {  
        get {   
			// check to see if the instance is null
			if (instance == null){
				// check to see if there is another instance of this class
				instance = GameObject.FindObjectOfType<Modes>();
				if (instance == null){
					instance = new Modes(); 				
				}// if
			}  // if
			return instance;   
	    }// get
    }// getInstance  
	
}// Modes
