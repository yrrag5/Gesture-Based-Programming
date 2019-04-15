using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour {
    public static Modes modes;
    public GameObject Panel;
    int counter;

    public void GameSettings()
    {
        counter++;
        if (counter % 2 == 1)
        {
            Panel.gameObject.SetActive(false);
        }
        else
        {
            Panel.gameObject.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    // Use this for initialization
    void Start () {
        modes = Modes.getInstance;
        modes.mode = Modes.Mode.Menu;
        // open the lock on the runner
        ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
    }
	
	// Update is called once per frame
	void Update () {
        modes.mode = Modes.Mode.Menu;
        // open the lock on the runner
        ModeRunner mr = GameObject.FindObjectOfType<ModeRunner>();
    }
    
}
