using System;

[Serializable]
public class EnigmaData
{
	public int id;
	public int index_unity;
	public int type;
	public string nom;
	public int temps_max;
	public int difficulte;
	public int score_max;
	public int tentatives_max;
	public string competence;
	public int competence_id;

	public EnigmaData ()
	{
		id = -1;
		index_unity = -1;
		type = -1;
		nom = "empty enigma";
		temps_max = 0;
		difficulte = -1;
		score_max = 0;
		tentatives_max = 0;
		competence = "";
		competence_id = -1;
	}
}


