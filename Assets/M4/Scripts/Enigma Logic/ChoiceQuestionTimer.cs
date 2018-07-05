using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceQuestionTimer : ChoiceQuestion {
	public int timer;
	private bool timerStarted;
	GameObject questionTextGO;
	GameObject answerListGO;

	public void Awake(){
		timerStarted = false;
	}


	// Use this for initialization
	void Start () {
		base.Start();
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
			endTimer();
		}
	}
 	public void startTimer(){
		//GameObject.Find("StartQuestionButton").SetActive(false);
		Destroy(GameObject.Find("StartQuestionButton"));
		questionTextGO.SetActive(true);
		answerListGO.SetActive(true);
		timerStarted = true;
	}

	public void endTimer(){
		//GameObject.Find("EndQuestionText").SetActive(false);
		GameObject.Find("TimerArea").transform.GetChild(0).gameObject.SetActive(true);
		questionTextGO.SetActive(false);
		answerListGO.SetActive(false);
	}
}
