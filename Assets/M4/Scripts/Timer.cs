using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public GameObject timerOnScreen;

	public float timeLimit = 300f;
	float time;
	// Use this for initialization
	void Start () {
		time = 0;
	}

	void OnEnable(){
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		timerOnScreen.GetComponent<UnityEngine.UI.Text> ().text = (int)time / 60 + " : " + (int)time % 60;
	}

	public void resetTimer(){
		time = 0;
	}
}
