using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;


/**
 * Popup permettant d'éditer une partie d'énoncé dans une énigme
 * La partie éditée est celle qui a été selectionnées par l'utilisateur
 * @type {[type]}
 */
public class EditScenarioPartPopup : PopupWindowContent
{
	Editor editor = null;
	Vector2 scrollPos;
	bool enigmaTypeError = false;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(500, 400);
    }

		//crée un éditeur en fonction du l'object selectionné.
		public override void OnOpen(){
			if(GameObject.Find("Managers").GetComponentInChildren<CaseValidation>() == null){
				enigmaTypeError = true;
			} else {
				enigmaTypeError = false;
			}
			GameObject selectedGO = Selection.activeGameObject;
			if(selectedGO == null || selectedGO.GetComponent<CaseScenarioPart>() == null){
				editor = null;
				return;
			}
			ChoiceQuestion question = selectedGO.GetComponent<ChoiceQuestion>();
			if(question){
				editor = Editor.CreateEditor(question);
				return;
			}
			Image image = selectedGO.GetComponent<Image>();
			if(image){
				editor = Editor.CreateEditor(image);
				return;
			}
			Text text = selectedGO.GetComponent<Text>();
			if(text){
				editor = Editor.CreateEditor(text);
				return;
			}
		}

		//détruit l'éditeur pour éviter les doublons
		public override void OnClose(){
			Editor.DestroyImmediate(editor);
		}

		//affiche l'éditeur
    public override void OnGUI(Rect rect){
			if(enigmaTypeError){
				GUILayout.Label("Attention, vous n'avez pas ouvert une énigme de type Study Case.", EditorStyles.boldLabel);
				return;
			}
			if(editor == null){
				GUILayout.Label("Vous n'avez pas selectionné de partie de scénarion à modifier.\nVeuillez selectionner une partie.", EditorStyles.boldLabel);
				return;
			}
			GUILayout.Label("Modifier une partie de scénario : " + Selection.activeGameObject.name , EditorStyles.boldLabel);
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(500), GUILayout.Height(300));
			editor.OnInspectorGUI();
			EditorGUILayout.EndScrollView();
    }
}
