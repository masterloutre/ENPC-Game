using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Class qui gère les méthodes de load des scènes
 * */
using UnityEngine.Events;


public class SceneLoader : MonoBehaviour {

	// les méthodes retournant des IEnumerator peuvent être utilisées dans des coroutines et donc dans une séquence d'action asynchrones

	public void Awake(){
		//à chaque fois que l'event sceneLoaded est appelé pour une scène, elle effectue la methode setActive sur cette scene
		SceneManager.sceneLoaded += setActive;
	}

	// Load une scene d'après son nom
	public void loadScene(string sceneName){
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

    // Unload une scene d'après son nom
    public IEnumerator  removeScene(string sceneName){
		AsyncOperation async = SceneManager.UnloadSceneAsync(sceneName);
		while (!async.isDone) {
			yield return null;
		}
	}

	// Load une scene de façon additive (en mémoire), la scène ne deviend pas la scène active
	public IEnumerator addScene(string sceneName){
		SceneManager.sceneLoaded -= setActive;
		//SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
		//yield break;
		while (!async.isDone) {
			//Debug.Log ("loading");
			yield return null;
		}
		SceneManager.sceneLoaded += setActive;
	}

	// Rend active la scène qui vient d'être chargée en mémoire
	void setActive(Scene scene, LoadSceneMode mode){
		SceneManager.SetActiveScene(scene);
		Debug.Log ("---------------------  " + scene.name + " is now active");
	}

	// Load la page d'acceuil
	public IEnumerator loadLandingPage(){
		loadScene("HomeScene");
		yield break;
	}

	// Load la page de selection des compétences 
	public IEnumerator loadSkillsMenu(){
		loadScene("SelectionScene");
		yield break;
	}
    // Charge de nouvelles énigmes en rapport avec skill
	public IEnumerator loadEnigmaSequence (Skill skill){

        // Appel du EnigmaSequenceManager existant pour loader une nouvelle série d'énigme en rapport avec skill 
        UnityAction<UnityEngine.SceneManagement.Scene,UnityEngine.SceneManagement.LoadSceneMode> setSkill = delegate (Scene scene, LoadSceneMode mode) {
		    //if(scene.name == "EnigmaSequenceScene"){
		    EnigmaSequenceManager esm = GameObject.Find("MainManager").GetComponent<EnigmaSequenceManager>();
		    esm.updateEnigmaSequence(skill);
		    //}
		};

		SceneManager.sceneLoaded += setSkill;
		loadScene ("EnigmaSequenceScene");
		SceneManager.sceneUnloaded += delegate (Scene scene) {
			if(scene.name == "EnigmaSequenceScene"){
				//Debug.Log(scene.name + ", scene unloaded : removes setSkill");
				SceneManager.sceneLoaded -= setSkill;
			}
		};
		//REFAIR AVEC DES LOAD ASYNC ET DES COROUTINES
		yield break;
	}

    // Ajout additive de la scène de l'énigme EnigmaX dans le SceneManager
	public IEnumerator loadEnigma(int unityIndex){
		//Debug.Log ("Scene loader : load enigma");
		yield return StartCoroutine(addScene("Enigma" + unityIndex));
		//loadScene ("Enigma" + unityIndex);
		//StartCoroutine(addScene("Enigma" + unityIndex));
		//yield break;
		
	}
    // Suppression de la scène de l'énigme EnigmaX dans le SceneManager
    public IEnumerator unloadEnigma(int unityIndex){
		yield return StartCoroutine(removeScene("Enigma" + unityIndex));
	}


}
