using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* Component qui représente une question à choix multiple en temps limité
*/
public class ChoiceQuestionTimerConditional : ChoiceQuestionTimer {

/*
	void Start(){
		base.Start();
	}

	void Update(){
		base.Update();
	}
*/
	public override void endTimer(){

		if(timer != null){
			base.endTimer();
		} else {
			gameObject.transform.Find("TimerArea").GetChild(2).gameObject.SetActive(true);
			gameObject.transform.Find("TimerArea").GetChild(0).gameObject.SetActive(false);
		}

		EventManager.instance.Raise(new RequestUnlockNextPartsEvent(transform.GetSiblingIndex()));
	}

	public void forceTimeOut(){
		time = 0;
		endTimer();
	}

}
