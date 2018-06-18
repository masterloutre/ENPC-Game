using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Class qui gère les méthodes de load des scènes
 * */
using UnityEngine.Events;


public class SceneLoader : MonoBehaviour {

	//les méthodes retournant des IEnumerator peuvent être utilisées dans des coroutines et donc dans une séquence d'action asynchrones

	public void Awake(){
		//à chaque fois que l'event sceneLoaded est appelé pour une scène effectue la methode setActive sur cette scene
		SceneManager.sceneLoaded += setActive;
	}

	//load une scene
	public void loadScene(string sceneName){
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

	//load une scene de façon additive, la scene ne deviendra pas la scène active
	public void addScene(string sceneName){
		SceneManager.sceneLoaded -= setActive;
		SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		SceneManager.sceneLoaded += setActive;
	}

	//La scène qui vient d'être loadée devient la scene active
	void setActive(Scene scene, LoadSceneMode mode){
		SceneManager.SetActiveScene(scene);
		Debug.Log (scene.name + " is now active");
	}

	//load la page d'acceuil
	public IEnumerator loadLandingPage(){
		loadScene("HomeScene");
		yield break;
	}

	//load la page de selection des compétences 
	public IEnumerator loadSkillsMenu(){
		loadScene("SelectionScene");
		yield break;
	}

	public IEnumerator loadEnigmaSequence (Skill skill){
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
				Debug.Log(scene.name + ", scene unloaded : removes setSkill");
				SceneManager.sceneLoaded -= setSkill;
			}
		};
		//REFAIR AVEC DES LOAD ASYNC ET DES COROUTINES
		yield break;
	}

	IEnumerator loadEnigma(int unityIndex){
		yield break;
		
	}

	IEnumerator unLoadEnigma(int unityIndex){
		yield break;
	}

		
		


}
