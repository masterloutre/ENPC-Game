using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour {

	public void PreviousScene(){
		EventManager.instance.Raise (new RequestPreviousSceneEvent(gameObject.scene.name, 0));
	}
}
