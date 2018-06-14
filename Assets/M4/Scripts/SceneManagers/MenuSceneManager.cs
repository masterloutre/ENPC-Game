using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour {
	public int choice = 0;
	public void nextMenu(){
		EventManager.instance.Raise (new RequestNextMenuEvent(gameObject.scene.name, choice));
	}
}
