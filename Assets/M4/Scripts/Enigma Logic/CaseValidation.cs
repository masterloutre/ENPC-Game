﻿using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEditor;

public class CaseValidation : MonoBehaviour, ValidationMethod
{
    /*Référence à faire dans unity:
     - flèche pour changer de question : arrowclicked(this)
     - Entryxxx réponse : eventtrigger enter(colorchange(this)) exit(colorback(this)) click(answerselected(this))
     */

    
    private GameObject activeEntries; // rpz les réponses possibles affichées
    int activeQuestionIndex; // rpz l'index de la question en cours dans les deux tableaux de réponses answersheet et answers

    private int[] answerSheet; // rpz la fiche de réponse sélectionnées par question
    public string[] answers; // rpz les bonnes réponses

    public string selectionColor = "#459FE7";
    public string normalColor = "#1067AC";
    private Color unselected, selected; // rpz les couleurs des dots
    
    


    public void Start()
    {
        print("Starting CASEVALIDATION");
        ColorUtility.TryParseHtmlString(normalColor, out unselected);
        ColorUtility.TryParseHtmlString(selectionColor, out selected);

        activeQuestionIndex = 0;
        // initialise answersheet à la bonne taille, avec que des -1
        answerSheet = Enumerable.Repeat<int>(-1, findQuestion().ToArray().Length).ToArray();

        // scripter les choix
        scriptthisshit(findQuestion().ToArray());

        EventManager.instance.AddListener<RequestNextQuestionEvent>(nextQuestion);
        
    }

    public void OnDestroy()
    {
        EventManager.instance.RemoveListener<RequestNextQuestionEvent>(nextQuestion);
    }

    private void scriptthisshit(GameObject[] questions)
    {
        foreach(GameObject question in questions)
        {
            foreach(Transform entry in question.transform.Find("Entries"))
            {
                entry.gameObject.GetComponent<Button>().onClick.AddListener(delegate { answerSelected(entry.gameObject); });
            }
        }
    }
    private List<GameObject> findQuestion()
    {
        List<GameObject> list= new List<GameObject>();
        GameObject Parts = GameObject.Find("Parts");
        foreach(Transform go in Parts.transform)
        {
            if (go.name.Contains("Question"))
            {
                list.Add(go.gameObject);
            }
        }
        return list;
    }
    
    public float score()
    {
        return 0.0f;
    }
    public bool answerIsRight()
    {
        GameObject go;
        GameObject[] questions = findQuestion().ToArray();
        for (int i = 0; i < answerSheet.Length; i++)
        {
            if (answerSheet[i] == -1)
            {
                print("NOT ANSWERED");
                return false;
            }
            go = questions[i].transform.Find("Entries").GetChild(answerSheet[i]).gameObject;
            print("Comparaison : " + go.GetComponentInChildren<Text>().text + " VS " + answers[i]);
            if (go.GetComponentInChildren<Text>().text != answers[i])
            {
                print("WRONNNNNNNG");
                return false;
            }
        }
        return true;
    }

    
     
    // Soulève un event de type " a sélectionné une réponse "
    // à relier à un gameobject entry en onclick
    public void answerSelected(GameObject go)
    {
        int indextocolorback = answerSheet[activeQuestionIndex];
        colorChange(activeEntries.transform.GetChild(go.transform.GetSiblingIndex()).gameObject);
        answerSheet[activeQuestionIndex] = go.transform.GetSiblingIndex();
        colorBack(activeEntries.transform.GetChild(indextocolorback).gameObject);
    }
    


    // Change la question en cours et réactualise les couleurs des réponses sélectionnées précédemment
    public void nextQuestion(RequestNextQuestionEvent e)
    {
        int res;
        string nb = new String(e.currentSceneName.Where(Char.IsDigit).ToArray());
        int.TryParse(nb,out res);
        activeQuestionIndex = res-1;

        if (e.currentSceneName.Contains("Question"))
        {
            activeEntries = GameObject.Find(e.currentSceneName).transform.Find("Entries").gameObject;
            print(activeEntries.name);
            colorRefresh();
        }
        


    }

    // Recolorie les réponses, blanc par défaut et bleu si sélectionné
    public void colorRefresh()
    {
        GameObject go;
        print("voici la réponse choisi dans answersheet[" + activeQuestionIndex + "] : " + answerSheet[activeQuestionIndex]);
        for (int i = 0; i < activeEntries.transform.childCount; i++)
        {
            go = activeEntries.transform.GetChild(i).gameObject;
            // answersheet contient -1 si c'est la 1er fois, sinon on se contente de recolorier ce qui a été choisi avant
            
            if (i == answerSheet[activeQuestionIndex])
            {
                colorChange(go);
            }
            else
            {
                colorBack(go);
            }
        }
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
        print("Coloring back: " + go.name);
        if (go.transform.GetSiblingIndex() == answerSheet[activeQuestionIndex])
        {
            return;
        }
        Color outcolor;
        ColorUtility.TryParseHtmlString("#FFFFFF", out outcolor);
        go.GetComponentInChildren<Text>().GetComponent<Text>().color = outcolor;

        go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;


    }


    
}
// Soulève un event de type " a cliqué sur la flèche "
// à relier à un gameobject flèche en onclick
//public void arrowClicked(GameObject go)
//{
//    if (go.name == "left_arrow")
//    {
//        EventManager.instance.Raise(new RequestNextQuestionEvent(go.name, 0));
//    }
//    if (go.name == "right_arrow")
//    {
//        EventManager.instance.Raise(new RequestNextQuestionEvent(go.name, 1));
//    }
//}
// Change la couleur de la réponse sélectionné à la réception d'un event " a sélectionné une réponse "
// devient bleu, et l'ancienne réponse sélectionnée - s'il y en avait une - devient blanche
//public void answerSelection(RequestSelectionEvent e)
//{
//    print("indice de choix: "+e.choiceId);
//    print("entries sélectionné: " + entries.transform.GetChild(e.choiceId));
//    int indextocolorback = answerSheet[activeDot];
//    colorChange(entries.transform.GetChild(e.choiceId).gameObject);
//    answerSheet[activeDot] = e.choiceId;
//    colorBack(entries.transform.GetChild(indextocolorback).gameObject);
//}