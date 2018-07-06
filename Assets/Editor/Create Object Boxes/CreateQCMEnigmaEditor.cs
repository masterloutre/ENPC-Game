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
		
		if (GUILayout.Button("Créer une énigme depuis un fichier", GUILayout.Width(200))) {
				showPopupContentAtCenter(new CreateEnigmaFromFilePopup(EnigmaType.QCM));
    }
    if (GUILayout.Button("Ajouter une légende", GUILayout.Width(200))) {
        showPopupContentAtCenter(new CreateLegendPopup());
    }
  }
}
