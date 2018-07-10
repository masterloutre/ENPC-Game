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
public class CreateDiagramPopup : PopupWindowContent
{
	Texture2D textureAsset;
	string name = "DiagramTest";
	
		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(300, 250);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter un schéma", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom du GameObject", name);
				textureAsset = (Texture2D)EditorGUILayout.ObjectField("selectionez une image", textureAsset, typeof(Texture2D), true);
				//bouton OK
				if (GUILayout.Button("OK", GUILayout.Width(200))) {
		      createDiagramObject();
		    }
        
    }
		
		//Crée un GameObject à partir du prefab diagram et le place dans le groupe Schémas de la scène actuellement ouverte
		//Si plusieurs scènes d'énigmes sont ouvertes ils seront placé dans le premier groupe Légende trouvé
		public  void createDiagramObject(){
			//crée un sprite d'apres l'image selectionnée par l'utilisateur
			Sprite sp = Sprite.Create(textureAsset,new Rect(0,0,textureAsset.width,textureAsset.height),new Vector2(0.5f,0.5f));
			
			//création du gameObject
			GameObject diagramGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/diagram.prefab", typeof(GameObject)));
			
			//remplissage du game Object
			diagramGO.name = name;
			Image diagramImg = diagramGO.GetComponent<Image>();
			diagramImg.sprite = sp;
			
			//Positionnement dans la hierarchie de la scène
			GameObject parent = GameObject.Find("Schémas");
			if(parent){
				diagramGO.transform.SetParent(parent.transform, false);
			}	
		}
		
}
