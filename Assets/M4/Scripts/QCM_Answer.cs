using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QCM_Answer : MonoBehaviour {


	public GameObject[] slotsList;
	public GameObject victoryTimeline;
	public GameObject errorTimeline;

	// Use this for initialization
	void Start () {
		
	}


	public void validation(){
        //mettre à jour le nombre de tentatives
        ScoreControl.validationRequest();
        bool maxAttemptsReached = ScoreControl.maxAttempsReached();
        bool isRightAnswer = false;
        if (checkEmptySlots ()) {
			if (checkAnswers ()) {
				Debug.Log ("c'est tout bon");
				victoryTimeline.SetActive (true);
                isRightAnswer = true;

            } else {
				Debug.Log ("ya une erreur");
				if(errorTimeline != null)
					errorTimeline.SetActive (true);
			}
		}
		else
			Debug.Log ("manque un truc");

        if ( isRightAnswer || maxAttemptsReached)
        {
            //quitter l'énigme
            ScoreControl.Success(isRightAnswer);
            EnigmaManager.enigmaEnd();
        }
    }

	public bool checkEmptySlots(){
		bool returnCheck = true;
		foreach( GameObject go in slotsList){
			
			Item it = go.GetComponentInChildren<Item>();
			if(it == null){
				ItemSlot it_s = go.GetComponent<ItemSlot> ();
				if(it_s != null)
					it_s.glowMissingItem ();
				returnCheck = false;
			}
		}
		return returnCheck;
	}


	public bool checkAnswers(){
		bool returnCheck = true;
		foreach (GameObject go in slotsList) {
			Item it = go.GetComponentInChildren<Item>();
			ItemSlot it_s = go.GetComponent<ItemSlot> ();

			if (it.item_id != it_s.expected_id) {
				Debug.Log (it.item_id + " est au mauvais endroit(" + it_s.expected_id + ")");
				returnCheck = false;
			}
		}
		return returnCheck;

	}
}
