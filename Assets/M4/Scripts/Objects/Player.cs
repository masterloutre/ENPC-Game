using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
	public int id;
	public string studentNumber;
	public string surname;
	public string firstname;
	public string graduatingYear;

	public Player(){
		id = -1;
		studentNumber = "0000000000";
		surname = "anonyme";
		firstname = "étudiant";
		graduatingYear = "0000";
	}

	public Player (int _id, string _studentNumber, string _surname, string _firstname, string graduatingYear)
	{
		id = _id;
		studentNumber = _studentNumber;
		surname = _surname;
		firstname = _firstname;
		graduatingYear = graduatingYear;
	}
}
