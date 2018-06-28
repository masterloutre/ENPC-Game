using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using B83.ExpressionParser;

public class InputValidation : MonoBehaviour, ValidationMethod
{
	public float expectedResult;
	public string formula;
	public double marginError;
	List<double> paramList;
	ExpressionParser parser;


	void Start ()
	{
		GameObject[] paramGOList = GameObject.FindGameObjectsWithTag ("Input Param");
		parser = new ExpressionParser();
		paramList = new List<double> ();
		foreach(GameObject paramGO in paramGOList){
			paramList.Add(paramGO.GetComponent<InteractiveValue> ().value);
		}
	}

  public float score()
  {
		return (answerIsRight()) ? 100F : 0F;
  }

  public bool answerIsRight(){
		string studentInput = GameObject.FindGameObjectWithTag("Input Text").GetComponentInChildren<UnityEngine.UI.Text> ().text;
		Expression expCorrect = parser.EvaluateExpression(formula);
		if(studentInput != ""){
			float studentAnswer = float.Parse(studentInput);
			setParam (expCorrect);

			float error = (float)(studentAnswer - expCorrect.Value);

			//quicki fix pour le prototype à revoir après
			error = (float)(studentAnswer - expectedResult);


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
		for (int i = 0; i < paramList.Count; i++) {
			char tmp = (char)('a' + i);
			string id = "" + tmp;
			expCorrect.Parameters[id].Value = paramList[i]; // set the named parameter
		}
	}
}
