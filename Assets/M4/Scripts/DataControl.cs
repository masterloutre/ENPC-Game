using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataControl : MonoBehaviour {

	public static DataControl control;

    public int temps;
    public int bonnes_reponses;
		public string nom;

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
        GUI.Label(new Rect(30, 10, 150, 30), "Temps : " + temps);
        GUI.Label(new Rect(30, 25, 150, 30), "Bonnes réponses : " + bonnes_reponses);
    }

		public string getJSON()
		{
			Save data = new Save();
			ScoreEnigme score = new ScoreEnigme();
			score.nom = nom;

			return JsonUtility.ToJson(data);
		}

    public void Save()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/playerInfo.dat");
				//print(Application.persistentDataPath);

				DataUploader.du.Upload();

				file.WriteLine(getJSON());
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
						StreamReader file = new StreamReader(Application.persistentDataPath + "/playerInfo.dat");
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
