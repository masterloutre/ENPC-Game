using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using B83.ExpressionParser;


public class InputAnswer : MonoBehaviour {

	public string formula;
	float studentAnswer;
	public GameObject inputField;
	public GameObject victoryTimeline;
	public GameObject errorTimeline;
	public GameObject[] paramInput;
	List<double> param;
	public double marginError;
	ExpressionParser parser;

	void Start() {
		parser = new ExpressionParser();
		param = new List<double> ();
		foreach(GameObject go in paramInput){
			param.Add(double.Parse (go.GetComponent<UnityEngine.UI.Text> ().text));
		}
	}
		


	public void validation(){
		string studentInput = inputField.GetComponent<UnityEngine.UI.Text> ().text;
		Expression expCorrect = parser.EvaluateExpression(formula);
		if(studentInput != ""){
			studentAnswer = float.Parse(studentInput); 
			setParam (expCorrect);


			float error = (float)(studentAnswer - expCorrect.Value);

			if (Mathf.Abs (error) <= marginError) {
				Debug.Log ("Bonne réponse : ");
				victoryTimeline.SetActive (true);
			}
			else {
				Debug.Log ("Mauvaise réponse : "+studentAnswer+" " + expCorrect.Value);
				errorTimeline.SetActive (true);
			}
		}
	}


	void setParam(Expression expCorrect){ //set param in alphabetical order
		for (int i = 0; i < param.Count; i++) {
			char tmp = (char)('a' + i);
			string id = "" + tmp;
			expCorrect.Parameters[id].Value = param[i]; // set the named parameter 
		}
	}
}
