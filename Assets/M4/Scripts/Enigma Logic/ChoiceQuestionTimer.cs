using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceQuestionTimer : ChoiceQuestion {
	public int timer;
	private bool timerStarted;
	GameObject questionTextGO;
	GameObject answerListGO;
	// Use this for initialization
	void Start () {
		base.Start();
		timerStarted = false;
		questionTextGO = GameObject.Find("QuestionText");
		answerListGO = GameObject.Find("AnswerList");
		if(timer > 0){
			questionTextGO.SetActive(false);
			answerListGO.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () {
		updateTimer();
	}

	private void updateTimer(){
		if(timerStarted){
			timer --;
		}
		if(timer <= 0){
		gameObject.SetActive(false);
		}
	}
 	public void startTimer(){
		GameObject.Find("StartQuestionButton").SetActive(false);
		questionTextGO.SetActive(true);
		answerListGO.SetActive(true);
		timerStarted = true;
	}
}
