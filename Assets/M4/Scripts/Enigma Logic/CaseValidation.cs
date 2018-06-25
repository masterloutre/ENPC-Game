using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class CaseValidation : MonoBehaviour, ValidationMethod
{
    /*Référence à faire dans unity:
     - flèche pour changer de question : arrowclicked(this)
     - Entryxxx réponse : eventtrigger enter(colorchange(this)) exit(colorback(this)) click(answerselected(this))
     */

    private GameObject stepsDots; // rpz les points bleu du style carousel
    private GameObject entries; // rpz les réponses possibles
    int activeDot; // rpz l'index de la question en cours
    private int[] answerSheet; // rpz la fiche de réponse sélectionnées par question
    private string[] answers; // rpz les bonnes réponses

    Color unselected, selected; // rpz les couleurs des dots

    public void Start()
    {
        ColorUtility.TryParseHtmlString("#1067AC", out unselected);
        ColorUtility.TryParseHtmlString("#459FE7", out selected);

        activeDot = 0;
        // taille dépendante du nombre de questions intermédiaire
        answerSheet = new int[] { -1, -1, -1 };

        // Réponse fourni par enigma manager
        answers = new string[] { "heeeeeey", "hoooooooooo", "yoooolooooo" };


        stepsDots = GameObject.Find("StepsDots");
        entries = GameObject.Find("Entries");
        EventManager.instance.AddListener<RequestNextQuestionEvent>(nextQuestion);
        EventManager.instance.AddListener<RequestSelectionEvent>(answerSelection);
    }


    public bool answerIsRight()
    {
        GameObject go;
        for (int i = 0; i < answerSheet.Length; i++)
        {
            if (answerSheet[i] == -1)
            {
                print("NOT ANSWERED");
                return false;
            }
            go = entries.transform.GetChild(answerSheet[i]).gameObject;
            if (go.GetComponentInChildren<Text>().text != answers[i])
            {
                print("WRONNNNNNNG");
                return false;
            }
        }
        return true;
    }

    // Soulève un event de type " a cliqué sur la flèche "
    // à relier à un gameobject flèche en onclick
    public void arrowClicked(GameObject go)
    {
        if (go.name == "left_arrow")
        {
            EventManager.instance.Raise(new RequestNextQuestionEvent(go.name, 0));
        }
        if (go.name == "right_arrow")
        {
            EventManager.instance.Raise(new RequestNextQuestionEvent(go.name, 1));
        }
    }
    // Soulève un event de type " a sélectionné une réponse "
    // à relier à un gameobject entry en onclick
    public void answerSelected(GameObject go)
    {
        EventManager.instance.Raise(new RequestSelectionEvent(go.name, go.transform.GetSiblingIndex()));

    }



    // Change la couleur de la réponse sélectionné à la réception d'un event " a sélectionné une réponse "
    // devient bleu, et l'ancienne réponse sélectionnée - s'il y en avait une - devient blanche
    public void answerSelection(RequestSelectionEvent e)
    {
        int indextocolorback = answerSheet[activeDot];
        colorChange(entries.transform.GetChild(e.choiceId).gameObject);
        answerSheet[activeDot] = e.choiceId;
        colorBack(entries.transform.GetChild(indextocolorback).gameObject);
    }


    // Change la question en cours et réactualise les couleurs des réponses sélectionnées précédemment
    public void nextQuestion(RequestNextQuestionEvent e)
    {
        if (e.choiceId == 0)
        {
            stepsDots.transform.GetChild(activeDot).transform.GetComponent<Image>().color = unselected;
            if (activeDot == 0)
            {
                activeDot = answerSheet.Length - 1;
            }
            else
            {
                activeDot--;
            }

            stepsDots.transform.GetChild(activeDot).transform.GetComponent<Image>().color = selected;
        }

        if (e.choiceId == 1)
        {
            stepsDots.transform.GetChild(activeDot).transform.GetComponent<Image>().color = unselected;
            activeDot = (activeDot + 1) % answerSheet.Length;
            stepsDots.transform.GetChild(activeDot).transform.GetComponent<Image>().color = selected;
        }
        colorRefresh();


    }

    // Recolorie les réponses, blanc par défaut et bleu si sélectionné
    public void colorRefresh()
    {
        GameObject go;
        for (int i = 0; i < entries.transform.childCount; i++)
        {
            go = entries.transform.GetChild(i).gameObject;
            if (i == answerSheet[activeDot])
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
        Color outcolor;
        ColorUtility.TryParseHtmlString("#64E8FF", out outcolor);
        go.GetComponentInChildren<Text>().GetComponent<Text>().color = outcolor;

        go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;


    }
    // Colorie en blanc une réponse, sauf si elle est sélectionné comme réponse finale par l'user
    public void colorBack(GameObject go)
    {
        if (go.transform.GetSiblingIndex() == answerSheet[activeDot])
        {
            return;
        }
        Color outcolor;
        ColorUtility.TryParseHtmlString("#FFFFFF", out outcolor);
        go.GetComponentInChildren<Text>().GetComponent<Text>().color = outcolor;

        go.GetComponentInChildren<Image>().GetComponent<Image>().color = outcolor;


    }


    
}
