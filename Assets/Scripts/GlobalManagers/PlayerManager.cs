using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
 * Class qui gère la création du joueur et toutes les méthodes utilisant ses propriétés 
 */ 

public class PlayerManager : MonoBehaviour {
	private PlayerData player;

	//Crée le joueur en utilisant les infos de getPlayerData()
	public IEnumerator instanciatePlayer(){
		this.player = new PlayerData ();
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
            // apparemment des caractères invisibles apparaissent dans le json récupéré. Il s'agit du "zero-width no-break space", de valeur 65279
            int antiespace = 0;
            while(getRequest.downloadHandler.text[antiespace]== 65279)
            {
                Debug.LogError("-"+(int)getRequest.downloadHandler.text[0]);
                antiespace++;
                
            }
            string result = getRequest.downloadHandler.text.Substring(antiespace, getRequest.downloadHandler.text.Length - (antiespace));
            Debug.LogError(antiespace);
            Debug.LogError(result);
            
            Debug.LogError("Parsing Player data ...");
            Debug.LogError(result);
            JsonUtility.FromJsonOverwrite(result, this.player);
            Debug.LogError("Parsing Complete");

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

	public int getPlayerId(){
		return player.id;
	}
}
