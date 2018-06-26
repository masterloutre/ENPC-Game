using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveValue : MonoBehaviour {
	public float valeur; //pour la lisibilité dans l'éditeur
	public string unité; //pour la lisibilité dans l'éditeur
	public char nomDeVariable;
	public string légende;
	public bool valeurÀLaLigne;
	public float value { get; private set;} //pour l'utilisation dans le programme
	public string unit { get; private set;} //pour l'utilisation dans le programme
	public char variableName { get; private set;} //pour l'utilisation dans le programme


	//Initialise la valeur en récupérant la valeur et unité entrées dans l'éditeur et en les affichant dans le component
	void Start () {
		value = valeur;
		unit = unité;
		variableName = nomDeVariable;
		print ("tralal");
		gameObject.GetComponent<Text> ().text = computeText();
	}

	//crée le text qui doit etre affiché en fonction des valeurs des attributs du component
	string computeText(){
		string separator = (valeurÀLaLigne) ? "\n" : " ";
		return légende + separator + value + " " + unit;
	}

}
