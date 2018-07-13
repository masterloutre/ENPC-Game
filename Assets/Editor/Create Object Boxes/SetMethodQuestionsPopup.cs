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
	int selectedIndex = 0;
	//ChoiceQuestion test;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(400, 350);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
			//ActiveEditorTracker.sharedTracker.isLocked = false;
      GUILayout.Label("Configurer les questions de méthode.", EditorStyles.boldLabel);
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(400), GUILayout.Height(300));
			//test.text = EditorGUILayout.TextField("test ",test.text);
			
			foreach(ChoiceQuestion question in questionList){
				displayQuestionEditor(question);
			}
			
			
			EditorGUILayout.EndScrollView();
    }

		public override void OnOpen(){
			GameObject popUpGroup = GameObject.Find("Answer Popup");
			foreach(ChoiceQuestion question in popUpGroup.GetComponentsInChildren<ChoiceQuestion>()){
				questionList.Add(question);
				EditorUtility.SetDirty(question);
			}
			
			//test = popUpGroup.GetComponentsInChildren<ChoiceQuestion>()[0];
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
			if (GUILayout.Button("Ajouter une réponse", GUILayout.Width(200))) {
	      addAnswer(question);
	    }
			deleteAnswerPopup(question);
			if (GUILayout.Button("Supprimer une réponse", GUILayout.Width(200))) {
	      deleteAnswer(question, selectedIndex);
	    }
			question.professionalSituationId = EditorGUILayout.IntField("Identifiant de la situation professionnelle", question.professionalSituationId);
			GUILayout.EndVertical();
		}
		
		public void addAnswer(ChoiceQuestion question){
			question.answerList.Add(new Answer());
		}
		
		public void deleteAnswer(ChoiceQuestion question, int selectedIndex){
			try{
				question.answerList.RemoveAt(selectedIndex - 1);
			} catch (Exception e){
				Debug.Log("Exception caught : " + e.Message);
			}
		}
		
		public void deleteAnswerPopup(ChoiceQuestion question){
			List<string> optionList = new List<string>();
			optionList.Add("aucune");
			foreach(Answer answer in question.answerList){
				optionList.Add(answer.text);
			}
			selectedIndex = EditorGUILayout.Popup(selectedIndex, optionList.ToArray());
		}


}
