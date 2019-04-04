using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGameUI : MonoBehaviour {
	// axis text component
	public Text axis;
	// user notifications
	public Text message;

	public void ToggleAxisText(bool visible) {
		axis.gameObject.SetActive(visible);
	}// ToggleAxisText

	public void UpdateAxisText(string text){
		axis.text = text;
	}// UpdateAxisText

	public void UpdateMessageText(string text){
		message.text = text;
	}// UpdateAxisText

}// UpdateGameUI
