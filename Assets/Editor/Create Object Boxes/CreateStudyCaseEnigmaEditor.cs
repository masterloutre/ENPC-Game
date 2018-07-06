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
    GUILayout.Label("Creation d'énigme de type cas d'étude", EditorStyles.boldLabel);
		
		if (GUILayout.Button("Créer une énigme depuis un fichier", GUILayout.Width(200))) {
				showPopupContentAtCenter(new CreateEnigmaFromFilePopup(EnigmaType.STUDY_CASE));
    }
    if (GUILayout.Button("Ajouter une légende", GUILayout.Width(200))) {
        showPopupContentAtCenter(new CreateLegendPopup());
    }
  }
}
