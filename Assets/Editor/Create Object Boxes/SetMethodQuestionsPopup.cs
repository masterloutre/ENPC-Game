using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;


/**
 * Modifie le component inputValidation
 * Permet d'entrer la formule de calcule du resultat et la marge d'erreur
 * @type {[type]}
 */
public class SetMethodQuestionsPopup : PopupWindowContent
{
	List<ChoiceQuestion> questionList = new List<ChoiceQuestion>();
	Vector2 scrollPos;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(400, 350);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){

      GUILayout.Label("Configurer les questions de méthode.", EditorStyles.boldLabel);
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(400), GUILayout.Height(300));
			foreach(ChoiceQuestion question in questionList){
				displayQuestionEditor(question);
			}
			//bouton OK
			/*
			if (GUILayout.Button("OK", GUILayout.Width(200))) {
	      setValidator();
	    }
			*/
			EditorGUILayout.EndScrollView();
    }

		public override void OnOpen(){
			GameObject popUpGroup = GameObject.Find("Answer Popup");
			foreach(ChoiceQuestion question in popUpGroup.GetComponentsInChildren<ChoiceQuestion>()){
				questionList.Add(question);
			}
		}

		public void displayQuestionEditor(ChoiceQuestion question){
			GUILayout.BeginVertical("box");
			question.text = EditorGUILayout.TextArea(question.text,  GUILayout.Height(50));
			foreach(Answer answer in question.answerList){
				GUILayout.BeginVertical("box");
				answer.text = EditorGUILayout.TextField("Réponse", answer.text);
				answer.percent = EditorGUILayout.FloatField("Pourcentage de réussite associé à la réponse", answer.percent);
				GUILayout.EndVertical();
			}
			GUILayout.EndVertical();
		}


}
