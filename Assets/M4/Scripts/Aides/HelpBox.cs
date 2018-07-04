using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBox : MonoBehaviour {
	[SerializeField]
	[Help("Ceci est un message d'aide")]
	[TextArea]
	public string message;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
