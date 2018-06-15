using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsMenuSceneManager : MenuSceneManager {

	public List<Skill> skillList;
	public GameObject skillPrefab;

	public void Start(){
		QuerySkillListEvent query = new QuerySkillListEvent ();
		EventManager.instance.Raise (query);
		skillList = query.skillList;
		if (skillList == null) {
			generateDummySkillList ();
		}
		instantiateSkills ();
	}

	public void Update(){
	}

	public void chooseSkill(int _choice){
		choice = _choice;
	}

	public void instantiateSkills(){
		//GameObject[] skillGOList = GameObject.FindGameObjectsWithTag ("Skill");
		Debug.Log ("Instantiate Skills, number of skills : "+ skillList.Count);
		GameObject skillPanelGO = GameObject.FindGameObjectWithTag ("Skill Panel");
		Debug.Log (skillPanelGO);
		Vector3 wolrdPosition = skillPanelGO.transform.position;
		float offsetX = 150;
		float offsetY = 120;
		for (int i = 0; i < skillList.Count; i++) {
			Debug.Log (skillList [i].name);

			//skillGOList [i].transform.GetChild (1).gameObject.GetComponent<Text>().text = skillList [i].name;
			GameObject skillGO = Instantiate(skillPrefab, wolrdPosition + new Vector3(i%2 * 200 - 100, i/2 * (-offsetY) ,0), Quaternion.identity, skillPanelGO.transform);
			skillGO.transform.GetChild (1).gameObject.GetComponent<Text>().text = skillList [i].name;
		}
	}

	public void generateDummySkillList(){
		skillList = new List<Skill> ();
		skillList.Add(new Skill(0, "Dummy skill number 0"));
		skillList.Add(new Skill(1, "Dummy skill number 1"));
		skillList.Add(new Skill(2, "Dummy skill number 2"));

	}


}
