using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

	public void Focus(Vector3 target){
         transform.LookAt(target);
    }// Focus

}// LookAt
