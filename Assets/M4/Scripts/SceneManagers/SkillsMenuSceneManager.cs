using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/*
 * Component qui gère le menu de choix de compétence
 */ 

public class SkillsMenuSceneManager : MenuSceneManager {

	public List<Skill> skillList; // Liste des compétences disponibles
	public GameObject skillPrefab; // Lien vers le préfab qui représente un bouton de compétence, à référencer dans l'éditeur


	public void Start(){
		try {
            // on récupère la liste de compétences
			QuerySkillListEvent query = new QuerySkillListEvent ();
			EventManager.instance.Raise (query);
			skillList = query.skillList;
		} catch(Exception e) {
            // si on ne réussit pas, on crée de faux skill Dummies
			Debug.Log ("!!! ERROR, could not get skillList: " + e.Message);
			generateDummySkillList ();
		}
		if (skillList == null) {
			generateDummySkillList ();
		}
        // Instantiation des objets
		instantiateSkills ();
	}

    // Crée un bouton intéractif par compétence (icone et nom, sur le modèle du préfab). Une action est associée à chaque instance (chooseSkill) pour repérer la compétence ciblée
    public void instantiateSkills()
    {
		GameObject skillPanelGO = GameObject.FindGameObjectWithTag ("Skill Panel");
		Vector3 wolrdPosition = skillPanelGO.transform.position;
		float offsetX = 150;
		float offsetY = 120;

        // Itération sur la liste de skills
		for (int i = 0; i < skillList.Count; i++) {
			GameObject skillGO = Instantiate(skillPrefab, wolrdPosition + new Vector3(i%2 * 200 - 100, i/2 * (-offsetY) ,0), Quaternion.identity, skillPanelGO.transform);
			skillGO.transform.GetChild (1).gameObject.GetComponent<Text>().text = skillList [i].name;
			//int skillId = skillList[i].id;
			int skillId = i;
			skillGO.GetComponent<Button> ().onClick.AddListener (delegate {chooseSkill(skillId); });
		}
	}

    // Renseigne l'ID de la compétence choisie (récupéré lors du clic) et demande la prochaine scène
    public void chooseSkill(int _choice)
    {
        choice = _choice;
        nextScene();
    }

    // Génère une liste factice de compétence dummies. A utilisé si la connexion au serveur n'a pas encore eu lieu
    public void generateDummySkillList(){
		skillList = new List<Skill> ();
		skillList.Add(new Skill(0, "Dummy skill number 0"));
		skillList.Add(new Skill(1, "Dummy skill number 1"));
		skillList.Add(new Skill(2, "Dummy skill number 2"));
        // ajouter autant de dummies que souhaité, ce n'est important
	}
		

}
