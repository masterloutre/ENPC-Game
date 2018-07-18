using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Score {
	public float maxNumberPoints {get; set;}
	public bool enigmaSuccess {get; set;}
	public float certaintyLevel {get; set;}
	public float time {get; set;}
	public bool help {get; set;}
	public Dictionary<int, float> methodPointsByProfessionalSituation {get; private set;}
	public Dictionary<int, float> enigmaPointsByProfessionalSituation {get; private set;}

	public Score(float _max){
		maxNumberPoints = _max;
		enigmaSuccess = false;
		certaintyLevel = 0;
		methodPointsByProfessionalSituation = new Dictionary<int, float>();
		enigmaPointsByProfessionalSituation = new Dictionary<int, float>();
	}

	public Score() : this(0) {

	}

	public void addEnigmaSuccess(int id, float percent){
				try
		{
		    enigmaPointsByProfessionalSituation.Add(id, percent);
		}
		catch (ArgumentException)
		{
		    Console.WriteLine("An element with Key = " + id + " already exists.");
				enigmaPointsByProfessionalSituation[id] += percent;
		}

	}

	public void addMethodSuccess(int id, float percent){
				try
		{
				methodPointsByProfessionalSituation.Add(id, percent);
		}
		catch (ArgumentException)
		{
				Console.WriteLine("An element with Key = " + id + " already exists.");
				methodPointsByProfessionalSituation[id] += percent;
		}
	}


	private float computeScoreFromCertainty( float score){
    int deltaMax =(int)Math.Round(( (1/ Math.Exp(Math.Pow(score, 0.83)/25.0)) - 0.58) * 120);
    //int deltaMax = (int)((1/Math.Exp(Math.Pow(score, 0.83)/25.0)) -0.58) *120;
    Debug.Log("max delta certainty = " + deltaMax.ToString());
    float delta = (float)Math.Round((100.0 - certaintyLevel) * deltaMax / 100.0);
    float result = score + delta;
    return (result < 0)? 0 : (result > 100)? 100 : result;
  }

	public float getProSituationSuccess(int id){
		try{
			float success = computeScoreFromCertainty(enigmaPointsByProfessionalSituation[id]) + methodPointsByProfessionalSituation[id];
			return (success < 0)?0: (success > 100)? 100: success;
		} catch (Exception e){
			float success = computeScoreFromCertainty(enigmaPointsByProfessionalSituation[id]);
			return (success < 0)?0: (success > 100)? 100: success;
		}
	}

	//Moyenne des score des situations pro repassé en nombre de points (pas en %)
	public float getGlobalSuccess () {
		float success = 0;
		float number = 0;
		foreach( KeyValuePair<int, float> proSit in enigmaPointsByProfessionalSituation){
			success += getProSituationSuccess(proSit.Key);
			number += 1;
		}
		success = success / number;
	 	success = (success < 0)?0: (success > 100)? 100: success;
		return success;
	}

	public int getGlobalScore() {
		float globalSuccess = getGlobalSuccess();
		return (int)Math.Round(globalSuccess * maxNumberPoints / 100.0);
	}



	public string ToString(){
		string s = "Score :"
		+ "\nsuccess : " + enigmaSuccess
		+ "\ncertainty : " + certaintyLevel
		+ "\nEnigma questions : ";
		foreach(KeyValuePair<int, float> percent in enigmaPointsByProfessionalSituation){
			s += "\nFor pro situation " + percent.Key + ", Success percentage is : " + percent.Value;
		}
		s += "\nMethod questions : ";
		foreach(KeyValuePair<int, float> percent in methodPointsByProfessionalSituation){
			s += "\nFor pro situation " + percent.Key + ", Success percentage is : " + percent.Value;
		}
		s += "\n\nGlobal score is : " + getGlobalScore() + " on " +  maxNumberPoints;

		return s;
	}
}
