using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using System.Collections.Generic;


public enum PopupState { ACTIVATED, CERTAINTY, SUCCESSORNOT, METHOD, DEACTIVATED};


/*
 * Pour utiliser PopupManager, vous devez créer un gameobject nommé "Answer Popup" dans le Canvas.
 * Il faut ensuite insérer ce script dans le gameobject contenant le script EnigmaSceneManager, et le compléter avec les préfab.
 *
*/
public class PopupManager : MonoBehaviour
{
    PopupState state = PopupState.ACTIVATED;
    private GameObject sure, victory, defeat, method, validationButton; // Écrans des étapes
    private List<ChoiceQuestion> questionList;
    int currentMethodQuestionIndex = 0;
    private ValidationMethod validator;
    private Score enigmaScore;



    public void Awake()
    {
        // INSTANCIATION des modèles, masqués par défaut
        /*
        Transform[] tranformList = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in tranformList )
        {
          if (child != transformList[0]){
            child.gameObject.SetActive(true);
          }
        }
        */
        sure = gameObject.transform.Find("Certitude").gameObject;
        victory = gameObject.transform.Find("Victoire").gameObject;
        defeat = gameObject.transform.Find("Défaite").gameObject;
        method = gameObject.transform.Find("Méthode").gameObject;
        validationButton = gameObject.transform.Find("ValidationButton").gameObject;

        questionList = new List<ChoiceQuestion>();
        foreach(ChoiceQuestion question in GameObject.Find("Méthode").GetComponentsInChildren<ChoiceQuestion>()){
          questionList.Add(question);
          question.gameObject.SetActive(false);
        }
        questionList[0].gameObject.SetActive(true);

        sure.SetActive(false);
        victory.SetActive(false);
        defeat.SetActive(false);
        method.SetActive(false);
        validationButton.SetActive(false);

        validator = gameObject.GetComponent<PopupValidation>();

    }
    public void Start()
    {
        setValidationButton();
    }
    void OnDisable()
    {
        //Debug.Log("PrintOnDisable: script PopupManager was disabled");

    }

    // Pour scripter les boutons de validations
    private void setValidationButton()
    {
        validationButton.GetComponent<Button>().onClick.AddListener(submit);

    }
    public PopupState getState()
    {
        return state;
    }
    public void setScore(Score score){
        enigmaScore = score;
    }



    // Activé lorsque l'on confirme une sélection
    public void submit()
    {
        switch (state)
        {
            case PopupState.CERTAINTY:
                {
                    enigmaScore.certaintyLevel = GameObject.Find("Slider").GetComponent<Slider>().value;
                    updateState(PopupState.SUCCESSORNOT);
                }
                break;
            case PopupState.METHOD:
                {
                    if(currentMethodQuestionIndex < questionList.Count -1){
                      questionList[currentMethodQuestionIndex].gameObject.SetActive(false);
                      currentMethodQuestionIndex ++;
                      questionList[currentMethodQuestionIndex].gameObject.SetActive(true);
                    } else {
                      enigmaScore = validator.fillScore(enigmaScore);
                      endPopUpQuestionsSequence();
                    }
                }
                break;
            case PopupState.SUCCESSORNOT:
                {
                    updateState(PopupState.METHOD);
                }
                break;
            default:
                {
                    return;
                }
        }

    }
    // méthode d'affichage
    public void updateState(PopupState newState)
    {
        if (newState != null)
        {
            state = newState;
            displayScreen();
        }
    }
    public void displayScreen()
    {
        switch (state)
        {
            case PopupState.CERTAINTY:
                {
                    EventManager.instance.Raise(new RequestDisableEnigmaUIEvent());
                    method.SetActive(false);
                    victory.SetActive(false);
                    defeat.SetActive(false);
                    sure.SetActive(true);
                    validationButton.SetActive(true);
                }
                break;

            case PopupState.SUCCESSORNOT:
                {
                  sure.SetActive(false);
                  if(enigmaScore.enigmaSuccess){
                    victory.SetActive(true);
                  } else {
                    defeat.SetActive(true);
                  }
                }
                break;
            case PopupState.METHOD:
                {
                    victory.SetActive(false);
                    defeat.SetActive(false);
                    method.SetActive(true);
                }
                break;
            case PopupState.DEACTIVATED:
              {
                gameObject.SetActive(false);
              }
              break;
            case PopupState.ACTIVATED:
              {
                gameObject.SetActive(true);
              }
              break;
            default:
                {
                  return;
                }
        }
    }

    public void beginPopUpQuestionsSequence(Score score){
      enigmaScore = score;
      updateState(PopupState.CERTAINTY);
    }

    public void endPopUpQuestionsSequence(){
      gameObject.SetActive(false);
      EventManager.instance.Raise(new PopUpQuestionsOverEvent());
    }

    public Score getScore(){
      return enigmaScore;
    }



}
