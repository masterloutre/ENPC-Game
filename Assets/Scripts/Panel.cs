using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {

	public GameObject itemsNumber;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (itemsNumber.GetComponentInChildren<UnityEngine.UI.Text> () != null) {
			if (this.transform.childCount <= 1) {
				itemsNumber.SetActive (false);
			}
			itemsNumber.GetComponentInChildren<UnityEngine.UI.Text> ().text = this.transform.childCount.ToString();
		}
	}
}
