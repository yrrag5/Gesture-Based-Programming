using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySaves : MonoBehaviour {
	SceneState ss;
    List<string> myDropOptions = new List<string>{};
	  Dropdown myDropdown;

	  private void Start () {
		  	ss = SceneState.getInstance;
		    myDropdown = GetComponent<Dropdown>();
		    Display();
	  }// Start
	
		void Display() {
				// string myPath = @"C:\Users\Hughballs\Documents\guiProSave";
				string myPath = Application.persistentDataPath;

				DirectoryInfo dir = new DirectoryInfo(myPath);
				FileInfo[] info = dir.GetFiles("*.csv");
				
				// List the save files in the data path.
				for (int i = 0; i < info.Length; i++)	{
						myDropOptions.Add(info[i].Name);
						Debug.Log("options: " + myDropOptions);				
				}// for
				myDropdown.AddOptions(myDropOptions);		
		}// Display

		public void onClick() {
				int value = this.GetComponent<Dropdown>().value;
				string SelectedSave = this.GetComponent<Dropdown>().options[value].text;
				Debug.Log(SelectedSave);
				// send it to the load scene
				ss.LoadState(SelectedSave);
		}// onClick

}// DisplaySaves
