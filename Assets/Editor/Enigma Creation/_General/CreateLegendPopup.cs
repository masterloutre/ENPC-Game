using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


/**
 * Popup permettant d'ajouter une légende ou une donnée dans une énigme
 * Le gameObject est créé d'apres le prefab InteractiveValue et peut ou non être déterminant pour le calcul du résultat (Input Param)
 * Il est placé dans le groupe Légende de la scène actuelle
 * @type {[type]}
 */
public class CreateLegendPopup : CreateElementPopup
{
	float value = 0;
	string unit = "cm";
	string legend = "longueur";
	string variableName = "";
	bool jump = false;
	bool inputValue = false;

		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(250, 250);
    }

		//initialisation
		public override void OnOpen() {
			name = "LegendLongeur";
			createGO = createLegendGameObject;
			parentName = "Légendes";
		}

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter une légende", EditorStyles.boldLabel);
				//description de la légende
				name = EditorGUILayout.TextField("Nom de l'object", name);
				value = EditorGUILayout.FloatField("Valeur", value);
				unit = EditorGUILayout.TextField("Unité", unit);
				legend = EditorGUILayout.TextField("Texte de légende", legend);
				jump = EditorGUILayout.Toggle("Aller à la ligne", jump);
				//input value ou pas
				inputValue = EditorGUILayout.Toggle("Sert au calcul du résultat", inputValue);
				if(inputValue){
					variableName =  EditorGUILayout.TextField("Nom de la variable", "x");
				} else {
					variableName = "";
				}
				//bouton OK
				displayCreateButton();
    }

		//Crée un GameObject à partir du prefab InteractiveValue et le place dans le groupe Légendes de la scène actuellement ouverte
		public GameObject createLegendGameObject(){
			//création du gameObject
			GameObject legendGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(pathToPrefabs + "Enigmas/General/InteractiveValue.prefab", typeof(GameObject)));
			legendGO.name = name;

			//remplissage du component InteractiveValue
			InteractiveValue iv = legendGO.GetComponent<InteractiveValue>();
			iv.valeur = value;
			iv.unité = unit;
			iv.légende = legend;
			iv.nomDeVariable = (variableName == "")? ' ':variableName[0];
			iv.valeurÀLaLigne = jump;

			//Ajout du tag (nécessaire pour la validation de l'énigme INPUT)
			if(inputValue){
				legendGO.tag = "Input Param";
			} else {
				legendGO.tag = "Untagged";
			}
			return legendGO;
		}

}
