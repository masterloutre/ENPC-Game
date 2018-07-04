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
    // pour l'élève
    int certitudelvl; // Niveau de certitude
    int methodchoice; // Indice du choix de réponse de justification

    public string methodeUserInput { get; private set;}
    public float certitudeUserInput { get; private set;}
    string state; // Étape en cours, peut valoir : "none", "Certitude", "Justification", "Correction", "Victoire", "Défaite"
    bool enigmaSuccess;

    // pour le créateur
    public string[] answerList;
    public int goodAnswer; // numéro dans answerList
    // à référencer dans l'éditeur
    public GameObject button_model, sure_model, justify_model, victory_model, defeat_model,correct_model; // Variable de référence des Prefabs correspondant à chaque étape


    private AnswerBlock[] block; // bloc de choix de méthode
    private GameObject sure, justify, victory, defeat, correct; // Écrans des étapes



    public void Awake()
    {
        // INSTANCIATION des modèles, masqués par défaut

        sure = Instantiate(sure_model, GameObject.Find("Answer Popup").transform);
        justify = Instantiate(justify_model, GameObject.Find("Answer Popup").transform);
        victory = Instantiate(victory_model, GameObject.Find("Answer Popup").transform);
        defeat = Instantiate(defeat_model, GameObject.Find("Answer Popup").transform);
        correct = Instantiate(correct_model, GameObject.Find("Answer Popup").transform);

        certitudelvl = -1;
        methodchoice = -1;
        methodeUserInput = "";
        certitudeUserInput = 0F;
        state = "none";

        // pour créer dynamiquement des boutons custom
        block = new AnswerBlock[answerList.Length];
        for ( int i = 0 ; i < answerList.Length ; i++ )
        {
          float offsetY = i / 2 * (-50);
            block[i] = new AnswerBlock(button_model, answerList[i], new Vector2(200 * (i%2) - 100, offsetY));
            GameObject tmp = block[i].go;
            block[i].script(delegate { answerSelected(tmp); });
        }
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
        go = justify.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // " Correction "
        go = correct.transform.Find("ChoiceButtonS").gameObject;

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


    // colorie le go lorsqu'on clique dessus
    public void answerSelected(GameObject go)
    {

        int index = go.transform.GetSiblingIndex();
        int indextocolorback=-1 ;

        colorChange(go);
        indextocolorback = methodchoice; // le choix précédent, s'il y en avait un
        methodchoice= index;

        if (indextocolorback != -1)
        {
            colorBack(block[indextocolorback].go);
        }

    }
    // Colorie en bleu foncé
    public void colorChange(GameObject go)
    {
        Color outcolor;
        ColorUtility.TryParseHtmlString("#1E366D", out outcolor);
        go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;


    }
    // Colorie en noir
    public void colorBack(GameObject go)
    {

        Color outcolor;
        ColorUtility.TryParseHtmlString("#080C15", out outcolor);

        go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;


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
            case "Justification":
                {
                    methodeUserInput = GameObject.Find("ChoiceButtonS").transform.GetChild(methodchoice).GetComponentInChildren<Text>().text;
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
        switch (state)
        {
            case "Certitude":
                {
                    correct.SetActive(false);
                    victory.SetActive(false);
                    defeat.SetActive(false);
                    justify.SetActive(false);
                    sure.SetActive(true);

                    methodchoice = -1;
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

                    methodchoice = certitudelvl = -1;
                    foreach(AnswerBlock ab in block)
                    {
                        ab.parent(justify.transform.Find("ChoiceButtonS").gameObject);
                    }
                }
                break;
            case "Correction":
                {
                    defeat.SetActive(false);
                    correct.SetActive(true);

                    methodchoice = certitudelvl = -1;
                    foreach (AnswerBlock ab in block)
                    {
                        ab.parent(correct.transform.Find("ChoiceButtonS").gameObject);
                    }
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
