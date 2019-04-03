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
        public GameObject obj1;
        public GameObject obj2;
        public GameObject obj3;
        public GameObject obj4;
        public GameObject obj5;
    }

    public void Save(GameInProgress saveGame)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(FileName, FileMode.Create);
        List<Objects> newDataCollection = new List<Objects>();
        for (int i = 0; i < Object.Length; i++)
        {
            Objects newData = new Objects();

          /*  newData.obj1 = Object[i].transform.position.x;
            newData.obj2 = Object[i].transform.position.x;
            newData.obj3 = Object[i].transform.position.x;
            newData.obj4 = Object[i].transform.position.x;
            newData.obj5 = Object[i].transform.position.x;*/
        }// for
        bf.Serialize(file, newDataCollection);
        file.Close();
        Debug.Log("Game Saved");
    }

   /* public void Load()
    {
        if (File.Exists(FileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(FileName, FileMode.Create);

            Objects newData1 = (Objects)bf.Deserialize(file);
            for(int i = 0; i < Object.Length; i++)
            {

                Object[i].transform.position = new Vector4(newData1.obj1, newData1.obj2, newData1.obj3, newData1.obj4, newData1.obj5);
            }
            file.Close();
        }

    }*/
}


