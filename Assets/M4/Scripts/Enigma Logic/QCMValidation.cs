using UnityEngine;
using System.Collections;

public class QCMValidation : MonoBehaviour, ValidationMethod
{

	public GameObject[] slotsList;

	// Use this for initialization
	void Awake ()
	{
		Transform list = GameObject.Find("Destination Slots").transform;
        int count = list.childCount;
        slotsList = new GameObject[count];
        for (int i=0;i<count;i++)
        {
            slotsList[i]= list.GetChild(i).gameObject;
        }
		//foreach (GameObject go in slotsList) {
		//	print (go.name);		
		//}
	}

    public float score()
    {
		return (answerIsRight()) ? 100F : 0F;
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

	bool checkEmptySlots(){
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


	bool checkAnswers(){
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

