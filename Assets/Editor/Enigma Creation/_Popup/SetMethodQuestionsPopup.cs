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
	GameObject popupGO = null;
	List<Editor> questionEditorList = new List<Editor>();
	Vector2 scrollPos;
	int selectedIndex = 0;
	//ChoiceQuestion test;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(500, 350);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
			EditorGUIUtility.labelWidth = 300;
			//ActiveEditorTracker.sharedTracker.isLocked = false;
      GUILayout.Label("Configurer les questions de méthode.", EditorStyles.boldLabel);
			if (GUILayout.Button("Ajouter une question", GUILayout.Width(200))) {
				CreateQuestion();
			}
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(500), GUILayout.Height(300));
			for(int i = 0; i < questionEditorList.Count; i++){
				EditorGUILayout.BeginVertical();
				questionEditorList[i].OnInspectorGUI();
				if (GUILayout.Button("Supprimer", GUILayout.Width(100))) {
					DeleteTargetQuestion(questionEditorList[i]);
					i--;
				}
				EditorGUILayout.EndVertical();
			}


			if(popupGO == null){
				GUILayout.Label("Le popUp n'est pas activé.\nVous ne pouvez pas voir les questions de méthode", EditorStyles.boldLabel);
			}



			EditorGUILayout.EndScrollView();
    }

		public override void OnOpen(){
			try {
				popupGO = GameObject.Find("Responsive Canvas").GetComponentInChildren<PopupManager>(true).gameObject;
				foreach(ChoiceQuestion question in popupGO.GetComponentsInChildren<ChoiceQuestion>()){
					questionEditorList.Add(Editor.CreateEditor(question, typeof(ChoiceQuestionEditor)));
					EditorUtility.SetDirty(question);
				}
			} catch (Exception e) {

			}
			//test = popUpGroup.GetComponentsInChildren<ChoiceQuestion>()[0];
		}

		public override void OnClose(){
			foreach(ChoiceQuestionEditor questionEditor in questionEditorList){
				Editor.DestroyImmediate(questionEditor);
			}
		}

		public void DeleteTargetQuestion(Editor questionEditor){
			ChoiceQuestion question = (ChoiceQuestion)questionEditor.target;
			questionEditorList.Remove(questionEditor);
			Editor.DestroyImmediate(questionEditor);
			GameObject.DestroyImmediate(question.gameObject);
		}

		public void CreateQuestion(){
			Transform parent = popupGO.transform.Find("Méthode");
			if(parent == null){
				Debug.Log("Pas de Méthode popup trouvé");
				return;
			}
			GameObject questionGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enigmas/Popup/_Elements/MethodQuestion.prefab", typeof(GameObject)));
			questionGO.transform.SetParent(parent, false);
			questionEditorList.Add(Editor.CreateEditor(questionGO.GetComponent<ChoiceQuestion>(), typeof(ChoiceQuestionEditor)));
		}

/*
		public void displayQuestionEditor(ChoiceQuestion question){

			GUILayout.BeginVertical("box");
			Editor editor = Editor.CreateEditor((UnityEngine.Object)question);
			editor.DrawDefaultInspector();

			GUILayout.Label("Ennoncé de la question", EditorStyles.boldLabel);
			question.text = EditorGUILayout.TextArea(question.text,  GUILayout.Height(50));
			question.professionalSituationId = EditorGUILayout.IntField("Identifiant de la situation professionnelle", question.professionalSituationId);
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

			GUILayout.EndVertical();
		}
*/
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
