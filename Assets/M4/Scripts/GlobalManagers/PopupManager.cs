﻿using UnityEngine;
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


    public void Start()
    {
        justify = Instantiate(justify_model, GameObject.Find("Answer Popup").transform);
        victory = Instantiate(victory_model, GameObject.Find("Answer Popup").transform);
        defeat = Instantiate(defeat_model, GameObject.Find("Answer Popup").transform);
        correct = Instantiate(correct_model, GameObject.Find("Answer Popup").transform);
        certitudelvl = -1;
        methodchoice = errorchoice = -1;
        state = "none";
        answerblock= answerblock = justify.transform.Find("ChoiceButtonS").gameObject;
        EventManager.instance.AddListener<ValidationScreenEvent>((e)=> { });
        EventManager.instance.AddListener<ConfidanceErrorItemSelectionEvent>(answerSelection);

        //configurer le prefab pour le relier à ces variables de récupération

    }


    public void answerSelected(GameObject go)
    {
        EventManager.instance.Raise(new ConfidanceErrorItemSelectionEvent(go.transform.GetSiblingIndex()));

    }
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
        colorBack(answerblock.transform.GetChild(indextocolorback).gameObject);

    }
    // Colorie en bleu clair une réponse
    public void colorChange(GameObject go)
    {
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
    public void submit()
    {
        switch (state)
        {
            case "Justification":
                {
                    EventManager.instance.Raise(new ValidationScreenEvent(state,GameObject.Find("ChoiceButtonS").transform.GetChild(methodchoice).GetComponent<Text>().text,certitudelvl));
                }
                break;
            case "Correction":
                {
                    EventManager.instance.Raise(new ValidationScreenEvent(state, GameObject.Find("ChoiceButtonS").transform.GetChild(errorchoice).GetComponent<Text>().text));
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

    

    public void justifyScreen()
    {
        justify.SetActive(true);
        state = "Justification";
        answerblock = justify.transform.Find("ChoiceButtonS").gameObject;
    }

    public void victoryScreen()
    {
        justify.SetActive(false);
        victory.SetActive(true);
        state = "Victoire";

    }

    public void defeatScreen()
    {
        justify.SetActive(false);
        defeat.SetActive(true);
        state = "Défaite";
    }

    public void correctScreen()
    {
        defeat.SetActive(false);
        correct.SetActive(true);
        state = "Correction";
        answerblock = correct.transform.Find("ChoiceButtonS").gameObject;
    }

    
    // where does it raise, in enigmauimanager

    //what do you do then
}