using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
 * Component qui gère le menu de choix de compétence
 */ 

public class SkillsMenuSceneManager : MenuSceneManager {

	public List<Skill> skillList; //liste des compétences disponibles
	public GameObject skillPrefab; //lien vers le préfab qui reprénse un bouton de compétence, à entrer dans l'éditeur

	//Au démarrage du component, récupère la liste de compétence et génère les boutons du menu en fonction de la liste
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

	//Donne l'id de la compétence choisie comme choix et demande la prochaine scène
	public void chooseSkill(int _choice){
		choice = _choice;
		nextScene ();
	}

	//Génère et affiche un icon + nom (sur le model du préfab) par compétence et y attache une action à faire au click (chooseSkill)
	public void instantiateSkills(){
		GameObject skillPanelGO = GameObject.FindGameObjectWithTag ("Skill Panel");
		Vector3 wolrdPosition = skillPanelGO.transform.position;
		float offsetX = 250;
		float offsetY = 120;
		for (int i = 0; i < skillList.Count; i++) {
			GameObject skillGO = Instantiate(skillPrefab, wolrdPosition + new Vector3(i%2 * offsetX - offsetX/2, i/2 * (-offsetY) ,0), Quaternion.identity, skillPanelGO.transform);
			skillGO.transform.GetChild (1).gameObject.GetComponent<Text>().text = skillList [i].name;
			//int skillId = skillList[i].id;
			int skillId = i;
			skillGO.GetComponent<Button> ().onClick.AddListener (delegate {chooseSkill(skillId); });
		}
	}

	//Génère une liste factice de compétence si la connexion au serveur n'a pas encore eu lieu
	public void generateDummySkillList(){
		skillList = new List<Skill> ();
		skillList.Add(new Skill(0, "Dummy skill number 0"));
		skillList.Add(new Skill(1, "Dummy skill number 1"));
		skillList.Add(new Skill(2, "Dummy skill number 2"));
	}
		

}
