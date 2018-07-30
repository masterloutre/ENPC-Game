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
public class CreateDestinationSlotPopup : CreateElementPopup
{
	int expectedId = 0;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(250, 250);
    }

		//initialisation
		public override void OnOpen() {
			name = "DestinationSlot";
			parentName = "Destination Slots";
			if(GameObject.Find("Managers").GetComponentInChildren<QCMValidation>() == null){
				createGO = null;
				errorMssg = "Attention, vous n'avez pas ouvert une énigme de type QCM";
			} else {
				createGO = createObject;
			}
		}

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter un slot d'arrivée", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom de l'object", name);

				expectedId =  EditorGUILayout.IntField("Identifiant attendu", expectedId);

				//bouton OK
				displayCreateButton();

    }

		//Créer l'objet et le place dans le groupe Departure Slots
		public GameObject createObject(){
			//création du gameObject
			GameObject slotGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/qcm/Destination Slot.prefab", typeof(GameObject)));
			slotGO.name = name;

			//remplissage du component slot
			ItemSlot itemSlot = slotGO.GetComponentInChildren<ItemSlot>();
			itemSlot.expected_id = expectedId;

			return slotGO;
		}

}
