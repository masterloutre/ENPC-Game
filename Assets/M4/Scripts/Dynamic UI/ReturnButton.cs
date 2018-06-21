using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour {

	public void PreviousScene(){
		EventManager.instance.Raise (new RequestPreviousSceneEvent(SceneManager.GetActiveScene().name, 0));
	}
}
