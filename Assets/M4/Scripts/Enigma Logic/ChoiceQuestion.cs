using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


/**
 * Component qui représente une question à choix multiples
 * @type {[type]}
 */
public class ChoiceQuestion : MonoBehaviour {
	public string text;
	public Answer[] answerList;
	private int userChoice;
	public int professionalSituationId;

    // #64E8FF pour du bleu cyan
    public string normalColor, selectedColor; //hexa

	// Use this for initialization
	public void Start () {
        userChoice = -1;
        print("Start:" + gameObject.name);
        gameObject.transform.Find("QuestionText").GetComponent<Text>().text = this.text;
        createAnswerGameObject();
    }
    

//crée les gameObject des réponse et leur assigne un script au click
	public void createAnswerGameObject(){
        print(gameObject.name);
		GameObject answerModel = gameObject.transform.Find("AnswerList").Find("Answer").gameObject;
        //GameObject answerModel = gameObject.transform.Find("Answer").gameObject
        foreach (Answer answer in answerList){
			GameObject answerGameObject = GameObject.Instantiate(answerModel, answerModel.transform.parent);
			answerGameObject.GetComponentInChildren<Text>().text = answer.text;
			answerGameObject.GetComponent<Button>().onClick.AddListener( delegate { setUserChoice(answerGameObject.transform.GetSiblingIndex());});
		}
		Destroy(answerModel);
	}
    public int getUserChoice()
    {
        int i = userChoice;
        return i ;
    }
	//assigne à l'attribut userChoice le bon index et change les couleurs des boutons
	public void setUserChoice(int id){
        // passer par gameobject et non pas GameObejct sinon la recherche se fait globale et il prend le premier venu, donc pas forcément celui appartenant à cette question
        // ce qui était le cas : les valeurs prises ici étaient systématiquement celle de la première question dans la hiérarchie
        // now it's fixed

        GameObject answerGOList = gameObject.transform.Find("AnswerList").gameObject;
		print("(SetUserChoice) Vous avez sélectionné la réponse numéro: "+id);
		if(userChoice != -1){
			colorBack(answerGOList.transform.GetChild(userChoice).gameObject);
		}
		userChoice = id;
		colorChange(answerGOList.transform.GetChild(userChoice).gameObject);
	}

	//retourne le pourcentage de balidation de la question
	public float getAnswerValidation()
    {
        print("(getAnswerValidation) Votre réponse:");
        print("Texte: " + answerList[userChoice].text);
        print("Numéro du choix: " + userChoice);
        print("Valeur de point accordé(%) : " + answerList[userChoice].percent);
        
        try
        {
            
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
			ColorUtility.TryParseHtmlString(selectedColor, out outcolor);
			go.GetComponentInChildren<Text>().GetComponent<Text>().color = outcolor;

			go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;


	}

	// Colorie en blanc une réponse, sauf si elle est sélectionné comme réponse finale par l'user
	public void colorBack(GameObject go)
	{
			Color outcolor;
			ColorUtility.TryParseHtmlString(normalColor, out outcolor);
			go.GetComponentInChildren<Text>().GetComponent<Text>().color = outcolor;

			go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;
	}

}
