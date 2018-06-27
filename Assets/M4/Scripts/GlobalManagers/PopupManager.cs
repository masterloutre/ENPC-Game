using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    int certitudelvl; // Niveau de certitude
    int methodchoice; // Indice du choix de réponse de justification
    string state; // Étape en cours, peut valoir : "none", "Certitude", "Justification", "Correction", "Victoire", "Défaite"

    public GameObject sure_model, justify_model, victory_model, defeat_model,correct_model; // Variable de référence des Prefabs correspondant à chaque étape, doivent être attribués depuis l'éditeur
    private GameObject sure, justify, victory, defeat, correct; // Écrans des étapes
    private GameObject answerblock; // Les justifications possible
    

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
        state = "none";
        answerblock = justify.transform.Find("ChoiceButtonS").gameObject;
    }
    public void Start()
    {
        // LISTENERS
        EventManager.instance.AddListener<ConfidanceErrorItemSelectionEvent>(answerSelection); // En réponse à la sélection d'un choix
        scriptThisShit();
        //configurer le prefab pour le relier à ces variables de récupération

    }
    void OnDestroy()
    {
        EventManager.instance.RemoveListener<ConfidanceErrorItemSelectionEvent>(answerSelection);
    }
    private void scriptThisShit()
    {
        print("SCRIPT THIS SHIT EST LANCE");
        // Scripting " Justification " gameobject
        GameObject go = justify.transform.Find("ChoiceButtonS").gameObject;
        
        for (int i=0; i < go.transform.childCount; i++)
        {
            
            GameObject tmp = go.transform.GetChild(i).gameObject;
            tmp.GetComponent<Button>().onClick.AddListener(delegate {answerSelected(tmp); });
            
        }
        
        go = justify.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // Scripting " Correction " gameobject
        go = correct.transform.Find("ChoiceButtonS").gameObject;
        for (int i = 0; i < go.transform.childCount; i++)
        {
            
            GameObject tmp = go.transform.GetChild(i).gameObject;
            tmp.GetComponent<Button>().onClick.AddListener(delegate {answerSelected(tmp); });

        }
        go = correct.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // Scripting " Victoire " gameobject
        go = victory.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // Scripting " Défaite " gameobject
        go = defeat.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // Scripting " Certitude " gameobject
        go = sure.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);
    }

    public string getState()
    {
        return state;
    }

    // Renvoie l'indice d'enfant du go parmi les réponses possibles du bouton cliqué
    public void answerSelected(GameObject go)
    {

        EventManager.instance.Raise(new ConfidanceErrorItemSelectionEvent(go.transform.GetSiblingIndex()));
    }
    // Colorie la sélection
    public void answerSelection(ConfidanceErrorItemSelectionEvent e)
    {
        int indextocolorback=-1 ;
        colorChange(answerblock.transform.GetChild(e.choiceindex).gameObject);
        indextocolorback = methodchoice;
        methodchoice= e.choiceindex;
        
        if (indextocolorback != -1)
        {
            colorBack(answerblock.transform.GetChild(indextocolorback).gameObject);
        }
        

    }
    // Colorie en bleu clair une réponse
    public void colorChange(GameObject go)
    {
        Color outcolor;
        ColorUtility.TryParseHtmlString("#1E366D", out outcolor);
        go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;


    }
    // Colorie en blanc une réponse, sauf si elle est sélectionné comme réponse finale par l'user
    public void colorBack(GameObject go)
    {
        
        Color outcolor;
        ColorUtility.TryParseHtmlString("#080C15", out outcolor);
        
        go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;


    }
    // Activé lorsque l'on confirme une sélection, lève un event contenant la réponse choisie
    public void submit()
    {
        switch (state)
        {
            case "Certitude":
                {
                    try
                    {
                        EventManager.instance.Raise(new ValidationScreenEvent(state, GameObject.Find("Slider").GetComponent<Slider>().value));
                    }
                    catch
                    {
                        throw new Exception("WRYYYYYY MANQUE UN ARGUMENT POUR RAISE");
                    }
                }
                break;
            case "Correction":
            case "Justification":
                {
                    try
                    {
                        EventManager.instance.Raise(new ValidationScreenEvent(state, GameObject.Find("ChoiceButtonS").transform.GetChild(methodchoice).GetComponentInChildren<Text>().text));
                    } catch {
                        throw new Exception("WRYYYYYY MANQUE UN ARGUMENT POUR RAISE");
                    }


                }break;
        
            case "Victoire":
            case "Défaite":
                {
                    EventManager.instance.Raise(new ValidationScreenEvent(state));
                }
                break;
            default:
                {
                    print("reeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
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
            print("[ PopupManager.updateState a crashé ] WHAT IS THAT VALUE REEEEEEEEE : " + value);
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
                    answerblock = justify.transform.Find("ChoiceButtonS").gameObject;
                }
                break;
            case "Correction":
                {
                    defeat.SetActive(false);
                    correct.SetActive(true);

                    methodchoice = certitudelvl = -1;
                    answerblock = correct.transform.Find("ChoiceButtonS").gameObject;
                }
                break;

            default:
                {
                    print("REEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                    return;
                }
        }
    }

    
}