using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController {

    public static int number = 0;
    public Text numText;
    public static List<string> theObjects = new List<string>();
    public InputField inputField;

    public void SaveGame()
    {
        GameInProgress gameToSave = new GameInProgress();
        SaveLoad.Save(gameToSave);
    }

	
}
