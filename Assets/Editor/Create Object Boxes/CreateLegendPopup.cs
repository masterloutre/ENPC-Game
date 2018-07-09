using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class CreateLegendPopup : PopupWindowContent
{
	string name = "LegendLongeur";
	float value = 0;
	string unit = "cm";
	string legend = "longueur";
	string variableName = "";
	bool jump = false;
	bool inputValue = false;
	

    public override Vector2 GetWindowSize()
    {
        return new Vector2(250, 250);
    }

    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Ajouter une légende", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom de l'object", name);
				value = EditorGUILayout.FloatField("Valeur", value);
				unit = EditorGUILayout.TextField("Unité", unit);
				legend = EditorGUILayout.TextField("Texte de légende", legend);
				jump = EditorGUILayout.Toggle("Aller à la ligne", jump);
				inputValue = EditorGUILayout.Toggle("Sert au calcul du résultat", inputValue);
				if(inputValue){
					variableName =  EditorGUILayout.TextField("Nom de la variable", "x");
				} else {
					variableName = "";
				}
				if (GUILayout.Button("OK", GUILayout.Width(200))) {
		      createLegendGameObject();
		    }
        
    }

    public override void OnOpen()
    {
        Debug.Log("Popup opened: " + this);
    }

    public override void OnClose()
    {
        Debug.Log("Popup closed: " + this);
    }
		
		public void createLegendGameObject(){
			GameObject legendGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/interactive value.prefab", typeof(GameObject)));
			legendGO.name = name;
			
			InteractiveValue iv = legendGO.GetComponent<InteractiveValue>();
			iv.valeur = value;
			iv.unité = unit;
			iv.légende = legend;
			iv.nomDeVariable = (variableName == "")? ' ':variableName[0];
			iv.valeurÀLaLigne = jump;
			if(inputValue){
				legendGO.tag = "Input Param";
			}
			
			GameObject parent = GameObject.Find("Légendes");
			legendGO.transform.SetParent(parent.transform, false);
			Debug.Log("Legend created : " + legendGO.name + ", parent : " + parent.name);
		}
		
}
