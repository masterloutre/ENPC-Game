﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour {
	private List<ScoreData> scores;
	// Use this for initialization
	void Start () {
		scores = new List<ScoreData> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void addScore(ScoreData score){
		scores.Add (score);
	}

	IEnumerator sendScore(ScoreData score){
		List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
		formData.Add( new MultipartFormDataSection("id_enigme", score.id_enigme.ToString()));
		formData.Add( new MultipartFormDataSection("id_etudiant", score.id_etudiant.ToString()));
		formData.Add( new MultipartFormDataSection("points", score.points.ToString()));
		formData.Add( new MultipartFormDataSection("tentatives", score.tentatives.ToString()));
		formData.Add( new MultipartFormDataSection("temps", score.temps.ToString()));
		formData.Add( new MultipartFormDataSection("aide", score.aide.ToString()));

		string serverURL = GlobalManager.webInterfaceRootURL;
		UnityWebRequest www = UnityWebRequest.Post(serverURL + "/index.php?action=envoyer-score", formData);
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log("Form upload complete!");
		}
	}
}
