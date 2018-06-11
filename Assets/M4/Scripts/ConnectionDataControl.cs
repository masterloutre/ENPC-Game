using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

public class ConnectionDataControl : MonoBehaviour
{

    public static ConnectionDataControl control;
    public string studentToken;
    public lancement_jeu lancementjeu;
    public int checkConnectionCode;
    public string responseStr;

    void Awake()
    {
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

    public string getLancementJeuJson()
    {
        string json = JsonUtility.ToJson(lancementjeu);
        print("lancement password json: " + lancementjeu.mdp);
        //json = "EN45pO";
        return (json);
    }

    public bool checkPhasePassword()
    {
        bool isPassword = false;
        print("waley: "+DataUploader.du.url);
        //DataUploader.du.checkConnectionData();
        DataUploader.du.StartCoroutine(DataUploader.du.checkConnectionData());
        print("response code = " + checkConnectionCode);
        print("response string = " + responseStr);
        if (checkConnectionCode == 1)
        {
            isPassword = true;
            print("yes it's true");
        }
        return isPassword;
    }

    public bool checkLoginInfo()
    {
        bool correctInfo = false;
        DataUploader.du.checkLoginData("test");
        print("response code = " + checkConnectionCode);
        print("response string = " + responseStr);
        if (checkConnectionCode == 1) correctInfo = true;
        return correctInfo;
    }

    public void SaveConnection()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/connectionInfo.dat");
        //print(Application.persistentDataPath);
        //DataUploader.du.Upload();
        file.WriteLine(getLancementJeuJson());
        file.Close();
    }
    public void Save()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/connectionInfo.dat");
        //print(Application.persistentDataPath);
        //DataUploader.du.Upload();
        file.WriteLine(getLancementJeuJson());
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/connectionInfo.dat"))
        {
            //get content of json string
            StreamReader file = new StreamReader(Application.persistentDataPath + "/connectionInfo.dat");
            string json = file.ReadToEnd();
            //deserialize json string
            file.Close();
            //save the loaded data in the DataControl
        }
    }
}
/*
[Serializable]
class PlayerData
{
    public int temps;
    public int bonnes_reponses;
}*/
