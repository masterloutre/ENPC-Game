using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;


/**
 * Popup permettant d'ajouter une vidéo dans une énigme
 * Le gameObject est créé d'apres le prefab Video Block
 * Il est placé dans le groupe Vidéos de la scène actuelle
 * Il marche en tandem avec le Video Player Popup qui est déjà dans la scène
 */
public class CreateVideoPopup : CreateElementPopup
{
	string url = "http://millenaire4.enpc.fr/vidéos/name.mov";

		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(300, 250);
    }

		//initialisation
		public override void OnOpen() {
			name = "VideoBlock";
			createGO = createObject;
			parentName = "Vidéos";
		}

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter une vidéo", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom du GameObject", name);
				url = EditorGUILayout.TextField("url de l'image", url);
				//bouton OK
				displayCreateButton();
    }

		//Crée un GameObject à partir du prefab textElement et le place dans le groupe Légende de la scène actuellement ouverte
		//Si plusieurs scènes d'énigmes sont ouvertes ils seront placé dans le premier groupe Schéma trouvé
		public  GameObject createObject(){
			//création du gameObject
			GameObject objectGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(pathToPrefabs + "Enigmas/General/VideoBlock.prefab", typeof(GameObject)));

			//remplissage du game Object
			objectGO.name = name;
			objectGO.GetComponent<VideoBlock>().url = url;

			return objectGO;
		}

}
