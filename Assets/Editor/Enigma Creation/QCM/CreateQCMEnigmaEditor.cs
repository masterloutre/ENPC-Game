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
    //création de la scène
    GUILayout.Label("Creation d'énigme de type QCM", EditorStyles.boldLabel);
		if (GUILayout.Button("Créer une énigme QCM", GUILayout.Width(250))) {
				showPopupContentAtCenter(new CreateEnigmaPopup(EnigmaType.QCM));
    }

    scrollPos = EditorGUILayout.BeginScrollView(scrollPos,  GUILayout.Width(this.position.width), GUILayout.Height(this.position.height - 40));
    //éléments généraux
    base.OnGUI();
    //éléments particuliers
    EditorGUILayout.BeginVertical("box");
    GUILayout.Label("Type QCM");
    if (GUILayout.Button("Ajouter un slot de départ", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateDepartureSlotPopup());
    }
    if (GUILayout.Button("Ajouter un slot d'arrivée", GUILayout.Width(250))) {
        showPopupContentAtCenter(new CreateDestinationSlotPopup());
    }
    if (GUILayout.Button("Modifier un slot", GUILayout.Width(250))) {
        showPopupContentAtCenter(new EditSlotPopup());
    }
    EditorGUILayout.EndVertical();
    EditorGUILayout.EndScrollView();
  }
}
