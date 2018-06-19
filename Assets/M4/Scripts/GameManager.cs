using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject[] EnigmaList;
	int currentEnigmaIndex;

	public GameObject estimatedTimeGO;
	public GameObject descriptionGO;
	public GameObject enigmaTitleGO;
	public GameObject enigmaTypeGO;
	public GameObject enigmaDifficultyGO;
	public GameObject timerGO;

	// Use this for initialization
	void Start () {
        //DataControl.control.Load();
		if (EnigmaList.Length > 0) {
			currentEnigmaIndex = 0;
			EnigmaList [0].SetActive (true);

			for (int i = 1; i < EnigmaList.Length; i++) {
				EnigmaList [i].SetActive (false);
			}
		}

		updateDatas ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void changeEnigma(){
        //DataControl.control.Save();
        EnigmaList [currentEnigmaIndex].SetActive (false);
		currentEnigmaIndex++;
		if (currentEnigmaIndex >= EnigmaList.Length)
			currentEnigmaIndex = 0;
		EnigmaList [currentEnigmaIndex].SetActive (true);
		disableDescription ();
		updateDatas ();
		resetTimer ();
        DataControl.control.resetScore();

    }

	public void resetTimer(){
		timerGO.GetComponent<Timer> ().resetTimer ();
	}


	public void enigmaCallValidation(){
		if (EnigmaList [currentEnigmaIndex].GetComponentInChildren<InputAnswer> () != null)
			EnigmaList [currentEnigmaIndex].GetComponentInChildren<InputAnswer> ().validation ();
		else if (EnigmaList [currentEnigmaIndex].GetComponentInChildren<AlgoAnswer> () != null)
			EnigmaList [currentEnigmaIndex].GetComponentInChildren<AlgoAnswer> ().validation ();
	}


	public void updateDatas(){
		estimatedTimeUpdate ();
		enigmaUpdate ();
	}

	public void estimatedTimeUpdate(){
		string estimatedTime = EnigmaList [currentEnigmaIndex].GetComponent<Enigme_Data> ().enigmaEstimatedTime;
		estimatedTimeGO.GetComponent<UnityEngine.UI.Text> ().text = estimatedTime;
	}


	public void enigmaUpdate(){
		string description = EnigmaList [currentEnigmaIndex].GetComponent<Enigme_Data> ().enigmaDescription;
		descriptionGO.GetComponent<UnityEngine.UI.Text> ().text = description;

		string title = EnigmaList [currentEnigmaIndex].GetComponent<Enigme_Data> ().enigmaTitle;
		enigmaTitleGO.GetComponent<UnityEngine.UI.Text> ().text = title;

		string type = EnigmaList [currentEnigmaIndex].GetComponent<Enigme_Data> ().enigmaType.ToString();
		enigmaTypeGO.GetComponent<UnityEngine.UI.Text> ().text = type;

		string diff = EnigmaList [currentEnigmaIndex].GetComponent<Enigme_Data> ().enigmaDifficulty.ToString();
		enigmaDifficultyGO.GetComponent<UnityEngine.UI.Text> ().text = diff;
	}


	public void disableDescription(){
		descriptionGO.SetActive (false);
	}
		
}
