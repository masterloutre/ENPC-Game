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
public class SetFeedBackPopup : PopupWindowContent
{
	Text success;
	Text failure;
	string successFeedback;
	string failureFeedback;

	GameObject popupGO = null;
	//ChoiceQuestion test;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(500, 350);
    }


		public override void OnOpen(){
			try {
				popupGO = GameObject.Find("Responsive Canvas").GetComponentInChildren<PopupManager>(true).gameObject;
				success= popupGO.transform.Find("Victoire").Find("Feedback").gameObject.GetComponent<Text>();
				failure= popupGO.transform.Find("Défaite").Find("Feedback").gameObject.GetComponent<Text>();
				successFeedback = success.text;
				failureFeedback = failure.text;

			} catch (Exception e) {
			}
		}

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
			EditorGUIUtility.labelWidth = 300;

			if(popupGO == null || success == null || failure == null){
				GUILayout.Label("Le popUp n'est pas présent.\nVous ne pouvez pas voir les feedback", EditorStyles.boldLabel);
				return;
			}
			EditorUtility.SetDirty(success);
			EditorUtility.SetDirty(failure);

      GUILayout.Label("Feedback de réussite.", EditorStyles.boldLabel);
			success.text = EditorGUILayout.TextArea(success.text,  GUILayout.Height(50));

			GUILayout.Label("Feedback de défaite.", EditorStyles.boldLabel);
			failure.text = EditorGUILayout.TextArea(failure.text,  GUILayout.Height(50));


    }

}
