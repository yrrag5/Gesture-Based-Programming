using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureBehaviour : MonoBehaviour {
	// singleton design pattern  
    private static GestureBehaviour instance = null; 
	// enum
	public enum Gesture {N0NE, LEFTWAVE, RIGHTWAVE, CLOSEDFIST, OPENPALM, DOUBLETAP}
	public Gesture gesture;

	void Awake() {
		if(instance != null && instance != this) {
			Destroy(this.gameObject);
		}// if
		DontDestroyOnLoad(this.gameObject);
	}// Awake

	private GestureBehaviour() {  
		// set the initial mode
		this.gesture = Gesture.N0NE;
	}// Constructor 

    public static GestureBehaviour getInstance {  
        get {   
			// check to see if the instance is null
			if (instance == null){
				// check to see if there is another instance of this class
				instance = GameObject.FindObjectOfType<GestureBehaviour>();
				if (instance == null){
					instance = new GestureBehaviour(); 				
				}// if
			}  // if
			return instance;   
	    }// get
    }// getInstance  


	// this function is for testing purposes only
	public void Parse(string myString) {
		try
		{
			Gesture enumerable = (Gesture)System.Enum.Parse(typeof(Gesture), myString);
			gesture = enumerable;
		} catch (System.Exception) {
			Debug.LogErrorFormat("Parse: Can't convert {0} to enum, please check the spelling of: ", myString);
		}// try/catch
	}// Parse
	
}// GestureBehaviour
