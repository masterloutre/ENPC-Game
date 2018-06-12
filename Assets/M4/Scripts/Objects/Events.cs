using System;

public class RequestNextMenuEvent : EventManager {
	public string currentSceneName;
	public int choiceId;

	public RequestNextMenuEvent(string _currentSceneName, int _choiceId){
		currentSceneName = _currentSceneName;
		choiceId = _choiceId;
	}
}

