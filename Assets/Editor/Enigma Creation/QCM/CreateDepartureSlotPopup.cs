﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


/**
 * Popup permettant d'ajouter un slot de qcm de départ
 * Le gameObject est créé d'apres le prefab Departure Slot, ce prefab possède un composant Item nécessaire à la validation de l'énigme
 * Il est placé dans le groupe Departure Slots de la scène actuelle
 * @type {[type]}
 */
public class CreateDepartureSlotPopup : CreateElementPopup
{
	int id = 0;
	float value = 0;
	string unit = "cm";
	string text = "";
	bool jump = false;
	bool legend = true;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(250, 250);
    }

		//initialisation
		public override void OnOpen() {
			name = "DepartureSlot";
			parentName = "Departure Slots";
			if(GameObject.Find("Managers").GetComponentInChildren<QCMValidation>() == null){
				createGO = null;
				errorMssg = "Attention, vous n'avez pas ouvert une énigme de type QCM";
			} else {
				createGO = createObject;
			}
		}



		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter un slot de départ", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom de l'object", name);
				id =  EditorGUILayout.IntField("Identifiant", id);
				legend = EditorGUILayout.Toggle("Ajouter une valeur", legend);
				if(legend){
					value = EditorGUILayout.FloatField("Valeur", value);
					unit = EditorGUILayout.TextField("Unité", unit);
					text = EditorGUILayout.TextField("Texte de légende", text);
					jump = EditorGUILayout.Toggle("Aller à la ligne", jump);
				}
				//bouton OK
				displayCreateButton();

    }

		//Créer l'objet et le place dans le groupe Departure Slots
		public GameObject createObject(){
			//création du gameObject
			GameObject slotGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/qcm/Departure Slot.prefab", typeof(GameObject)));
			slotGO.name = name;

			//remplissage du component InteractiveValue
			InteractiveValue iv = slotGO.GetComponentInChildren<InteractiveValue>();
			if(legend){
				iv.valeur = value;
				iv.unité = unit;
				iv.légende = text;
				iv.nomDeVariable = ' ';
				iv.valeurÀLaLigne = jump;
			} else {
				GameObject.DestroyImmediate(iv.gameObject);
			}


			//remplissage du component item
			Item item = slotGO.GetComponentInChildren<Item>();
			item.item_id = id;

			return slotGO;
		}

}
