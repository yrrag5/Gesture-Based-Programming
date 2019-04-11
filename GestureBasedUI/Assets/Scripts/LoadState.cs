using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadState : MonoBehaviour {
	private GameObject[] objects;
	private GameObject cube;
	[SerializeField]
	private GameObject cuboid;
	[SerializeField]
	private GameObject cylinder;
	int counter = 0;// counter
	public Canvas gameUI;// Canvas object

	public void ParseFile(string fileName) {
		string line;// temp string storage
		// get a handle on the application path and add 
		string path = Application.persistentDataPath + fileName;
		// access the file input stream
		//Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
		// string list to store the read lines
		List<string> stringList = null;

		// parse the file, until null
		while ((line = reader.ReadLine()) != null) {
			stringList.Add(reader.ReadLine());
			counter++;
		}// while

		// set the size of the object array based on the counter
		objects = new GameObject[counter];
		LoadData(stringList);

		// set the UI message
		gameUI.gameObject.GetComponent<UpdateGameUI>().UpdateMessageText("Game state successfully loaded.");

		// close the file
		reader.Close();
	}// PaserFile

	public void LoadData(List<string> data) {
		int counter = 0;
		// get a handle on the scene state
		SceneState ss = SceneState.getInstance;

		// loop throught the string list
		foreach(string s in data) {
			ExtractObject(s);
			counter++;
		}// foreach

		// remove all objects from the scene state
		if(ss.ArrayLength() > 0)
			ss.RemoveGameObjects();
		// set the new instanciated objects 
		ss.setObjects(objects);
	}// LoadData

	public void ExtractObject(string s) {
		// split the string by commas
		string[] data = s.Split(',');
		// first name, x, y, z
		InstanciateObject(data[0], float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
	}// ExtractObject

	public void InstanciateObject(string name, float x, float y, float z) {
		// using the name to choose prefab to instanciate
		GameObject g;
		if(name == "cuboid") g = cuboid;
		else if(name == "cube") g = cube;
		else g = cylinder;

		// instanciate the object
		GameObject newObj = (GameObject)Instantiate(g, new Vector3(x, y, z), Quaternion.identity);
		// add the object to the objects array
		objects[counter] = newObj;
	}// InstanciateObject

}// main
