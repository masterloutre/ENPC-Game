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
	public GameObject NoSkillText;


	public void Start(){
		NoSkillText.SetActive(false);
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

		if (skillList.Count == 0 ){
			NoSkillText.SetActive(true);
			GameObject.FindGameObjectWithTag ("Skill Panel").transform.Find("content").gameObject.SetActive(false);
		} else {
			GameObject skillPanelGO = GameObject.FindGameObjectWithTag ("Skill Panel").transform.Find("content").gameObject;
			Vector3 wolrdPosition = skillPanelGO.transform.position;
			float offsetX = 250;
			float offsetY = 120;

	        // Itération sur la liste de skills
			for (int i = 0; i < skillList.Count; i++) {
				GameObject skillGO = Instantiate(skillPrefab, wolrdPosition + new Vector3(i%2 * offsetX - offsetX/2, i/2 * (-offsetY) ,0), Quaternion.identity, skillPanelGO.transform);
				skillGO.transform.GetChild (1).gameObject.GetComponent<Text>().text = skillList [i].name;
				//int skillId = skillList[i].id;
				int skillId = i;
				skillGO.GetComponent<Button> ().onClick.AddListener (delegate {chooseSkill(skillId); });
                // 2 en 1 : ajoute un script zoom et le paramètre à 1.2
                skillGO.AddComponent<ZoomOnHover>().coeffHover = 1.2f ;
            }
		}

	}

    // Renseigne l'ID de la compétence choisie (récupéré lors du clic) et demande la prochaine scène
    public void chooseSkill(int _choice)
    {
        choice = _choice;
        nextScene(); //hérité de MenuSceneManager
    }

    // Génère une liste factice de compétence dummies. A utilisé si la connexion au serveur n'a pas encore eu lieu
    public void generateDummySkillList(){
		skillList = new List<Skill> ();
		skillList.Add(new Skill(0, "Dummy skill number 0"));
		skillList.Add(new Skill(1, "Dummy skill number 1"));
		skillList.Add(new Skill(2, "Dummy skill number 2"));
		skillList.Add(new Skill(3, "Dummy skill number 3"));
		skillList.Add(new Skill(4, "Dummy skill number 4"));
        // ajouter autant de dummies que souhaité, ce n'est important
	}


}
