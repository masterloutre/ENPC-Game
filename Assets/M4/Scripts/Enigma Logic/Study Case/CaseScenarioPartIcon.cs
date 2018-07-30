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
        iconGO.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(delegate { EventManager.instance.Raise(new RequestShowPartEvent(scenarioPart.id)); });

        if (scenarioPart.indiquerNumeroPartie){
			string separator = "";
			if(prefab.name != "PartIcon"){
				separator = " ";
			}
			iconGO.GetComponentInChildren<Text>().text += separator + (scenarioPart.id+1).ToString();
        }
	}

	public void glow(){
		Outline outline = iconGO.transform.GetChild(1).gameObject.AddComponent<Outline>();
		outline.effectColor = new Color(0F, 1F, 1F, 1F);
		outline.effectDistance = new Vector2(3F, 3F);

	}

	public void unglow(){
		Outline outline = iconGO.GetComponentInChildren<Outline>(true);
		if(outline != null) { Component.Destroy(outline);}
	}

}
