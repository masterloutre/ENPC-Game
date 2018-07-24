using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using B83.ExpressionParser;

public class InputValidation : MonoBehaviour, ValidationMethod
{
	public string formula;
	public double marginError;
	ExpressionParser parser;
	Dictionary<char,double> paramList;

	/*
	public InputValidation(InputValidation validator){
		formula = validator.formula;
		marginError = validator.marginError;
		parser = validator.parser;
		paramList = validator.paramList;
	}
	*/

	void Start ()
	{
		GameObject[] paramGOList = GameObject.FindGameObjectsWithTag ("Input Param");
		parser = new ExpressionParser();
		paramList = new Dictionary<char, double>();
		foreach(GameObject paramGO in paramGOList){
			InteractiveValue iVal = paramGO.GetComponent<InteractiveValue> ();
			paramList.Add(iVal.variableName, iVal.value);
		}

	}

	public Score fillScore(Score score){
		bool success = answerIsRight();
		score.enigmaSuccess = success;
		score.addEnigmaSuccess(0, (success) ? 100F : 0F);
		return score;
	}

  public bool answerIsRight(){
		string studentInput = GameObject.FindGameObjectWithTag("Input Text").GetComponentInChildren<UnityEngine.UI.Text> ().text;
		Expression expCorrect = parser.EvaluateExpression(formula);
		if(studentInput != ""){
			float studentAnswer = float.Parse(studentInput);
			setParam (expCorrect);

			float error = (float)(studentAnswer - expCorrect.Value);
			if (Mathf.Abs (error) <= marginError) {
				Debug.Log ("Bonne réponse : ");
				return true;
			}
			else {
				Debug.Log ("Mauvaise réponse : "+studentAnswer+" " + expCorrect.Value);
				return false;
			}
		}
		return false;
	}

	void setParam(Expression expCorrect){ //set param in alphabetical order

		foreach( KeyValuePair<char, double> param in paramList){
			try{
				string name = "" + param.Key;
				expCorrect.Parameters[name].Value = param.Value; // set the named parameter
			} catch (KeyNotFoundException e){
				print("This may be wanted or not but the parameter named " + param.Key + "is not used for computing the result");
			}

		}
	}
}
