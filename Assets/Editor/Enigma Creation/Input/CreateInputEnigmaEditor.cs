using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateInputEnigmaEditor : CreateEnigmaEditor
{

  [MenuItem("Creation Enigme/Type Input")]
  static void Init()
  {
    EditorWindow window = EditorWindow.CreateInstance<CreateInputEnigmaEditor>();
    window.Show();
  }

  void OnGUI()
  {
    GUILayout.Label("Creation d'énigme de type input", EditorStyles.boldLabel);

		if (GUILayout.Button("Créer une énigme INPUT", GUILayout.Width(250))) {
				showPopupContentAtCenter(new CreateEnigmaPopup(EnigmaType.INPUT));
    }
    scrollPos = EditorGUILayout.BeginScrollView(scrollPos,  GUILayout.Width(this.position.width), GUILayout.Height(this.position.height - 40));
    //éléments généraux
    base.OnGUI();
    //éléments particuliers
    EditorGUILayout.BeginVertical("box");
    GUILayout.Label("Type Input");
    if (GUILayout.Button("Ajouter un champ d'entrée de valeur", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateUserInputPopup());
    }
    if (GUILayout.Button("Modifier le calcul du résultat", GUILayout.Width(250))) {
        showPopupContentAtCenter(new SetInputValidationPopup());
    }
    EditorGUILayout.EndVertical();
    EditorGUILayout.EndScrollView();
  }
}
