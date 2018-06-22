using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.Remoting;
using System;

public class ScoreManager : MonoBehaviour {
	private List<ScoreData> localScoreList;
	// Use this for initialization
	void Start () {
		localScoreList = new List<ScoreData> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addScore(ScoreData score){
		localScoreList.Add (score);
	}

	public IEnumerator postScoreToServer(ScoreData score){
		if (score == null) {
			throw new ArgumentNullException("Score is null");
		}
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
			addScore (score);
			throw new ServerException ("The score could not be saved : " + www.error);
		}
		else {
			Debug.Log("Form upload complete! : Score Sent");
		}
	}

	public void sendLocalScoresToServer(){
		List<ScoreData> tempList = localScoreList;
		localScoreList = new List<ScoreData> ();
		foreach (ScoreData score in tempList) {
			saveScoreToServer (score);
		} 
	}

	public void saveScoreToServer(ScoreData score){
		try{
			StartCoroutine(postScoreToServer (score));
		} catch (ServerException exception){
			print ("GlobalManager : " + exception.Message);
		} catch (ArgumentNullException exception){
			print ("GlobalManager : " + exception.Message);
		}
	}
}
