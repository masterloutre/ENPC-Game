using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;


/**
 * Popup permettant d'ajouter un schéma dans une énigme
 * Le gameObject est créé d'apres le prefab diagram
 * Il est placé dans le groupe Schémas de la scène actuelle
 * @type {[type]}
 */
public class CreateDiagramPopup : CreateElementPopup
{
	Texture2D textureAsset;

		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(300, 250);
    }

		//initialisation
		public override void OnOpen() {
			name = "DiagramTest";
			createGO = createDiagramObject;
			parentName = "Schémas";
		}

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
      GUILayout.Label("Ajouter un schéma", EditorStyles.boldLabel);
			name = EditorGUILayout.TextField("Nom du GameObject", name);
			textureAsset = (Texture2D)EditorGUILayout.ObjectField("selectionez une image", textureAsset, typeof(Texture2D), true);
			//bouton OK
			if(textureAsset != null){
				displayCreateButton();
			 } else {
				 errorMssg = "Vous devez choisir une image!";
				 displayErrorMssg();
			 }
    }

		//crée le gameObject
		public GameObject createDiagramObject(){
			//création du gameObject
			GameObject diagramGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(pathToPrefabs + "Enigmas/General/Diagram.prefab", typeof(GameObject)));
			//remplissage du game Object
			diagramGO.name = name;
			//crée un sprite d'apres l'image selectionnée par l'utilisateur
			Sprite sp = Sprite.Create(textureAsset,new Rect(0,0,textureAsset.width,textureAsset.height),new Vector2(0.5f,0.5f));
			diagramGO.GetComponent<Image>().sprite = sp;
			return diagramGO;
		}

}
