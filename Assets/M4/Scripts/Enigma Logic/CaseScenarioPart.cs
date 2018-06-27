using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CaseScenarioPart : MonoBehaviour {
	public int id { get; private set; }
	public GameObject iconPrefab;
	// Use this for initialization
	void Start () {
		id = Array.IndexOf(transform.parent.GetComponentsInChildren<CaseScenarioPart> (), this);
		CaseScenarioPartIcon icon = new CaseScenarioPartIcon (this, iconPrefab);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void show(){
		foreach (CaseScenarioPart scenarioPart in transform.parent.GetComponentsInChildren<CaseScenarioPart>()) {
			scenarioPart.hide ();
		}
		gameObject.SetActive (true);
	}

	public void hide(){
		gameObject.SetActive (false);
	}
}
