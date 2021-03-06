﻿using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEditor;

public class CaseValidation : MonoBehaviour, ValidationMethod
{
    // les questions de l'énigme, et le taux de réussite associé à la réponse choisi
    // la réponse choisi est contenu dans l'objet question, en indice UserChoice (le taux aussi tu me diras, mais à un niveau plus profond que la réponse)
    private Dictionary<ChoiceQuestion, float> successByQuestion;


    public void Awake(){
      successByQuestion = new Dictionary<ChoiceQuestion, float>();

    }

    public void Start()
    {


    }
    // rempli le dico avec toute les questions et tous les points (%) associés au réponses choisies à ces questions
    private void getQuestionsValidation(){
      if(successByQuestion.Count == 0){
        ChoiceQuestion[] questionList = GameObject.Find("Parts").GetComponentsInChildren<ChoiceQuestion>(true);
        foreach(ChoiceQuestion question in questionList){
          successByQuestion.Add(question, question.getAnswerValidation());
        }
      }
    }

    public bool answerIsRight(){
      float result = 0;
        // évite la division par 0 en dessous
        if (successByQuestion.Count == 0){
            return false;
        }
      foreach(KeyValuePair<ChoiceQuestion, float> question in successByQuestion){
        result += question.Value;
      }
      result = result/successByQuestion.Count;
      result = (result < 0)? 0: (result > 100)? 100: result;
      return (result >= 50);
    }


    public Score fillScore(Score score){
        // on récupère les réponses
      getQuestionsValidation();
      foreach(KeyValuePair<ChoiceQuestion, float> question in successByQuestion){
        score.addEnigmaSuccess(question.Key.professionalSituationId, question.Value);
      }
      score.enigmaSuccess = answerIsRight();
      return score;

    }

    public string getScoreFeedback(){
      string feedback = "\n\nRésultat : \n\n";
      int count = 0;
      foreach(KeyValuePair<ChoiceQuestion, float> question in successByQuestion){
        count ++;
        feedback += "Question "+ count + " : " + question.Key.text + "\nRéussite : " + question.Value + "%\n\n";
      }
      return feedback;
    }








}
