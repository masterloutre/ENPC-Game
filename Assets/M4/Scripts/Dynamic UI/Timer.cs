using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public GameObject timerOnScreen;
	public float timeLimit = 300f;
	float time;
	public bool reverse = false;
	// Use this for initialization
	void Start () {
		if(reverse){
			time = timeLimit;
		} else {
			time = 0;
		}
		EventManager.instance.AddListener<QueryTimerEvent> (sendTime);
	}

	void OnEnable(){
		if(reverse){
			time = timeLimit;
		} else {
			time = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		if(reverse){
			time -= Time.deltaTime;
		} else {
			time += Time.deltaTime;
		}

		timerOnScreen.GetComponent<UnityEngine.UI.Text> ().text = (int)time / 60 + " : " + (int)time % 60;
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<QueryTimerEvent> (sendTime);

	}

	public void resetTimer(){
		if(reverse){
			time = timeLimit;
		} else {
			time = 0;
		}
	}

    public float getTime()
    {
        return time;
    }

	public void sendTime(QueryTimerEvent e){
		e.time = time;
	}
}
