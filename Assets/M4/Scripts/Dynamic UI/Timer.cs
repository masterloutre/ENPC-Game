using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public GameObject timerOnScreen;

	public float timeLimit = 300f;
	float time;
	// Use this for initialization
	void Start () {
		time = 0;
		EventManager.instance.AddListener<QueryTimerEvent> (sendTime);
	}

	void OnEnable(){
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		timerOnScreen.GetComponent<UnityEngine.UI.Text> ().text = (int)time / 60 + " : " + (int)time % 60;
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<QueryTimerEvent> (sendTime);

	}

	public void resetTimer(){
		time = 0;
	}

    public float getTime()
    {
        return time;
    }

	public void sendTime(QueryTimerEvent e){
		e.time = time;
	}
}
