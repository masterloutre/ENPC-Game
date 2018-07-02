using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CaseScenarioPart : MonoBehaviour {
	public int id { get; private set; }
	public GameObject iconPrefab;
	public bool indiquerNumeroPartie;
	// Use this for initialization
	void Awake(){
		id = Array.IndexOf(transform.parent.GetComponentsInChildren<CaseScenarioPart> (), this);
	}


	void Start () {
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

	public override bool Equals(object obj)
	{

		CaseScenarioPart item = obj as CaseScenarioPart;

		if (item == null)
		{
			return false;
		}
		Debug.Log ("Equal override : " + this.gameObject.name + " = " + item.gameObject.name);
		return this.id == item.id && this.iconPrefab == item.iconPrefab && this.gameObject.name == item.gameObject.name;
	}

	public override int GetHashCode()
	{
		return this.id.GetHashCode();
	}
}
