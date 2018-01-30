using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataControl : MonoBehaviour {

	public static DataControl control;

    public float temps;
    public int enigmaId;
    public int tentatives;
    public int points;
    public bool aideExt;
    public int enigmaMaxAttempts;
    public bool enigmeReussie;
    public Save save;
    public bool enigmaTimesDone; //relances enigme

    public int sceneToLoad;
    public List<Enigme_Data> enigmaDatas;

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
        GUI.Label(new Rect(30, 40, 150, 30), "tentatives : "+ tentatives);
    }

	public string getJSON()
	{

        Save data = new Save();
        //Add the updated score to the Save
        ScoreEnigme score = new ScoreEnigme();
        score.enigmaId = enigmaId;
        score.tentatives = tentatives;
        score.temps = Math.Round(temps,2);
        score.aideExt = aideExt;
        score.enigmeReussie = enigmeReussie;
        List<ScoreEnigme> currentScores = save.Scores();
        currentScores.Add(score);
        data.setScores(currentScores);
        save = data;
        bool prettyPrint = true;
        return JsonHelperList.ToJson(currentScores, prettyPrint); //serializing the list using a custom JsonHelper, adapting JsonUtility for Lists
        
    }

    public void Save()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/playerInfo.dat");
				//print(Application.persistentDataPath);
        //DataUploader.du.Upload();
				file.WriteLine(getJSON());
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            //get content of json string
            StreamReader file = new StreamReader(Application.persistentDataPath + "/playerInfo.dat");
            string json = file.ReadToEnd();
            //JsonUtility.FromJsonOverwrite(json, data);
            //deserialize json string and put its content in List of ScoreEnigme
            List<ScoreEnigme> loadedScores = new List<ScoreEnigme>();
            loadedScores = JsonHelperList.FromJson<ScoreEnigme>(json);
            file.Close();
            //save the loaded data in the DataControl
            save.scores = loadedScores;
        }
    }

    public void LoadEnigmaDatas()
    {
        if (File.Exists(Application.persistentDataPath + "/enigmaData.dat"))
        {
            //get content of json string
            StreamReader file = new StreamReader(Application.persistentDataPath + "/enigmaData.dat");
            string json = file.ReadToEnd();
            //deserialize json string and put its content in List of Enigme_Data
            List<Enigme_Data> loadedDatas = new List<Enigme_Data>();
            loadedDatas = JsonHelperList.FromJson<Enigme_Data>(json);
            file.Close();
            //save the loaded data in the DataControl
            enigmaDatas = loadedDatas;
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
