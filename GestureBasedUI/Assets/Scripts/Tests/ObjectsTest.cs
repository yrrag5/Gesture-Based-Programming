using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsTest : MonoBehaviour {
	[SerializeField]
	GameObject parent;
	SceneState sceneState;

	// Use this for initialization
	void Start () {
		// getting a handle on the scene state
		sceneState = (SceneState)FindObjectOfType(typeof(SceneState));
		// iterate over each child object and add them to the scene state
		foreach (Transform item in parent.transform)
      	{
        	sceneState.AddGameObject(item.gameObject);
      	}// foreach
	}// Start
	
}// ObjectsTest
