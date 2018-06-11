using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : MonoBehaviour {
	public Player player { get; private set; }

	// Use this for initialization
	void Start () {
		this.player = new Player ();
		StartCoroutine (getPlayerData());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator getPlayerData(){
		string serverURL = GlobalConfig.webInterfaceRootURL;
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
}
