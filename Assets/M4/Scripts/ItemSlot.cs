using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour {

	public int expected_id;


	public bool isItemCorrect(int item_id){
		if (expected_id == item_id)
			return true;
		else
			return false;
	}

	public void glowMissingItem(){
		Debug.Log ("glow " + this.transform.name);
	}


}
