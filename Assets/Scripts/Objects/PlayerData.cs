using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Classe qui décrit le joueur
 * Les champs sont publics car l'objet doit être sérializable depuis un objet JSON
 * Afin de protéger l'intégrité de l'objet, il est manipulé uniquement par le PlayerManager
 */

public class PlayerData{
	public int id;
	public string studentNumber;
	public string surname;
	public string firstname;
	public string graduatingYear;

	//construit un objetjoueur anonyme
	public PlayerData(){
		id = -1;
		studentNumber = "0000000000";
		surname = "anonyme";
		firstname = "étudiant";
		graduatingYear = "0000";
	}

	//contruit un objet joueur à partir d'informations
	public PlayerData (int _id, string _studentNumber, string _surname, string _firstname, string graduatingYear)
	{
		id = _id;
		studentNumber = _studentNumber;
		surname = _surname;
		firstname = _firstname;
		graduatingYear = graduatingYear;
	}
}
