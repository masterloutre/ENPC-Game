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
public class EditSlotPopup : PopupWindowContent
{
	Editor editor = null;
	Vector2 scrollPos;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(500, 400);
    }

		//crée un éditeur en fonction du l'object selectionné.
		public override void OnOpen(){
			GameObject selectedGO = Selection.activeGameObject;
			if(selectedGO == null){
				editor = null;
				return;
			}
			ItemSlot slot = selectedGO.GetComponentInChildren<ItemSlot>(true);
			Item item = selectedGO.GetComponentInChildren<Item>(true);
			if(slot != null){
				editor = Editor.CreateEditor(slot);
			} else if (item != null){
				editor = Editor.CreateEditor(item);
			}
		}

		//détruit l'éditeur pour éviter les doublons
		public override void OnClose(){
			Editor.DestroyImmediate(editor);
		}

		//affiche l'éditeur
    public override void OnGUI(Rect rect){
			if(editor == null){
				GUILayout.Label("Vous n'avez pas selectionné de slot à modifier.\nVeuillez selectionner un slot (départ ou destination).", EditorStyles.boldLabel);
				return;
			}
			GUILayout.Label("Modifier un Slot : " + Selection.activeGameObject.name, EditorStyles.boldLabel);
			scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(500), GUILayout.Height(300));
			editor.OnInspectorGUI();
			EditorGUILayout.EndScrollView();
    }
}
