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
public class CreateLegendPopup : PopupWindowContent
{
	string name = "LegendLongeur";
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
				if (GUILayout.Button("OK", GUILayout.Width(200))) {
		      createLegendGameObject();
		    }
        
    }
		
		//Crée un GameObject à partir du prefab InteractiveValue et le place dans le groupe Légendes de la scène actuellement ouverte
		//Si plusieurs scènes d'énigmes sont ouvertes ils seront placé dans le premier groupe Légende trouvé
		public void createLegendGameObject(){
			//création du gameObject
			GameObject legendGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/interactive value.prefab", typeof(GameObject)));
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
			
			//Positionnement dans la hierarchie de la scène
			GameObject parent = GameObject.Find("Légendes");
			if(parent){
				legendGO.transform.SetParent(parent.transform, false);
			}	
		}
		
}
