using UnityEngine;
using System.Collections;

public class QCMValidation : MonoBehaviour, ValidationMethod
{

	public GameObject[] slotsList;

	// Use this for initialization
	void Awake ()
	{
		slotsList = GameObject.FindGameObjectsWithTag ("DestinationSlot");
		foreach (GameObject go in slotsList) {
			print (go.name);		
		}
	}


	public bool answerIsRight(){
		bool isRightAnswer = false;
		if (checkEmptySlots ()) {
			if (checkAnswers ()) {
				Debug.Log ("c'est tout bon");
				isRightAnswer = true;

			} else {
				Debug.Log ("ya une erreur");
			}
		} else {
			Debug.Log ("manque un truc");
		}
		return isRightAnswer;
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
			ItemSlot it_s = go.GetComponentInChildren<ItemSlot> ();

			if (it.item_id != it_s.expected_id) {
				Debug.Log (it.item_id + " est au mauvais endroit(" + it_s.expected_id + ")");
				returnCheck = false;
			}
		}
		return returnCheck;
	}
}

