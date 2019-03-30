using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad: MonoBehaviour {

    public Transform[] Object;
    public string FileName = "SaveScene.txt";

    [System.Serializable]
    class Objects
    {
        public float obj1;
        public float obj2;
        public float obj3;
        public float obj4;
        public float obj5;

    }

    public void Save(GameInProgress saveGame)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(FileName, FileMode.Create);
        List<Objects> newDataCollection = new List<Objects>();
        for (int i = 0; i < Object.Length; i++)
        {
            Objects newData = new Objects();

            newData.obj1 = Object[i].transform.position.x;
            newData.obj2 = Object[i].transform.position.x;
            newData.obj3 = Object[i].transform.position.x;
            newData.obj4 = Object[i].transform.position.x;
            newData.obj5 = Object[i].transform.position.x;

        }
        bf.Serialize(file, newDataCollection);
        file.Close();
        Debug.Log("Game Saved");
    }

    public static void Load()
    {
        if (File.Exists(FileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(FileName, FileMode.Open,FileAccess)
        }
    }
}

