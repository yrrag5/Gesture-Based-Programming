using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySaves : MonoBehaviour {

  List<string> myDropOptions = new List<string>{};
	Dropdown myDropdown;
	SceneState ss;

	// Use this for initialization
	private void Start () {
		ss = SceneState.getInstance;
		myDropdown = GetComponent<Dropdown>();
		Display();
	}
	
	public void Display() {
		string myPath = @"C:\Users\Hughballs\Documents\guiProSave";
	//  string myPath = Application.persistentDataPath;

		DirectoryInfo dir = new DirectoryInfo(myPath);
		FileInfo[] info = dir.GetFiles("*.csv");

		Debug.Log(info.Length);
		
		// List the save files in the data path.
 		for (int i = 0; i < info.Length; i++)
        {
				myDropOptions.Add(info[i].Name);
				Debug.Log("options: " + myDropOptions);				
        }
		myDropdown.AddOptions(myDropOptions);		
	}

	public void onClick() {
		int value = this.GetComponent<Dropdown>().value;
		string SelectedSave = this.GetComponent<Dropdown>().options[value].text;
		Debug.Log(SelectedSave);
		
		// Pass this name to LoadState.
		ss.LoadState(SelectedSave);
	}
}
