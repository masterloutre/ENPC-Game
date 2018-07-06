using System;

/*
 * TOUTES LES CLASSES D'EVENTS
 */

//Super classe des game events
using System.Collections.Generic;
using UnityEngine;


public class GameEvent
{

}

//Event qui demande une transition vers le prochain menu dans le déroulement du jeu
//Notamment écouté par le globalManager
public class RequestNextSceneEvent : GameEvent {
	public string currentSceneName;
	public int choiceId;

	public RequestNextSceneEvent(string _currentSceneName, int _choiceId){
		currentSceneName = _currentSceneName;
		choiceId = _choiceId;
	}
}

public class QueryPlayerManagerEvent : GameEvent {
	public PlayerManager playerManager;

	public QueryPlayerManagerEvent(){
		playerManager = null;
	}
}

public class QuerySceneLoaderEvent : GameEvent {
	public SceneLoader sceneLoader;

	public QuerySceneLoaderEvent(){
		sceneLoader = null;
	}
}

public class QuerySkillListEvent : GameEvent {
	public List<Skill> skillList;

	public QuerySkillListEvent(){
		skillList = null;
	}
}

public class RequestPreviousSceneEvent : GameEvent {
	public string currentSceneName;
	public int choiceId;

	public RequestPreviousSceneEvent(string _currentSceneName, int _choiceId){
		currentSceneName = _currentSceneName;
		choiceId = _choiceId;
	}
}
public class QueryEnigmaListEvent : GameEvent
{
    public List<EnigmaData> enigmaList;
    public Skill skill;

    public QueryEnigmaListEvent(Skill _skill)
    {
        enigmaList = null;
        skill = _skill;
    }

		public QueryEnigmaListEvent()
    {
        enigmaList = null;
        skill = null;
    }
}


public class RequestNextEnigmaEvent : GameEvent
{
    public RequestNextEnigmaEvent()
    {
    }
}

public class RequestPreviousEnigmaEvent : GameEvent
{

    public RequestPreviousEnigmaEvent()
    {
    }
}

public class QueryCurrentEnigmaDataEvent : GameEvent
{
    public EnigmaData enigmaData;
    public QueryCurrentEnigmaDataEvent()
    {
        enigmaData = null;
    }
}


public class RequestNextQuestionEvent : GameEvent
{
    public string currentSceneName;
    public int choiceId;

    public RequestNextQuestionEvent(string _currentSceneName, int _choiceId)
    {
        currentSceneName = _currentSceneName;
        choiceId = _choiceId;
    }
}

public class RequestSelectionEvent : GameEvent
{
    public string currentSceneName;
    public int choiceId;

    public RequestSelectionEvent(string _currentSceneName, int _choiceId)
    {
        currentSceneName = _currentSceneName;
        choiceId = _choiceId;
    }
}


public class GOButtonPressedEvent : GameEvent {
    public GOButtonPressedEvent(){}
}

public class iButtonPressedEvent : GameEvent {
	public iButtonPressedEvent(){}
}

public class targetButtonPressedEvent : GameEvent {
    public targetButtonPressedEvent(){}
}

public class ValidationScreenEvent : GameEvent
{
    public string answer;
    public string state;
    public float confidance;
    public ValidationScreenEvent(string etat) { // victoire/defaite
        state = etat; //crucial
        answer = "none";
        confidance = -1;
    }
    public ValidationScreenEvent(string etat,float certitude) // certitude
    {
        answer = "none";
        state = etat;
        confidance = certitude; //crucial
    }
    public ValidationScreenEvent(string etat, string ans) // correction/justification
    {
        answer = ans; //crucial
        state = etat;
        confidance = -1;
    }
}

public class ConfidanceErrorItemSelectionEvent : GameEvent
{
    public int choiceindex;
    public ConfidanceErrorItemSelectionEvent(int indice)
    {
        choiceindex = indice;
    }

}

public class EnigmaSubmittedEvent : GameEvent{
	public EnigmaSubmittedEvent(){
	}
}

public class QueryEnigmaSuccessEvent : GameEvent{
	public bool enigmaSuccess;
    public float certitude;
    public string method;
    public float score;
	public QueryEnigmaSuccessEvent(){
		enigmaSuccess = false;
        certitude = -1;
        method = "none";
        score = -1;
	}
}


public class QueryEnigmaScoreEvent : GameEvent
{
    public bool enigmaSuccess;
}
public class QueryPopUpQuestionsScoreEvent : GameEvent{
	public float score;
	public QueryPopUpQuestionsScoreEvent(){
		score = 0;
	}
}

//traité dans EnigmaSceneManager
public class QueryScoreEvent : GameEvent{
	public Score score;
	public QueryScoreEvent(){
		score = null;
	}
}

public class QueryTimerEvent : GameEvent{
	public float time;
	public QueryTimerEvent(){
		time = 0;
	}
}


public class RequestSaveScoreEvent : GameEvent{
	public ScoreData score;
	public RequestSaveScoreEvent(ScoreData _score){
		score = _score;
	}
}

//à envoyer quand le séquence de questions popUp est terminée, traité dans EnigmaSceneManager
public class PopUpQuestionsOverEvent : GameEvent{

}

public class RequestEnigmaRemoved : GameEvent{
	public EnigmaData enigma;
	public RequestEnigmaRemoved(EnigmaData _enigma){
		enigma = _enigma;
	}
}

public class RequestDisableEnigmaUIEvent : GameEvent{
	//public RequestDisableEnigmaUIEvent(){}
}
