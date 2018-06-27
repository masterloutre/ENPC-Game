﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CaseScenarioPartIcon {
	private GameObject prefab;
	private CaseScenarioPart scenarioPart;
	// Use this for initialization

	public CaseScenarioPartIcon(CaseScenarioPart _scenarioPart, GameObject _prefab){
		scenarioPart = _scenarioPart;
		prefab = _prefab;

		createGameObject ();

	}

	public void activateScenarioPart(){
		scenarioPart.show ();
	}

	public void createGameObject(){
		float offsetY = scenarioPart.id * (-30) - 30;
		GameObject parentGO = GameObject.Find ("TimeLine");

		GameObject iconGO = GameObject.Instantiate(prefab, parentGO.transform, false);
		iconGO.transform.position += new Vector3 (0, offsetY, 0);
		iconGO.GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (activateScenarioPart);
	}

}
