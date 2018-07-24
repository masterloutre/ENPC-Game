using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;

public class EnigmaManager : MonoBehaviour
{

    private List<EnigmaData> enigmas;
	private List<Skill> skills;


    // INSTANCIATION des variables
    public IEnumerator instanciateEnigmas(){
		this.enigmas = new List<EnigmaData> ();
		yield return StartCoroutine (getEnigmaData());
		computeSkillList ();
		yield break;
	}

	// RÉCUPÉRATION des données d'énigmes depuis le serveur
	IEnumerator getEnigmaData(){
		string serverURL = GlobalManager.webInterfaceRootURL;
		UnityWebRequest getRequest = UnityWebRequest.Get (serverURL + "/index.php?action=enigmes-disponibles");
		yield return getRequest.SendWebRequest();

		if(getRequest.isNetworkError||getRequest.isHttpError) {
			Debug.Log(getRequest.error);
			Debug.Log(getRequest.downloadHandler.text);

		}
		else {
			string json = "{\"Items\":" + getRequest.downloadHandler.text + "}";
			this.enigmas = JsonHelperList.FromJson<EnigmaData>(json);
		}
	}

    // REMPLISSAGE des valeurs obtenues
    private void computeSkillList()
    {
        skills = new List<Skill>();
        foreach (EnigmaData ed in enigmas)
        {
            Skill newSkill = new Skill(ed.competence_id, ed.competence);
            if (!skills.Contains(newSkill))
            {
                skills.Add(newSkill);
            }
        }
    }

    // GETTER
    public List<EnigmaData> getEnigmas(){
		return new List<EnigmaData> (enigmas);
	}
	public List<EnigmaData> getEnigmasBySkill(Skill skill){
		return new List<EnigmaData> (enigmas).Where(ed => ed.competence == skill.name).ToList();
	}
	public List<Skill> getSkills(){
		return skills;
	}

  public void removeEnigma (EnigmaData enigma){
    enigmas.Remove(enigma);
    computeSkillList();
  }


    /*
	// OLD MAY NEED TO DELETE
    public static void enigmaEnd()
    {
        foreach (GameObject GO in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            //print("Checking RootGameObjects in Scene " + SceneManager.GetActiveScene().name);
            if (GO.GetComponentInChildren<Enigma>() != null)
            {
                //get scene's Enigma
                GO.GetComponentInChildren<Enigma>().enigmaEnd();
                print("end enigma");
            }
        }
    }
    */
}

//Classe décrivant les différents prédicats de recherche dans la liste d'énigmes
public class EnigmaSearch
{
	private int searchValueInt;
	private string searchValueString;

	public EnigmaSearch (int _searchValue){
		searchValueInt = _searchValue;
		searchValueString = "";
	}

	public EnigmaSearch (string _searchValue){
		searchValueInt = 0;
		searchValueString = _searchValue;
	}

	//chercher à partir de l'index unity
	public bool unityIndexSearch(EnigmaData ed){
		return ed.index_unity == searchValueInt;
	}

	//chercher à partir de la compétence
	public bool skillSearch(EnigmaData ed){
		return ed.competence == searchValueString;
	}

}
