using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsMenuSceneManager : MenuSceneManager {

	public List<Skill> skillList;

	public void Update(){
		Debug.Log (skillList [0].name);
	}

	public void chooseSkill(int _choice){
		choice = _choice;
	}
}
