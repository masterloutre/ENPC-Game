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
    if (GUILayout.Button("Ajouter une légende", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateLegendPopup());
    }
    if (GUILayout.Button("Ajouter un schéma", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateDiagramPopup());
    }
    if (GUILayout.Button("Ajouter un champ d'entrée de valeur", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateUserInputPopup());
    }
    if (GUILayout.Button("Modifier le calcul du résultat", GUILayout.Width(250))) {
        showPopupContentAtCenter(new SetInputValidationPopup());
    }
    if (GUILayout.Button("Modifier les questions de méthode", GUILayout.Width(250))) {
        showPopupContentAtCenter(new SetMethodQuestionsPopup());
    }
    generalSettingsGUI();
  }
}
