using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CaseScenarioPartIcon {
	private GameObject prefab;
	private CaseScenarioPart scenarioPart;
    private GameObject iconGO;
	// Use this for initialization

	public CaseScenarioPartIcon(CaseScenarioPart _scenarioPart, GameObject _prefab){
		scenarioPart = _scenarioPart;
		prefab = _prefab;

		createGameObject ();
        

	}
    public void hide()
    {
        Debug.Log("Hiding ICON" + scenarioPart);
        iconGO.SetActive(false);
    }
    public void show()
    {
        iconGO.SetActive(true);
    }
	

	public void createGameObject(){

		float offsetY = scenarioPart.id * (-45) - 25;
		GameObject parentGO = GameObject.Find ("TimeLine");

		iconGO = GameObject.Instantiate(prefab, parentGO.transform, false);
        iconGO.name = prefab.name+scenarioPart.id;
		iconGO.transform.position += new Vector3 (0, offsetY, 0);
		//iconGO.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener (activateScenarioPart);
        iconGO.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { EventManager.instance.Raise(new RequestNextQuestionEvent(scenarioPart.name, scenarioPart.id)); });

        if (scenarioPart.indiquerNumeroPartie){
			string separator = "";
			if(prefab.name != "PartIcon"){
				separator = " ";
			}
			iconGO.GetComponentInChildren<Text>().text += separator + (scenarioPart.id+1).ToString();
            Debug.Log(iconGO.GetComponentInChildren<Text>().text);
        }
	}

}
