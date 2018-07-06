using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

/*
 * Pour utiliser PopupManager, vous devez créer un gameobject nommé "Answer Popup" dans le Canvas.
 * Il faut ensuite insérer ce script dans le gameobject contenant le script EnigmaSceneManager, et le compléter avec les préfab.
 *
*/
public class PopupManager : MonoBehaviour
{
    bool enigmaSuccess;
    int certitudelvl; // Niveau de certitude

    public string methodeUserInput { get; private set;}
    public float certitudeUserInput { get; private set;}
    string state; // Étape en cours, peut valoir : "none", "Certitude", "Justification", "Correction", "Victoire", "Défaite"
    

    public ChoiceQuestion correctquestions,justifyquestions;
    private GameObject sure, justify, victory, defeat, correct; // Écrans des étapes



    public void Awake()
    {
        // INSTANCIATION des modèles, masqués par défaut

        sure = GameObject.Find("Certitude");
        justify = GameObject.Find("Justification");
        victory = GameObject.Find("Victoire");
        defeat = GameObject.Find("Défaite");
        correct = GameObject.Find("Correction");

        sure.SetActive(false);
        justify.SetActive(false);
        victory.SetActive(false);
        defeat.SetActive(false);
        correct.SetActive(false);

        certitudelvl = -1;
        methodeUserInput = "";
        certitudeUserInput = 0F;
        state = "none";

        // pour créer dynamiquement des boutons custom
        correctquestions = correct.GetComponent<ChoiceQuestion>();
        justifyquestions = justify.GetComponent<ChoiceQuestion>();
        enigmaSuccess = false;

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

        GameObject go;

        // " Justification "
        print(justify);
        go = justify.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // " Correction "
        go = correct.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // " Victoire "
        go = victory.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // " Défaite "
        go = defeat.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // " Certitude "
        go = sure.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);
    }
    public string getState()
    {
        return state;
    }
    public void setEnigmaSuccess(bool success)
    {
        enigmaSuccess = success;
    }
    public bool getEnigmaSuccess()
    {
        return enigmaSuccess;
    }



    // Activé lorsque l'on confirme une sélection
    public void submit()
    {
        switch (state)
        {
            case "Certitude":
                {
                    certitudeUserInput = GameObject.Find("Slider").GetComponent<Slider>().value;
                    if (enigmaSuccess){
                        updateState("Victoire");
                    } else {
                        updateState("Défaite");
                    }
                }
                break;
            case "Correction":
                {
                    // pour ne pas confondre answer list de popup manager avec answerlist de casestudy
                    print(correctquestions.getUserChoice());
                    methodeUserInput = GameObject.Find("Answer Popup").transform.Find("Correction").transform.Find("AnswerList").GetChild(correctquestions.getUserChoice()).GetComponentInChildren<Text>().text;
                    endPopUpQuestionsSequence();
                }
                break;
            case "Justification":
                {
                    // pour ne pas confondre answer list de popup manager avec answerlist de casestudy
                    print(justifyquestions.getUserChoice());
                    methodeUserInput = GameObject.Find("Answer Popup").transform.Find("Justification").transform.Find("AnswerList").GetChild(justifyquestions.getUserChoice()).GetComponentInChildren<Text>().text;
                    endPopUpQuestionsSequence();
                }break;

            case "Victoire":
                {
                    updateState("Justification");
                }break;

            case "Défaite":
                {
                    updateState("Correction");
                }break;

            default:
                {
                    return;
                }
        }

    }
    // méthode d'affichage
    public void updateState(string value)
    {
        print("CALLLING UPDATE STATE (" + value + ")");
        if (value == "Certitude" || value == "Justification" || value == "Victoire" || value == "Défaite" || value == "Correction")
        {
            state = value;
            displayScreen();
        }
        else
        {
        }
    }
    public void displayScreen()
    {
        EventManager.instance.Raise(new RequestDisableEnigmaUIEvent());
        switch (state)
        {
            case "Certitude":
                {
                    correct.SetActive(false);
                    victory.SetActive(false);
                    defeat.SetActive(false);
                    justify.SetActive(false);
                    sure.SetActive(true);
                    
                    certitudelvl = 0;

                }
                break;

            case "Victoire":
                {
                    sure.SetActive(false);
                    victory.SetActive(true);
                }
                break;
            case "Défaite":
                {
                    sure.SetActive(false);
                    defeat.SetActive(true);

                }
                break;

            case "Justification":
                {
                    victory.SetActive(false);
                    justify.SetActive(true);
                    certitudelvl = -1;
                    
                }
                break;
            case "Correction":
                {
                    defeat.SetActive(false);
                    correct.SetActive(true);
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


    public void endPopUpQuestionsSequence(){
      GameObject.Find("Answer Popup").SetActive(false);
      EventManager.instance.Raise(new PopUpQuestionsOverEvent());
    }



}
