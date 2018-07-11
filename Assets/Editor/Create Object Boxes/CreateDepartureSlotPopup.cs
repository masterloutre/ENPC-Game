using System.Collections;
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
public class CreateDepartureSlotPopup : PopupWindowContent
{
	string name = "DepartureSlot";
	int id = 0;
	float value = 0;
	string unit = "cm";
	string legend = "";
	bool jump = false;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(250, 250);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter un slot de départ", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom de l'object", name);

				id =  EditorGUILayout.IntField("Identifiant", id);

				value = EditorGUILayout.FloatField("Valeur", value);
				unit = EditorGUILayout.TextField("Unité", unit);
				legend = EditorGUILayout.TextField("Texte de légende", legend);
				jump = EditorGUILayout.Toggle("Aller à la ligne", jump);

				//bouton OK
				if (GUILayout.Button("OK", GUILayout.Width(200))) {
		      createLegendGameObject();
		    }

    }

		//Créer l'objet et le place dans le groupe Departure Slots
		public void createLegendGameObject(){
			//création du gameObject
			GameObject slotGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/qcm/Departure Slot.prefab", typeof(GameObject)));
			slotGO.name = name;

			//remplissage du component InteractiveValue
			InteractiveValue iv = slotGO.GetComponentInChildren<InteractiveValue>();
			iv.valeur = value;
			iv.unité = unit;
			iv.légende = legend;
			iv.nomDeVariable = ' ';
			iv.valeurÀLaLigne = jump;

			//remplissage du component item
			Item item = slotGO.GetComponentInChildren<Item>();
			item.item_id = id;

			//Positionnement dans la hierarchie de la scène
			GameObject parent = GameObject.Find("Departure Slots");
			if(parent){
				slotGO.transform.SetParent(parent.transform, false);
			}
		}

}
