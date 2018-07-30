using System;

[Serializable]
public class ScoreData
{
	public int id_enigme;
	public int id_etudiant;
	public int points; //Pour l'instant pas un % mais devra l'etre si on veut stocker par situation pro
	public int tentatives;
	public double temps;
	public int aide;
	//public int id_situation_pro

	public ScoreData ()
	{
		id_enigme = -1;
		id_etudiant = -1;
		points = 0;
		tentatives = 0;
		temps = 0;
		aide = 0;
		//id_situation_pro = -1;
	}

	public ScoreData (int _id_enigme, int _id_etudiant, int _points, int _tentatives, double _temps, bool _aide)
	{
		id_enigme = _id_enigme;
		id_etudiant = _id_etudiant;
		points = _points;
		tentatives = _tentatives;
		temps = _temps;
		aide = (_aide)? 1: 0;
		//id_situation_pro = _id_situation_pro;

	}

	public ScoreData(int _id_enigme, int _id_etudiant, Score score)
		: this(_id_enigme, _id_etudiant, (int)score.getGlobalSuccess(), 1, score.time, score.help){

	}

	public ScoreData(int _id_enigme, int _id_etudiant, Score score, int proSituationId)
		: this(_id_enigme, _id_etudiant, (int)score.getProSituationSuccess(proSituationId), 1, score.time, score.help){
	}
}
