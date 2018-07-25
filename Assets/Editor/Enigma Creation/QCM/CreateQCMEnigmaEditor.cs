using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateQCMEnigmaEditor : CreateEnigmaEditor
{

  [MenuItem("Creation Enigme/Type QCM")]
  static void Init()
  {
    EditorWindow window = EditorWindow.CreateInstance<CreateQCMEnigmaEditor>();
    window.Show();
  }

  void OnGUI()
  {
    GUILayout.Label("Creation d'énigme de type QCM", EditorStyles.boldLabel);

		if (GUILayout.Button("Créer une énigme QCM", GUILayout.Width(250))) {
				showPopupContentAtCenter(new CreateEnigmaPopup(EnigmaType.QCM));
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
    if (GUILayout.Button("Ajouter un slot de départ", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateDepartureSlotPopup());
    }
    if (GUILayout.Button("Ajouter un slot d'arrivée", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateDestinationSlotPopup());
    }
    if (GUILayout.Button("Modifier les questions de méthode", GUILayout.Width(250))) {
        showPopupContentAtCenter(new SetMethodQuestionsPopup());
    }

    generalSettingsGUI();
  }
}
