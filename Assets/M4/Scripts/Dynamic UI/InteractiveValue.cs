using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class InteractiveValue : MonoBehaviour {
	public float valeur; //pour la lisibilité dans l'éditeur
	public string unité; //pour la lisibilité dans l'éditeur
	public char nomDeVariable;
	public string légende;
	public bool valeurÀLaLigne;
	public float value { get; private set;} //pour l'utilisation dans le programme
	public string unit { get; private set;} //pour l'utilisation dans le programme
	public char variableName { get; private set;} //pour l'utilisation dans le programme

	public InteractiveValue(float _value, string _unit, char _variableName, string _legend, bool _jump){
		value = _value;
		unit = _unit;
		variableName = _variableName;
		légende = _legend;
		valeurÀLaLigne = _jump;
	}

	//Initialise la valeur en récupérant la valeur et unité entrées dans l'éditeur et en les affichant dans le component
	void Awake () {
		value = valeur;
		unit = unité;
		variableName = nomDeVariable;
		gameObject.GetComponent<Text> ().text = computeText();
	}

	void Update()
  {
		if(Application.isEditor){
			value = valeur;
			unit = unité;
			variableName = nomDeVariable;
			gameObject.GetComponent<Text> ().text = computeText();
		}
	}

	//crée le text qui doit etre affiché en fonction des valeurs des attributs du component
	string computeText(){
		string separator = (valeurÀLaLigne) ? "\n" : " ";
		return légende + separator + value + " " + unit;
	}

}
