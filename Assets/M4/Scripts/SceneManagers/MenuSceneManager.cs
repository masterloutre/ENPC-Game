using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour {
	public int choice = 0;

	public void nextScene(){
		EventManager.instance.Raise (new RequestNextSceneEvent(gameObject.scene.name, choice));
	}
}
