using System;

/*
 * TOUTES LES CLASSES D'EVENTS
 */ 

//Super classe des game events
using System.Collections.Generic;


public class GameEvent
{

}

//Event qui demande une transition vers le prochain menu dans le déroulement du jeu
//Notamment écouté par le globalManager
public class RequestNextMenuEvent : GameEvent {
	public string currentSceneName;
	public int choiceId;

	public RequestNextMenuEvent(string _currentSceneName, int _choiceId){
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

public class QuerySkillListEvent : GameEvent {
	public List<Skill> skillList;

	public QuerySkillListEvent(){
		skillList = null;
	}
}

