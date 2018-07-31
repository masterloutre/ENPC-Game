using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*
[CustomEditor(typeof(InteractiveValue))]
public class InteractiveValueEditor : Editor {

	SerializedProperty valeur;
	SerializedProperty unité; //pour la lisibilité dans l'éditeur
	SerializedProperty nomDeVariable;
	SerializedProperty légende;
	SerializedProperty valeurÀLaLigne;
    
    void OnEnable()
    {
        valeur = serializedObject.FindProperty("valeur");
				unité = serializedObject.FindProperty("unité");
				nomDeVariable = serializedObject.FindProperty("nomDeVariable");
				légende = serializedObject.FindProperty("légende");
				valeurÀLaLigne = serializedObject.FindProperty("valeurÀLaLigne");
    }
	
		public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(valeur);
				EditorGUILayout.PropertyField(unité);
				EditorGUILayout.PropertyField(nomDeVariable);
				EditorGUILayout.PropertyField(légende);
				EditorGUILayout.PropertyField(valeurÀLaLigne);
        serializedObject.ApplyModifiedProperties();
				EditorGUILayout.LabelField("This is a personalized editor");
    }
}
*/