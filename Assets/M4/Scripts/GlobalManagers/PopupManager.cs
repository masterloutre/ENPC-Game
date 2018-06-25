using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour {
    public GameObject justify_model, victory_model, defeat_model,correct_model; // à relier au prefab dans l'éditeur
    private GameObject justify, victory, defeat, correct;
    private GameObject answerblock;
    int certitudelvl;
    int methodchoice;
    int errorchoice;
    string state;

    public void Awake()
    {
        justify = Instantiate(justify_model, GameObject.Find("Answer Popup").transform);
        victory = Instantiate(victory_model, GameObject.Find("Answer Popup").transform);
        defeat = Instantiate(defeat_model, GameObject.Find("Answer Popup").transform);
        correct = Instantiate(correct_model, GameObject.Find("Answer Popup").transform);
        certitudelvl = -1;
        methodchoice = errorchoice = -1;
        state = "none";
        answerblock = justify.transform.Find("ChoiceButtonS").gameObject;
    }
    public void Start()
    {
        EventManager.instance.AddListener<ConfidanceErrorItemSelectionEvent>(answerSelection);
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
            tmp.GetComponent<Button>().onClick.AddListener(delegate { print(i); answerSelected(tmp); });

        }
        go = correct.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // Scripting " Victoire " gameobject
        go = victory.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);

        // Scripting " Défaite " gameobject
        go = defeat.transform.Find("Validation_button").gameObject;
        go.GetComponent<Button>().onClick.AddListener(submit);
    }

    public string getState()
    {
        return state;
    }

    // Renvoie l'indice d'enfant du go parmi les réponses possibles du bouton cliqué
    public void answerSelected(GameObject go)
    {
        print("selected gameobject : "+ go.transform.GetSiblingIndex());
        EventManager.instance.Raise(new ConfidanceErrorItemSelectionEvent(go.transform.GetSiblingIndex()));
    }
    // Colorie la sélection
    public void answerSelection(ConfidanceErrorItemSelectionEvent e)
    {
        int indextocolorback=-1 ;
        colorChange(answerblock.transform.GetChild(e.choiceindex).gameObject);
        if (state == "Justification")
        {
            indextocolorback = methodchoice;
            
            methodchoice= e.choiceindex;
            
        }
        if (state == "Correction")
        {
            indextocolorback = errorchoice;
            
            errorchoice = e.choiceindex;
            
        }
        print(indextocolorback);
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
            case "Justification":
                {
                    try
                    {
                        EventManager.instance.Raise(new ValidationScreenEvent(state, GameObject.Find("ChoiceButtonS").transform.GetChild(methodchoice).GetComponentInChildren<Text>().text, certitudelvl));
                    }catch{
                        throw new Exception("WRYYYYYY MANQUE UN ARGUMENT POUR RAISE");
                    }
                    
                    
                }
                break;
            case "Correction":
                {
                    try
                    {
                        EventManager.instance.Raise(new ValidationScreenEvent(state, GameObject.Find("ChoiceButtonS").transform.GetChild(errorchoice).GetComponentInChildren<Text>().text));
                    }
                    catch
                    {
                        throw new Exception("WRYYYYYY MANQUE UN ARGUMENT POUR RAISE");
                    }
                    
                }
                break;
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
        if (value == "Justification" || value == "Victoire" || value == "Défaite" || value == "Correction")
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
            case "Justification":
                {
                    correct.SetActive(false);
                    victory.SetActive(false);
                    defeat.SetActive(false);
                    methodchoice=errorchoice=certitudelvl=-1;
                    justify.SetActive(true);
                    state = "Justification";
                    answerblock = justify.transform.Find("ChoiceButtonS").gameObject;
                }
                break;
            case "Victoire":
                {
                    justify.SetActive(false);
                    victory.SetActive(true);
                    state = "Victoire";
                }
                break;
            case "Défaite":
                {
                    justify.SetActive(false);
                    defeat.SetActive(true);
                    state = "Défaite";
                }
                break;
            case "Correction":
                {
                    defeat.SetActive(false);
                    correct.SetActive(true);
                    state = "Correction";
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