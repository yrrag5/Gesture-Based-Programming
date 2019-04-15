using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadState : MonoBehaviour {
	private GameObject[] objects;
	[SerializeField]
	private GameObject cube;
	[SerializeField]
	private GameObject cuboid;
	[SerializeField]
	private GameObject cylinder;
	int counter = 0;// counter
	public Canvas gameUI;// Canvas object

	public void ParseFile(string fileName) {
		string line;// temp string storage
		// get a handle on the application path and add the filename
		string path = Path.Combine(Application.persistentDataPath, fileName);
		// access the file input stream
        StreamReader reader = new StreamReader(path);
		// string list to store the read lines
		List<string> stringList= new List<string>();

		// parse the file, until null
		while ((line = reader.ReadLine()) != null) {
			stringList.Add(line);
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
		// get a handle on the scene state
		SceneState ss = SceneState.getInstance;
		// reset the counter
		counter = 0;
		// loop throught the string list
		data.ForEach(delegate(string s) {
			if(s != null)
				ExtractObject(s);
		});
		
		// remove all objects from the scene state
		if(ss.ArrayLength() > 0)
			ss.RemoveGameObjects();
		// set the new instanciated objects 
		ss.setObjects(objects);
	}// LoadData

	public void ExtractObject(string s) {
		Debug.Log(s);
		string[] data = new string[4];
		// split the string by commas
		data = s.Split(',');
		// first name, x, y, z
		InstanciateObject(data[0], float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
		// increment the counter
		counter++;
	}// ExtractObject

	public void InstanciateObject(string name, float x, float y, float z) {
		// using the name to choose prefab to instanciate
		GameObject g;
		if(string.Compare(name, cuboid.name) == 0) g = cuboid;
		else if(string.Compare(name, cube.name) == 0) g = cube;
		else if(string.Compare(name, cylinder.name) == 0) g = cylinder;
		else g = null;

		// instanciate the object
		GameObject newObj = (GameObject)Instantiate(g, new Vector3(x, y, z), Quaternion.identity);

		// removing (clone) from new obj name
		if(string.Compare(name, cuboid.name) == 0) newObj.name = cuboid.name;
		else if(string.Compare(name, cube.name) == 0) newObj.name = cube.name;
		else if(string.Compare(name, cylinder.name) == 0) newObj.name = cylinder.name;
		// add the object to the objects array
		objects[counter] = newObj;
	}// InstanciateObject

}// main
