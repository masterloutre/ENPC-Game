﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void nextMenu(){
		EventManager.instance.Raise (new RequestNextMenuEvent(gameObject.scene.name, 0));
	}
}
