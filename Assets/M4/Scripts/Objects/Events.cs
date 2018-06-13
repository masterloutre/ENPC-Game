using System;

/*
 * TOUTES LES CLASSES D'EVENTS
 */ 

//Super classe des game events
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

