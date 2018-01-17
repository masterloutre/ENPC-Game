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
    public int tentatives;
    public int points;
    public bool aideExt;
    public Save save;

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
        //afficher le nombre de tentatives
        GUI.Label(new Rect(30, 40, 150, 30), "tentatives : "+ tentatives);
    }

		public string getJSON()
		{

        Save data = new Save();
        //Add the updated score to the Save
        ScoreEnigme score = new ScoreEnigme();
		score.nom = nom;
        score.tentatives = tentatives;
        List<ScoreEnigme> currentScores = new List<ScoreEnigme>();
        currentScores = save.Scores();
        currentScores.Add(score);
        data.setScores(currentScores);
        save = data;
        return JsonHelperList.ToJson(currentScores); //serializing the list using a custom JsonHelper, adapting JsonUtility for Lists
        //return JsonUtility.ToJson(score);
    }

    public void Save()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/playerInfo.dat");
				print(Application.persistentDataPath);
        //DataUploader.du.Upload();
				file.WriteLine(getJSON());
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
			StreamReader file = new StreamReader(Application.persistentDataPath + "/playerInfo.dat");
            String json = file.ReadToEnd();
            /*PlayerData data = new PlayerData();*/
            /*Save data = new Save();
            JsonUtility.FromJsonOverwrite(json, data);*/
            file.Close();

            //save = data;
            //tentatives = data.Scores().tentatives;
            /*temps = (int) data.temps;
            bonnes_reponses = (int) data.bonnes_reponses;*/

        }
    }

    public void resetScore()
    {
        tentatives = 0;
        points = 0;
        temps = 0;
        aideExt = false;
    }
}

[Serializable]
class PlayerData
{
    public int temps;
    public int bonnes_reponses;
}
