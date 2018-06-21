using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;

public class EnigmaManager : MonoBehaviour {
	private List<EnigmaData> enigmas;
	private List<Skill> skills;
    private PopupManager popm;

    public void Start()
    {
        EventManager.instance.AddListener<GOButtonPressedEvent>((e) => { });
        EventManager.instance.AddListener<iButtonPressedEvent>((e) => { });
        EventManager.instance.AddListener<targetButtonPressedEvent>((e) => { });

    }
    //instancie la liste d'énigmes
    public IEnumerator instanciateEnigmas(){
		this.enigmas = new List<EnigmaData> ();
		yield return StartCoroutine (getEnigmaData());
		computeSkillList ();
		yield break;
	}

	//récupère les informations des énigmes auprès de l'interface web
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

	/* dans une autre classe maintenant -> EnigmaSequenceManager
	//récupère un objet représentant les datas d'une énigme à partir de son index unity
	private EnigmaData getEnigmaByUnityIndex(int value){
		EnigmaSearch es = new EnigmaSearch (value);
		return this.enigmas.Find (es.unityIndexSearch);
	}

	//récupère l'id (position) d'une énigme dans la liste à partir de son index unity
	public int getIdByUnityIndex(int value){
		EnigmaSearch es = new EnigmaSearch (value);
		return this.enigmas.FindIndex (es.unityIndexSearch);
	}

	//récupère l'id de l'énigme suivante dans la liste à partir de l'index unity d'une énigme
	//envoie une exception si il n'y a pas d'énigme suivante
	public int getNextUnityIndex(int currentUnityIndex){
		int enigmaId = getIdByUnityIndex (currentUnityIndex);
		int nextId = enigmaId + 1;
		if (nextId >= enigmas.Count ()) {
			throw new InvalidOperationException ("This was the last enigma");
		} else {
			return nextId;
		}
	}
	*/

	public List<EnigmaData> getEnigmas(){
		return new List<EnigmaData> (enigmas);
	}

	public List<EnigmaData> getEnigmasBySkill(Skill skill){
		return new List<EnigmaData> (enigmas).Where(ed => ed.competence == skill.name).ToList();
	}

	public List<Skill> getSkills(){
		return skills;
	}

	private void computeSkillList(){
		skills = new List<Skill> ();
		foreach (EnigmaData ed in enigmas){
			Skill newSkill = new Skill (ed.competence_id, ed.competence);
			if (!skills.Contains (newSkill)) {
				skills.Add (newSkill);
			}
		}
	}
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