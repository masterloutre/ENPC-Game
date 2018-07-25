using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


/**
 * Popup permettant d'ajouter un slot de qcm d'arrivée
 * Le gameObject est créé d'apres le prefab Departure Slot, ce prefab possède un composant Item nécessaire à la validation de l'énigme
 * Il est placé dans le groupe Departure Slots de la scène actuelle
 * @type {[type]}
 */
public class CreateDestinationSlotPopup : PopupWindowContent
{
	string name = "DestinationSlot";
	int expectedId = 0;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(250, 250);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter un slot d'arrivée", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom de l'object", name);

				expectedId =  EditorGUILayout.IntField("Identifiant attendu", expectedId);

				//bouton OK
				if (GUILayout.Button("OK", GUILayout.Width(200))) {
		      createLegendGameObject();
		    }

    }

		//Créer l'objet et le place dans le groupe Departure Slots
		public void createLegendGameObject(){
			//création du gameObject
			GameObject slotGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/qcm/Destination Slot.prefab", typeof(GameObject)));
			slotGO.name = name;

			//remplissage du component slot
			ItemSlot itemSlot = slotGO.GetComponentInChildren<ItemSlot>();
			itemSlot.expected_id = expectedId;

			//Positionnement dans la hierarchie de la scène
			GameObject parent = GameObject.Find("Destination Slots");
			if(parent){
				slotGO.transform.SetParent(parent.transform, false);
			}
		}

}
