using System;

[Serializable]
public class ScoreData
{
	public int id_enigme;
	public int id_etudiant;
	public int points;
	public int tentatives;
	public double temps;
	public int aide;

	public ScoreData ()
	{
		id_enigme = -1;
		id_etudiant = -1;
		points = 0;
		tentatives = 0;
		temps = 0;
		aide = 0;
	}

	public ScoreData (int _id_enigme, int _id_etudiant, int _points, int _tentatives, double _temps, int _aide)
	{
		id_enigme = _id_enigme;
		id_etudiant = _id_etudiant;
		points = _points;
		tentatives = _tentatives;
		temps = _temps;
		aide = _aide;
	}
}


