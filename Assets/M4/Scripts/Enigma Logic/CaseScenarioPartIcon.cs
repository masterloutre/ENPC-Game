using System.Collections;
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

		if (scenarioPart.id != 0) {
			scenarioPart.hide ();
		}

	}

	public void activateScenarioPart(){
		scenarioPart.show ();
        Debug.Log(scenarioPart.id);
        EventManager.instance.Raise(new RequestNextQuestionEvent(scenarioPart.name,scenarioPart.id));
    }

	public void createGameObject(){

		float offsetY = scenarioPart.id * (-45) - 25;
		GameObject parentGO = GameObject.Find ("TimeLine");

		GameObject iconGO = GameObject.Instantiate(prefab, parentGO.transform, false);
		iconGO.transform.position += new Vector3 (0, offsetY, 0);
		iconGO.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener (activateScenarioPart);

		if(scenarioPart.indiquerNumeroPartie){
			string separator = "";
			if(prefab.name != "PartIcon"){
				separator = " ";
			}
			iconGO.GetComponentInChildren<Text>().text += separator + (scenarioPart.id+1).ToString();
		}
	}

}
