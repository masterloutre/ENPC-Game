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
		
		if (GUILayout.Button("Créer une énigme", GUILayout.Width(200))) {
				showPopupContentAtCenter(new CreateEnigmaPopup(EnigmaType.INPUT));
    }
    if (GUILayout.Button("Ajouter une légende", GUILayout.Width(200))) {
        showPopupContentAtCenter(new CreateLegendPopup());
    }
    if (GUILayout.Button("Ajouter un schéma", GUILayout.Width(200))) {
        showPopupContentAtCenter(new CreateDiagramPopup());
        
    }
  }
}
