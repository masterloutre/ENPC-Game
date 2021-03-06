﻿using System.Collections;
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
public class SetInputValidationPopup : PopupWindowContent
{
	List<InteractiveValue> paramList = new List<InteractiveValue>();
	InputValidation validator;
	bool enigmaTypeError = false;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(500, 250);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
			if(enigmaTypeError){
				GUILayout.Label("Attention, vous n'avez pas ouvert une énigme de type Input.", EditorStyles.boldLabel);
				return;
			}
				EditorGUIUtility.labelWidth = 300;
        GUILayout.Label("Configurer le calcul du résultat.", EditorStyles.boldLabel);
				GUILayout.Label("Variables actuellement disponibles dans la scène : " + paramList.Count);
				foreach(InteractiveValue param in paramList){
					GUILayout.Label(param.variableName + " = " + param.value + param.unit);
				}
				GUILayout.Label("ATTENTION si vous avez un doublon cela peut fausser le calcul du résultat!!!", EditorStyles.boldLabel);
				validator.formula = EditorGUILayout.TextField("Formule de calcul du resultat", validator.formula);
				GUILayout.Label("Vous pouvez également rentrer directement le resultat", EditorStyles.boldLabel);
				validator.marginError = EditorGUILayout.DoubleField("Marge d'erreur", validator.marginError);
				//bouton OK
				/*
				if (GUILayout.Button("OK", GUILayout.Width(200))) {
		      setValidator();
		    }
				*/
    }

		public override void OnOpen(){
			//validator = new InputValidation(GameObject.Find("Managers").GetComponentInChildren<InputValidation>());
			validator = GameObject.Find("Managers").GetComponentInChildren<InputValidation>();
			GameObject[] paramGOList = GameObject.FindGameObjectsWithTag("Input Param");
			foreach(GameObject go in paramGOList){
				paramList.Add(go.GetComponent<InteractiveValue>());
			}
			if(GameObject.Find("Managers").GetComponentInChildren<InputValidation>() == null){
				enigmaTypeError = true;
			} else {
				enigmaTypeError = false;
			}
		}

		public void setValidator(){
			InputValidation val = GameObject.Find("Managers").GetComponentInChildren<InputValidation>();
			val = validator;
		}


}
