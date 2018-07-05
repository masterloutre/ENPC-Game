using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChoiceQuestion : MonoBehaviour {
	public string text;
	public Answer[] answerList;
	private int userChoice;
	public int professionalSituationId;


	// Use this for initialization
	public void Start () {
		userChoice = -1;
		GameObject.Find("QuestionText").GetComponent<Text>().text = this.text;
		createAnswerGameObject();
	}

//crée les gameObject des réponse et leur assigne un script au click
	public void createAnswerGameObject(){
		GameObject answerModel = GameObject.FindGameObjectWithTag("CaseStudyAnswer");
		foreach(Answer answer in answerList){
			GameObject answerGameObject = GameObject.Instantiate(answerModel, answerModel.transform.parent);
			answerGameObject.GetComponentInChildren<Text>().text = answer.text;
			answerGameObject.GetComponent<Button>().onClick.AddListener( delegate { setUserChoice(answerGameObject.transform.GetSiblingIndex());});
		}
		Destroy(answerModel);
	}

	//assigne à l'attribut userChoice le bon index et change les couleurs des boutons
	public void setUserChoice(int id){
		GameObject[] answerGOList = GameObject.FindGameObjectsWithTag("CaseStudyAnswer");
		print(id);
		if(userChoice != -1){
			colorBack(answerGOList[userChoice]);
		}
		userChoice = id;
		colorChange(answerGOList[userChoice]);
	}

	//retourne le pourcentage de balidation de la question
	public float getAnswerValidation(){
		try{
			print("answer selected : " + answerList[userChoice].text + ", percent : " + answerList[userChoice].percent);
			return answerList[userChoice].percent;
		} catch( Exception e){
			print(e.Message);
		}
		return 0F;
	}


	// Colorie en bleu clair une réponse
	public void colorChange(GameObject go)
	{
			print("Coloring : " + go.name);
			Color outcolor;
			ColorUtility.TryParseHtmlString("#64E8FF", out outcolor);
			go.GetComponentInChildren<Text>().GetComponent<Text>().color = outcolor;

			go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;


	}

	// Colorie en blanc une réponse, sauf si elle est sélectionné comme réponse finale par l'user
	public void colorBack(GameObject go)
	{
			Color outcolor;
			ColorUtility.TryParseHtmlString("#FFFFFF", out outcolor);
			go.GetComponentInChildren<Text>().GetComponent<Text>().color = outcolor;

			go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;
	}

}
