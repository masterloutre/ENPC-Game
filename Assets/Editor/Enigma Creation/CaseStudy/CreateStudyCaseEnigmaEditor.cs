using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateStudyCaseEnigmaEditor : CreateEnigmaEditor
{

  [MenuItem("Creation Enigme/Type Cas d'étude")]
  static void Init()
  {
    EditorWindow window = EditorWindow.CreateInstance<CreateStudyCaseEnigmaEditor>();
    window.Show();
  }

  void OnGUI()
  {
    //Debug.Log(EditorWindow.focusedWindow.GetType());
    GUILayout.Label("Creation d'énigme de type Cas d'étude)", EditorStyles.boldLabel);

    if (GUILayout.Button("Créer une énigme Cas d'étude", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateEnigmaPopup(EnigmaType.STUDY_CASE));
    }
    if (GUILayout.Button("Ajouter une légende", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateLegendPopup());
    }
    if (GUILayout.Button("Ajouter un schéma", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateDiagramPopup());
    }
    if (GUILayout.Button("Ajouter un énoncé", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateTextPopup());
    }
    if (GUILayout.Button("Ajouter un partie de scénario", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateScenarioPartPopup());
    }
    if (GUILayout.Button("Modifier un partie de scénario", GUILayout.Width(250))) {
        showPopupContentAtCenter(new EditScenarioPartPopup());
    }
    if (GUILayout.Button("Modifier les questions de méthode", GUILayout.Width(250))) {
        showPopupContentAtCenter(new SetMethodQuestionsPopup());
    }

    generalSettingsGUI();
  }
}
