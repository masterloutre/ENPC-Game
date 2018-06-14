using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsMenuSceneManager : MenuSceneManager {

	public List<Skill> skillList;

	public void Awake(){
		QuerySkillListEvent query = new QuerySkillListEvent ();
		EventManager.instance.Raise (query);
		skillList = query.skillList;
		Debug.Log ("update skillMenuManager" + skillList);
	}

	public void Update(){
		//Debug.Log ("update skillMenuManager" + skillList [0].name);
	}

	public void chooseSkill(int _choice){
		choice = _choice;
	}
}
