using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;


/**
 * Popup permettant d'ajouter un ennoncé dans une énigme
 * Le gameObject est créé d'apres le prefab textElement
 * Il est placé dans le groupe Légende de la scène actuelle
 * @type {[type]}
 */
public class CreateTextPopup : PopupWindowContent
{
	string name = "EnoncéTest";
	string text = "Text de l'énoncé";

		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(300, 250);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter un schéma", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom du GameObject", name);
				text = EditorGUILayout.TextArea(text, GUILayout.Height(50));
				//bouton OK
				if (GUILayout.Button("OK", GUILayout.Width(200))) {
		      createObject();
		    }

    }

		//Crée un GameObject à partir du prefab textElement et le place dans le groupe Légende de la scène actuellement ouverte
		//Si plusieurs scènes d'énigmes sont ouvertes ils seront placé dans le premier groupe Schéma trouvé
		public  void createObject(){
			//création du gameObject
			GameObject objectGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/textElement.prefab", typeof(GameObject)));

			//remplissage du game Object
			objectGO.name = name;
			objectGO.GetComponent<Text>().text = text;

			//Positionnement dans la hierarchie de la scène
			GameObject parent = GameObject.Find("Légendes");
			if(parent){
				objectGO.transform.SetParent(parent.transform, false);
			}
		}

}
