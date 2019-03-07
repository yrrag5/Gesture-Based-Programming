using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour {
    public static Modes modes;

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
