using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enigma {
	public int dbId {get; private set;}
	public int UnityId {get; private set;}
	public int type {get; private set;}
	public string name {get; private set;}
	public int maxTime {get; private set;}
	public int difficulty {get; private set;}
	public int maxScore {get; private set;}
	public Skill skill {get; private set;}
	public Score score;

	public Enigma(EnigmaData data){
		dbId = data.id;
		UnityId = data.index_unity;
		type = data.type;
		name = data.nom;
		maxTime = data.temps_max;
		difficulty = data.difficulte;
		maxScore = data.score_max;
		skill = new Skill(data.competence_id, data.competence);
		score = new Score (maxScore);
	}

	public ScoreData createScoreData(){
		int playerId = -1;
		return new ScoreData(dbId, playerId, score);
	}

}
