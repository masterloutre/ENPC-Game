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
	GameObject imageGO;
	public Timer timer;


	// Use this for initialization
	public void Start () {
		print("Start");
		//utilise le start de la class parente
		base.Start();
		//Le timer n'a jamais démarré
		timerStarted = false;
		questionTextGO = gameObject.transform.Find("QuestionText").gameObject;
		answerListGO = gameObject.transform.Find("AnswerList").gameObject;
		if(img != null){
			imageGO = gameObject.transform.Find("QuestionImage").gameObject;
		}


		if(time > 0){
			questionTextGO.SetActive(false);
			answerListGO.SetActive(false);
			if(img != null){
				imageGO.SetActive(false);
			}
		} else {
			gameObject.transform.Find("TimerArea").Find("StartQuestionButton").gameObject.SetActive(false);
		}
	}

	// Update is called once per frame
	public void Update () {
		updateTimer();
	}

	public void setTimerComponent(){
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
		gameObject.transform.Find("TimerArea").Find("StartQuestionButton").gameObject.SetActive(false);
		questionTextGO.SetActive(true);
		answerListGO.SetActive(true);
		if(img != null){
			imageGO.SetActive(true);
		}
		timerStarted = true;
		setTimerComponent();
	}

	virtual public void endTimer(){
		timer.gameObject.SetActive(false);
		questionTextGO.SetActive(false);
		answerListGO.SetActive(false);
		if(img != null){
			imageGO.SetActive(false);
		}
		timerStarted = false;
		gameObject.transform.Find("TimerArea").GetChild(2).gameObject.SetActive(true);
	}
}
