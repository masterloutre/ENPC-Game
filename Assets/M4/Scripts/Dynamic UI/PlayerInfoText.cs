using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* 
 * Component qui affiche dans le component Text du gameObject auquel il est associé
 * un text d'information modulable liées au joueur (nom, numéro étudiant, promo)
 * Il peut être configurer depuis Unity pour sauter des lignes ou pas, ou pour customiser le symbole séparant les informations
 */

public class PlayerInfoText : MonoBehaviour {
	private Text text;
	public bool name;
	public bool studentNumber;
	public bool graduatingYear;
	public bool breakLines;
	public string separator = "";
	private PlayerManager pm;

	//récupère le component Text attaché au gameObject parent
	public void Awake(){
		pm = Object.FindObjectOfType<PlayerManager>();
	}

	//à l'initialisation du component génère le text en utilisant computeText()
	void Start () {
		text = gameObject.GetComponent<Text> ();
		text.text = computeText();
	}

	//en fonction des réglages demandé depuis l'éditeur Unity, génère le texte.
	string computeText(){
		string text = "";
		//ajouter un saut de ligne
		if (breakLines) {
			separator += "\n";
		}
		//ajouter le nom
		if (name) {
			text += pm.getPlayerName () + separator;
		}
		//ajouter le numéro étudiant
		if (studentNumber) {
			text += pm.getPlayerStudentNumber () + separator;
		}
		//ajouter l'année de promo
		if (graduatingYear) {
			text += pm.getPlayerGraduatingYear () + separator;
		}
		return text;
	}
}
