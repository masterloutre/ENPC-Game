using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

	public void InfoChangeState(GameObject go){

		go.SetActive (!go.activeSelf);

	}
}
