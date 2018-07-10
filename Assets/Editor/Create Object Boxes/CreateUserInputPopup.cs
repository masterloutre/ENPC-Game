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
public class CreateUserInputPopup : PopupWindowContent
{
	Texture2D iconAsset;
	string name = "UserInput";

		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(300, 250);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter une entrée utilisateur", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom du GameObject", name);
				iconAsset = (Texture2D)EditorGUILayout.ObjectField("selectionez une image d'icon", iconAsset, typeof(Texture2D), true);
				//bouton OK
				if (GUILayout.Button("OK", GUILayout.Width(200))) {
		      createUserInputObject();
		    }

				if(iconAsset == null){
					GUILayout.Label("En phase de test vous n'avez pas besoin de choisir une image d'icon", EditorStyles.boldLabel);
				}


    }

		//Crée un GameObject à partir du prefab UserInput et le place dans le groupe Iteractive Elements de la scène actuellement ouverte
		public  void createUserInputObject(){
			//crée un sprite d'apres l'image selectionnée par l'utilisateur
			//Sprite sp = Sprite.Create(iconAsset,new Rect(0,0,iconAsset.width,iconAsset.height),new Vector2(0.5f,0.5f));

			//création du gameObject
			GameObject inputGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/Input/UserInput.prefab", typeof(GameObject)));

			//remplissage du game Object
			inputGO.name = name;
			//add icon here

			//Positionnement dans la hierarchie de la scène
			GameObject parent = GameObject.Find("Interactive Elements");
			if(parent){
				inputGO.transform.SetParent(parent.transform, false);
			}
		}

}
