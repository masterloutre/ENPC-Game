using System;

public class RequestNextMenuEvent : GameEvent {
	public string currentSceneName;
	public int choiceId;

	public RequestNextMenuEvent(string _currentSceneName, int _choiceId){
		currentSceneName = _currentSceneName;
		choiceId = _choiceId;
	}
}

