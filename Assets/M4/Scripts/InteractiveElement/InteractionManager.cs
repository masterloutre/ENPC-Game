using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {


	Vector3 coeffHover = new Vector3 (1.1f, 1.1f, 1.1f);

	public void InfoChangeState(GameObject go){
		go.SetActive (!go.activeSelf);
	}

	public void zoomOnHover(GameObject go){
		go.transform.localScale = go.transform.localScale+coeffHover;
	}

	public void dezoom(GameObject go){
		go.transform.localScale = go.transform.localScale-coeffHover;
	}

	public void exitGameButton(){
		Application.Quit ();
	}






}
