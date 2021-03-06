﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SceneState : MonoBehaviour {
	// singleton design pattern  
    private static SceneState instance = null; 
	// GameObject array initialization
	private GameObject[] objects;

	// For saving & loading.
	private GameObject gobject;
    private string myPath; 
    private string saveName;
    private int arrayLength;
    private string objectName;
    private float x;
    private float y;
    private float z;

	void Awake() {
		this.objects = new GameObject[0];

		if(instance != null && instance != this) {
			Destroy(this.gameObject);
		}// if
		DontDestroyOnLoad(this.gameObject);
	}// Awake

	private SceneState() {  
	}// Constructor 

    public static SceneState getInstance {  
        get {   
			// check to see if the instance is null
			if (instance == null) {
				// check to see if there is another instance of this class
				instance = GameObject.FindObjectOfType<SceneState>();
				if (instance == null) {
					instance = new SceneState(); 				
				}// if
			}// if
			return instance;   
	    }// get
    }// getInstance  

	public void AddGameObject(GameObject newObject) {
		int i;
		// create a new array with space for another element
		GameObject[] temp = new GameObject[ArrayLength() + 1];
		// copy the contents of the objects array into the new array
		for(i = 0; i < ArrayLength(); i++) {
			temp[i] = objects[i];
		}// for
		// add the new gameobject to the new array
		temp[ArrayLength()] = newObject;
		// set the objects array to the new array
		objects = temp;
	}// AddGameObject

	public void RemoveGameObject(int index) {
		int count = 0;
		// create a new array with one less space for another element
		GameObject[] temp = new GameObject[objects.Length - 1];
		// copy the contents of the objects array, excluding the index, into the new array
		for(int i = 0; i < objects.Length-1; i++) {
			if(i != index) {
				temp[count] = objects[i];
				count++;
			} else {
				// delete the object from the scene
				Destroy(objects[i]);
			}// if/else
		}// for
		// set the objects array to the new array
		objects = new GameObject[temp.Length];
		objects = temp;
		Debug.Log(ArrayLength());
	}// RemoveGameObject

	public void RemoveGameObjects() {
		for(int i = 0; i < objects.Length; i++) {
			Destroy(objects[i]);// destroy each object
		}// for
		// reset the objects array
		objects = new GameObject[0];
	}// RemoveGameObjects

	public GameObject[] getObjects() {
		return objects;
	}// getObjects

	public void setObjects(GameObject[] g){
		objects = g;
	}// setObjects

	public GameObject getObject(int index) {
		if(ArrayLength() > index) {
			return this.objects[index];
		} else {
			return null;
		}// if/else
	}// getObject

	public int ArrayLength() {
		// return the number of elements in the array
		return objects.Length;
	}// ArrayLength

	public void SaveState() {
		Debug.Log("Entered.");
		if(ArrayLength() > 0)
			this.GetComponent<SaveState>().Save(objects);
	}// SaveState

	public void LoadState(string s) {
		this.GetComponent<LoadState>().ParseFile(s);
	}// LoadState

}// SceneState
