using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using System.Collections.Generic;


public enum PopupState { NONE, CERTAINTY, FAILURE, SUCCESS, METHOD };


/*
 * Pour utiliser PopupManager, vous devez créer un gameobject nommé "Answer Popup" dans le Canvas.
 * Il faut ensuite insérer ce script dans le gameobject contenant le script EnigmaSceneManager, et le compléter avec les préfab.
 *
*/
public class PopupManager : MonoBehaviour
{
    int certitudelvl; // Niveau de certitude

    public string methodeUserInput { get; private set;}
    public float certitudeUserInput { get; private set;}
    PopupState state; // Étape en cours, peut valoir : "none", "Certitude", "Justification", "Correction", "Victoire", FAILURE


    //private ChoiceQuestion methodquestions,justifyquestions;
    private GameObject sure, victory, defeat, method, validationButton; // Écrans des étapes
    private List<ChoiceQuestion> questionList;
    int currentMethodQuestionIndex = 0;

    private ValidationMethod validator;
    private Score enigmaScore;



    public void Awake()
    {
        // INSTANCIATION des modèles, masqués par défaut

        sure = GameObject.Find("Certitude");
        victory = GameObject.Find("Victoire");
        defeat = GameObject.Find("Défaite");
        method = GameObject.Find("Méthode");
        validationButton = GameObject.Find("ValidationButton");

        questionList = new List<ChoiceQuestion>();
        foreach(ChoiceQuestion question in GameObject.Find("Méthode").GetComponentsInChildren<ChoiceQuestion>()){
          questionList.Add(question);
          question.gameObject.SetActive(false);
          print("question added to list :" + question.text);
        }
        questionList[0].gameObject.SetActive(true);

        sure.SetActive(false);
        victory.SetActive(false);
        defeat.SetActive(false);
        method.SetActive(false);
        validationButton.SetActive(false);


        certitudelvl = -1;
        methodeUserInput = "";
        certitudeUserInput = 0F;
        state = PopupState.NONE;

        // pour créer dynamiquement des boutons custom
        //methodquestions = method.GetComponent<ChoiceQuestion>();
        //justifyquestions = justify.GetComponent<ChoiceQuestion>();


        validator = gameObject.GetComponent<PopupValidation>();

    }
    public void Start()
    {
        setValidationButton();
    }
    void OnDisable()
    {
        Debug.Log("PrintOnDisable: script PopupManager was disabled");

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
                    certitudeUserInput = GameObject.Find("Slider").GetComponent<Slider>().value;
                    if (enigmaScore.enigmaSuccess){
                        updateState(PopupState.SUCCESS);
                    } else {
                        updateState(PopupState.FAILURE);
                    }
                }
                break;
            case PopupState.METHOD:
                {
                  print("GO : question index " + currentMethodQuestionIndex + ", question count : " + questionList.Count );
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
            case PopupState.SUCCESS:
                {
                    updateState(PopupState.METHOD);
                }break;

            case PopupState.FAILURE:
                {
                    updateState(PopupState.METHOD);
                }break;

            default:
                {
                    return;
                }
        }

    }
    // méthode d'affichage
    public void updateState(PopupState newState)
    {
        print("CALLLING UPDATE STATE (" + newState+ ")");
        if (newState != null)
        {
            state = newState;
            displayScreen();
        }
    }
    public void displayScreen()
    {
        EventManager.instance.Raise(new RequestDisableEnigmaUIEvent());
        validationButton.SetActive(true);
        switch (state)
        {
            case PopupState.CERTAINTY:
                {
                    method.SetActive(false);
                    victory.SetActive(false);
                    defeat.SetActive(false);
                    sure.SetActive(true);

                    certitudelvl = 0;

                }
                break;

            case PopupState.SUCCESS:
                {
                    sure.SetActive(false);
                    victory.SetActive(true);
                }
                break;
            case PopupState.FAILURE:
                {
                    sure.SetActive(false);
                    defeat.SetActive(true);

                }
                break;

            case PopupState.METHOD:
                {
                    victory.SetActive(false);
                    defeat.SetActive(false);
                    method.SetActive(true);
                    certitudelvl = -1;

                }
                break;
            default:
                {
                    print("REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                    return;
                }
        }
    }

    public void beginPopUpQuestionsSequence(Score score){
      enigmaScore = score;
      updateState(PopupState.CERTAINTY);
    }

    public void endPopUpQuestionsSequence(){
      print(enigmaScore.ToString());
      GameObject.Find("Answer Popup").SetActive(false);
      EventManager.instance.Raise(new PopUpQuestionsOverEvent());
    }



}
