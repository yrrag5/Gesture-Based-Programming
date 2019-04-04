using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
    public static Modes modes;
    public float heightChange;
    public float distanceChange;

	public void Focus(Vector3 target){
        // gets a handle on the singleton instance
		modes = Modes.getInstance;
        
        // if we are in the create/select mode
        if(modes.mode == Modes.Mode.Create){
            // setting the change factors
            heightChange = 1.5f;
            distanceChange = -15;
        } else if(modes.mode == Modes.Mode.Select) {
            // setting the change factors
            heightChange = 1.5f;
            distanceChange = -10f;
        }// else if

        // move to the target object
        this.transform.position = new Vector3(target.x - distanceChange, target.y, target.z);
        // move above the object
        this.transform.Translate(this.transform.up * heightChange);
        // look at the object
        transform.LookAt(target);
    }// Focus

}// LookAt
