using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;


/**
 * Popup permettant d'ajouter un icon avec champs que l'utilisateur doit remplir dans une énigme
 * Le gameObject est créé d'apres le prefab UserInput
 * Il est placé dans le groupe Iteractive Elements de la scène actuelle
 * @type {[type]}
 */
public class CreateUserInputPopup : CreateElementPopup
{
	Texture2D iconAsset;

		//initialisation
		public override void OnOpen() {
			name = "UserInput";
			parentName = "Interactive Elements";
			if(GameObject.Find("Managers").GetComponentInChildren<InputValidation>() == null){
				createGO = null;
				errorMssg = "Attention, vous n'avez pas ouvert une énigme de type INPUT";
			} else {
				createGO = createUserInputObject;
			}
		}

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter une entrée utilisateur", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom du GameObject", name);
				iconAsset = (Texture2D)EditorGUILayout.ObjectField("selectionez une image d'icon", iconAsset, typeof(Texture2D), true);
				//bouton OK
				displayCreateButton();
				if(iconAsset == null){
					GUILayout.Label("En phase de test vous n'avez pas besoin de choisir une image d'icon", EditorStyles.boldLabel);
				}
    }

		//Crée un GameObject à partir du prefab UserInput et le place dans le groupe Iteractive Elements de la scène actuellement ouverte
		public GameObject createUserInputObject(){
			//création du gameObject
			GameObject inputGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/Input/UserInput.prefab", typeof(GameObject)));
			//remplissage du game Object
			inputGO.name = name;
			//add icon here
			if(iconAsset != null){
				Sprite sp = Sprite.Create(iconAsset,new Rect(0,0,iconAsset.width,iconAsset.height),new Vector2(0.5f,0.5f));
				inputGO.GetComponent<Image>().sprite = sp;
			}
			return inputGO;
		}

}
