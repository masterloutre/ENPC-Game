﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;

public class ConnectionDataControl : MonoBehaviour
{

    public static ConnectionDataControl control;

    public string phasePassword;
    public string studentToken;

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

    void OnGUI()
    {
        GUI.Label(new Rect(30, 70, 150, 30), "password" + phasePassword);
    }

    public string getPasswordJSON()
    {
        string placeholder = JsonUtility.ToJson(phasePassword);
        print("password json: " + placeholder);
        return placeholder;
    }

    public bool checkPhasePassword()
    {
        bool isPassword = false;
        if (phasePassword == "test") isPassword = true;
        return isPassword;
    }

    public void Save()
    {
        StreamWriter file = new StreamWriter(Application.persistentDataPath + "/connectionInfo.dat");
        //print(Application.persistentDataPath);
        //DataUploader.du.Upload();
        file.WriteLine(getPasswordJSON());
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
