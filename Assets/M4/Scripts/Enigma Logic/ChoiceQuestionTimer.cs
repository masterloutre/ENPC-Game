using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* Component qui représente une question à choix multiple en temps limité
*/
public class ChoiceQuestionTimer : ChoiceQuestion {
	public float time;
	private bool timerStarted;
	GameObject questionTextGO;
	GameObject answerListGO;
	Timer timer;


	// Use this for initialization
	public void Start () {
		//utilise le start de la class parente
		base.Start();
		//Le timer n'a jamais démarré
		timerStarted = false;
		questionTextGO = GameObject.Find("QuestionText");
		answerListGO = GameObject.Find("AnswerList");
		if(time > 0){
			questionTextGO.SetActive(false);
			answerListGO.SetActive(false);
		}
	}

	// Update is called once per frame
	public void Update () {
		updateTimer();
	}

	void setTimerComponent(){
		timer = gameObject.GetComponentInChildren<Timer>(true);
		timer.gameObject.SetActive(true);
		timer.timeLimit = time;
	}

	private void updateTimer(){
		if(timerStarted){
			time -= Time.deltaTime;
			if(time <= 0){
				endTimer();
			}
		}
	}
 	public void startTimer(){
		GameObject.Find("StartQuestionButton").SetActive(false);
		questionTextGO.SetActive(true);
		answerListGO.SetActive(true);
		timerStarted = true;
		setTimerComponent();
	}

	virtual public void endTimer(){
		timer.gameObject.SetActive(false);
		questionTextGO.SetActive(false);
		answerListGO.SetActive(false);
		timerStarted = false;
		print(GameObject.Find("TimerArea").transform.GetChild(2).gameObject.name);
		GameObject.Find("TimerArea").transform.GetChild(2).gameObject.SetActive(true);
	}
}
