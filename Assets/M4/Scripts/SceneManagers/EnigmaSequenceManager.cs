using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnigmaSequenceManager : MonoBehaviour {

	List<EnigmaData> enigmaDataList; //éventuellement temporaire pour test
	public Skill skill;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//arguments temporaire pour test 
	public void updateEnigmaSequence(Skill _skill){
		skill = _skill;
		Debug.Log ("update enigma data : " + skill.name);
		GameObject titleGO = GameObject.Find ("Title");
		titleGO.GetComponent<Text> ().text += skill.name;
	}
}
