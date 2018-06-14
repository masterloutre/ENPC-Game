using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsMenuSceneManager : MenuSceneManager {

	public List<Skill> skillList;

	public void Start(){
		QuerySkillListEvent query = new QuerySkillListEvent ();
		EventManager.instance.Raise (query);
		skillList = query.skillList;
	}

	public void Update(){
	}

	public void chooseSkill(int _choice){
		choice = _choice;
	}
}
