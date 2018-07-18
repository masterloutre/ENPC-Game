using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* Component qui représente une question à choix multiple en temps limité
*/
public class ChoiceQuestionTimerConditional : ChoiceQuestionTimer {


	void Start(){
		base.Start();
	}

	void Update(){
		base.Update();
	}

	public override void endTimer(){
		base.endTimer();
		EventManager.instance.Raise(new RequestUnlockNextPartsEvent(transform.GetSiblingIndex()));
	}

	public void forceTimeOut(){
		time = 0;
		endTimer();
	}

}
