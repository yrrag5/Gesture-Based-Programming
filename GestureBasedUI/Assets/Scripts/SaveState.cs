using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveState : MonoBehaviour {
	string PATH;
	[SerializeField]
	DisplaySaves ds;
	void Start(){
		PATH = Application.persistentDataPath;
	}// Start

	public string NextSaveFile() {
		int counter = 0;
		// get a handle on the directory
		DirectoryInfo dir = new DirectoryInfo(PATH);
		FileInfo[] info = dir.GetFiles("*.csv");
		// string array
		int[] numbers = new int[info.Length]; 

		// remove the "save" and ".csv" from the filename
		foreach(FileInfo f in info) {
			// remove the "save"
			string replace = "save";
			string s = f.Name.Replace(replace, "");
			// remove the ".csv"
			replace = ".csv";
			s = s.Replace(replace, "");
			// add the next int
			numbers[counter] = int.Parse(s);
			counter++;
		}// foreach

		int max = 0;

		if(numbers.Length > 0)
			max = numbers[0];

		// loop through the ints
		for (int i = 1; i < numbers.Length; i++) {
			// if new max is found
			if(max < numbers[i]) 
				max = numbers[i];
		}// for

		// take the number of the save and increment it
		max++;
		// construct the fileName string and return it
		return("save" + max + ".csv");
	}// NextSaveFile

	public void Save(GameObject[] objects) {
		// determine the name of the save file
		string fileName = NextSaveFile();
		// get a handle on the application path and add the filename to save to
		string path = PATH + "/" + fileName;

		// open the file stream
		StreamWriter writer = new StreamWriter(path, true);
		
		// parse through the game objects array
		// save each object to the file, on a new line
		foreach(GameObject g in objects) {
            // Write object details to file.
            writer.WriteLine(g.name + "," + g.transform.position.x + "," + 
				g.transform.position.y + "," + g.transform.position.z);
		}// foreach

        // Close StreamWriter.
        writer.Close();
		Debug.Log("Saved to: " + path);
    }// Save

}// SaveState
