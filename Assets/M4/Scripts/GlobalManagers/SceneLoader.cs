using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Class qui gère les méthodes de load des scènes
 * */
public class SceneLoader : MonoBehaviour {

	//les méthodes retournant des IEnumerator peuvent être utilisées dans des coroutines et donc dans une séquence d'action asynchrones

	//load la page d'acceuil
	public IEnumerator loadLandingPage(){
		SceneManager.LoadScene("HomeScene", LoadSceneMode.Single);
		yield break;
	}

	//load la page de selection des compétences 
	public IEnumerator loadSkillsMenu(){
		Debug.Log ("Scene Loader load skills");
		SceneManager.LoadScene("SelectionScene", LoadSceneMode.Single);
		yield break;
	}


}
