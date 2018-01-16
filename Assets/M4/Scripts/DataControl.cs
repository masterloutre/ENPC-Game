using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataControl : MonoBehaviour {

	public static DataControl control;

    public int temps;
    public int bonnes_reponses;

    void Awake () {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(100, 10, 150, 30), "Temps : " + temps);
        GUI.Label(new Rect(100, 40, 150, 30), "Bonnes réponses : " + bonnes_reponses);
    }

    public void Save()
    {
        //BinaryFormatter bf = new BinaryFormatter();
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/playerInfo.dat");
				print(Application.persistentDataPath);

        PlayerData data = new PlayerData();
        data.temps = temps;
        data.bonnes_reponses = bonnes_reponses;

        string json = JsonUtility.ToJson(data);
				file.WriteLine(json);

        //bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            //PlayerData data = (PlayerData)bf.Deserialize(file);
						StreamReader file = new StreamReader(Application.persistentDataPath + "/playerInfo.dat");
						print(Application.persistentDataPath);
            String json = file.ReadToEnd();
            PlayerData data = new PlayerData();
            JsonUtility.FromJsonOverwrite(json, data);
            file.Close();

            temps = (int) data.temps;
            bonnes_reponses = (int) data.bonnes_reponses;
        }

    }
}

[Serializable]
class PlayerData
{
    public int temps;
    public int bonnes_reponses;
}
