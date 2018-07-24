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
	public Dictionary<int, List<float>> methodPointsByProfessionalSituation {get; private set;}
	public Dictionary<int, List<float>> enigmaPointsByProfessionalSituation {get; private set;}

	public Score(float _max){
		maxNumberPoints = _max;
		enigmaSuccess = false;
		certaintyLevel = 0;
		methodPointsByProfessionalSituation = new Dictionary<int, List<float>>();
		enigmaPointsByProfessionalSituation = new Dictionary<int, List<float>>();
	}

	public Score() : this(0) {

	}

	public void addEnigmaSuccess(int id, float percent){
				try
		{
			List<float> percentList = enigmaPointsByProfessionalSituation[id]; //va envoyer une exception si l'id n'est pas valable
			percentList.Add(percent);
			Debug.Log("ADDED ENIGMA SCORE : A la situation pro n°"+ id+", vous avez obtenu "+ percent+"%" );
		}
		catch (KeyNotFoundException e)
		{
			Debug.Log("No enigma score for situation " + id + " yet" );
			List<float> percentList = new List<float>();
			percentList.Add(percent);
			enigmaPointsByProfessionalSituation[id] = percentList;
			Debug.Log("ADDED ENIGMA SCORE : A la situation pro n°"+ id+", vous avez obtenu "+ percent+"%" );
		}

	}

	public void addMethodSuccess(int id, float percent){
				try
		{
			List<float> percentList = methodPointsByProfessionalSituation[id];
			percentList.Add(percent);
			Debug.Log("ADDED METHOD SCORE : A la situation pro n°"+ id+", vous avez obtenu "+ percent+"%" );
		}
		catch (KeyNotFoundException e)
		{
			Debug.Log("No method score for situation " + id + " yet" );
			List<float> percentList = new List<float>();
			percentList.Add(percent);
			methodPointsByProfessionalSituation[id] = percentList;
			Debug.Log("ADDED METHOD SCORE : A la situation pro n°"+ id+", vous avez obtenu "+ percent+"%" );
		}
	}


	private float computeScoreFromCertainty( float score){
    int deltaMax =(int)Math.Round(( (1/ Math.Exp(Math.Pow(score, 0.83)/25.0)) - 0.58) * 120);
    //int deltaMax = (int)((1/Math.Exp(Math.Pow(score, 0.83)/25.0)) -0.58) *120;
    Debug.Log("max delta certainty = " + deltaMax.ToString());
    float delta = (float)Math.Round((100.0 - certaintyLevel) * deltaMax / 100.0);
    float result = score + delta;
    return constraintPercent(result);
  }

	public float listAverage(List<float> numberList){
		float result = 0F;
		int numberCount = 0;
		foreach(float number in numberList){
			result += number;
			numberCount ++;
		}
		if(numberCount == 0){
			return 0;
		}
		return (result / (float)numberCount);
	}

	public float getProSituationSuccess(int id){
		try{
			float success = computeScoreFromCertainty(listAverage(enigmaPointsByProfessionalSituation[id]))*0.5F + listAverage(methodPointsByProfessionalSituation[id])*0.5F;
			return constraintPercent(success);
		} catch (Exception e1){
			try{
				Debug.Log("there was a problem with either enigma points and method points");
				float success = computeScoreFromCertainty(listAverage(enigmaPointsByProfessionalSituation[id]));
				return constraintPercent(success);
			} catch(Exception e2){
				 Debug.Log("there was a problem with both enigma points and method points");
				 return 0;
			}
		}
	}

	//Moyenne des score des situations pro repassé en nombre de points (pas en %)
	public float getGlobalSuccess () {
		float success = 0;
		float number = 0;
		if(enigmaPointsByProfessionalSituation.Count > 0){
			foreach( KeyValuePair<int, List<float>> proSit in enigmaPointsByProfessionalSituation){
				success += getProSituationSuccess(proSit.Key);
				number += 1;
			}
			success = success / number;
		}
		return constraintPercent(success);
	}

	public int getGlobalScore() {
		float globalSuccess = getGlobalSuccess();
		return (int)Math.Round(globalSuccess * maxNumberPoints / 100.0);
	}


	float constraintPercent(float value){
		return (value < 0)?0: (value > 100)? 100: value;
	}

	public string ToString(){
		string s = "Score :"
		+ "\nsuccess : " + enigmaSuccess
		+ "\ncertainty : " + certaintyLevel
		+ "\nEnigma questions : ";
		foreach(KeyValuePair<int, List<float>> percent in enigmaPointsByProfessionalSituation){
			s += "\nFor pro situation " + percent.Key + ", Success percentage is : " + listAverage(percent.Value);
			s += "\nWith certainty , Success percentage is : " + computeScoreFromCertainty(listAverage(percent.Value));
		}
		s += "\nMethod questions : ";
		foreach(KeyValuePair<int, List<float>> percent in methodPointsByProfessionalSituation){
			s += "\nFor pro situation " + percent.Key + ", Success percentage is : " + listAverage(percent.Value);
		}
		s += "\n\nGlobal success is : " + getGlobalSuccess() + " on 100";
		s += "\n\nGlobal score is : " + getGlobalScore() + " on " +  maxNumberPoints;

		return s;
	}
}
