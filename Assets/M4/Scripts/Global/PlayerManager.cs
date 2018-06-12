using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : MonoBehaviour {
	//public Player player { private get; private set; }
	private Player player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator instanciatePlayer(){
		this.player = new Player ();
		yield return StartCoroutine (getPlayerData());
	}

	IEnumerator getPlayerData(){
		string serverURL = GlobalManager.webInterfaceRootURL;
		UnityWebRequest getRequest = UnityWebRequest.Get (serverURL + "/index.php?action=qui-joue");
		yield return getRequest.SendWebRequest();

		if(getRequest.isNetworkError || getRequest.isHttpError) {
			Debug.Log(getRequest.error);
			Debug.Log(getRequest.downloadHandler.text);

		}
		else {
			Debug.Log(getRequest.downloadHandler.text);
			JsonUtility.FromJsonOverwrite(getRequest.downloadHandler.text, this.player);

		}
	}

	public string getPlayerName(){
		return player.firstname + " " + player.surname;
	}

	public string getPlayerGraduatingYear(){
		return player.graduatingYear;
	}

	public string getPlayerStudentNumber(){
		return player.studentNumber;
	}
}
