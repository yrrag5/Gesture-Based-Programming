using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class SaveSystem : MonoBehaviour {
    private static SaveSystem instance = null;
   	private SceneState sceneState;
    private GameObject gobject;
    private string myPath; 
    private string saveName;
    private int arrayLength;
    private string objectName;
    private float x;
    private float y;
    private float z;


    public void SaveScene() {
        Debug.Log("In SaveScene");
   		sceneState = (SceneState)FindObjectOfType(typeof(SceneState));

        // Array length needed for looping through array.
        arrayLength = sceneState.ArrayLength();

        // Check the number of save files for new save file name.
        // Set path to save files.        
        myPath = @"C:\Users\Hughballs\Documents\guiProSave\save1.csv";

        StreamWriter writer = new StreamWriter(myPath, true);

        // Loop through object array.
        for (int i = 0; i < (arrayLength - 1); i++) {
            Debug.Log("save loop no: " + i); 
            // Get a handle on object in array.
            gobject = sceneState.getObject(i);

            // Get a hold of the object's variables.
            objectName = gobject.name;
            x = gobject.transform.position.x;
            y = gobject.transform.position.y;
            z = gobject.transform.position.z;

            // Write object details to file.
            writer.WriteLine(objectName + ", " + x + ", " + ", " + y + ", " + z);
        }

        // Close StreamWriter.
        writer.Close();

    }

    public static SaveSystem getInstance {  
        get {   
			// Check to see if the instance is null.
			if (instance == null) {
				// Check to see if there is another instance of this class.
				instance = GameObject.FindObjectOfType<SaveSystem>();
				if (instance == null) 
					instance = new SaveSystem(); 				
			} 
			return instance;   
	    }
    } 

}

