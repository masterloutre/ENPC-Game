using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillsMenuSceneManager : MenuSceneManager {

	public List<Skill> skillList;
	public GameObject skillPrefab;

	public void Start(){
		try {
			QuerySkillListEvent query = new QuerySkillListEvent ();
			EventManager.instance.Raise (query);
			skillList = query.skillList;
		} catch(Exception e) {
			Debug.Log ("!!! ERROR, could not get skillList: " + e.Message);
			generateDummySkillList ();
		}
		if (skillList == null) {
			generateDummySkillList ();
		}
		instantiateSkills ();
	}

	public void Update(){
	}

	public void chooseSkill(int _choice){
		choice = _choice;
		nextScene ();
	}

	public void instantiateSkills(){
		GameObject skillPanelGO = GameObject.FindGameObjectWithTag ("Skill Panel");
		Vector3 wolrdPosition = skillPanelGO.transform.position;
		float offsetX = 150;
		float offsetY = 120;
		for (int i = 0; i < skillList.Count; i++) {
			GameObject skillGO = Instantiate(skillPrefab, wolrdPosition + new Vector3(i%2 * 200 - 100, i/2 * (-offsetY) ,0), Quaternion.identity, skillPanelGO.transform);
			skillGO.transform.GetChild (1).gameObject.GetComponent<Text>().text = skillList [i].name;
			//int skillId = skillList[i].id;
			int skillId = i;
			skillGO.GetComponent<Button> ().onClick.AddListener (delegate {chooseSkill(skillId); });
		}
	}

	public void generateDummySkillList(){
		skillList = new List<Skill> ();
		skillList.Add(new Skill(0, "Dummy skill number 0"));
		skillList.Add(new Skill(1, "Dummy skill number 1"));
		skillList.Add(new Skill(2, "Dummy skill number 2"));
	}
		

}
