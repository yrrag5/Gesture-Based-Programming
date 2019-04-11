using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SaveSystem : MonoBehaviour {
    private string savePath; 
    private string saveName;

    public void SaveScene() {
		// Allows the user to save the current game object array with a personalised file name.
        // Application.persistentDataPath is a variable that returns a location where Unity can store data from the project.
//        dataPath = Path.Combine(Application.persistentDataPath, saveName + ".csv");

        // Prints the name, x, y & z coordinates of each game object to the save file.
        
    }

    public void LoadScene(int saveNo) {
        // Displays save files in a directory. Allows the user to select and load them.
        
        // Check if file exists. 

        // Path to file, using the integer passed from menu mode.
 //       Filesteam file = file.Open(Application.persistentDataPath, saveName + saveNo + ".csv", FileMode.Open);

        // Instantiates the GameObject array with the data in the save file.

        
    }

}

