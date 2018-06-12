using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 * Class qui gère la création du joueur et toutes les méthodes utilisant ses propriétés 
 */ 

public class PlayerManager : MonoBehaviour {
	private Player player;

	//Crée le joueur en utilisant les infos de getPlayerData()
	public IEnumerator instanciatePlayer(){
		this.player = new Player ();
		yield return StartCoroutine (getPlayerData());
	}

	//fait une requete GET sur le serveur de l'interface web pour obtenir les information du joueur connecté
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

	//renvoie le nom complet du joueur
	public string getPlayerName(){
		return player.firstname + " " + player.surname;
	}

	//renvoie l'année de promo du joueur 
	public string getPlayerGraduatingYear(){
		return player.graduatingYear;
	}

	//renvoie le numéro d'étudiant du joueur
	public string getPlayerStudentNumber(){
		return player.studentNumber;
	}
}
