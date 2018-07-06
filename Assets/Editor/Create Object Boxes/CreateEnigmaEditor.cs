using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateEnigmaEditor : EditorWindow
{
	EnigmaType type;
  
  [MenuItem("Creation Enigme/Type Input")]
  static void Init()
  {
    EditorWindow window = EditorWindow.CreateInstance<CreateEnigmaEditor>();
    window.Show();
  }
	
	void Awake(){
		type = EnigmaType.INPUT;
	}
	
	private Rect placeAtCenter(Vector2 popupContentSize){
		//centre de la fenetre unity en coordonnées d'écran
		Rect editorCenter = Extensions.GetEditorMainWindowPos();
		editorCenter.Set(editorCenter.width/2, editorCenter.height/2, 0, 0);	
		
		//position de la fenetre d'éditeur en coordonnées d'écran
		Rect windowPos = this.position;
		Debug.Log("window pos" + windowPos.ToString());
		
		//centre de la fenetre unity en coordonnées relative à la fenetre d'éditeur
		Rect relativeCenter = new Rect(editorCenter.x - windowPos.x, editorCenter.y - windowPos.y, popupContentSize.x, popupContentSize.y);
		//décalage pour que le milieu du popup soit au milieu de l'écran
		relativeCenter.Set(relativeCenter.x - relativeCenter.width/2, relativeCenter.y - relativeCenter.height/2, 0, 0);
		
		return relativeCenter;
	}
	
	void showPopupContentAtCenter(PopupWindowContent popupContent){
		Rect position = placeAtCenter(popupContent.GetWindowSize());
		PopupWindow.Show(position, popupContent);
	}
	
  void OnGUI()
  {
		
    {
      GUILayout.Label("Creation d'énigme de type input", EditorStyles.boldLabel);
			if (GUILayout.Button("Créer une énigme depuis un fichier", GUILayout.Width(200)))
      {
					showPopupContentAtCenter(new CreateEnigmaFromFilePopup(type));
      }
      if (GUILayout.Button("Ajouter une légende", GUILayout.Width(200)))
      {
          showPopupContentAtCenter(new CreateLegendPopup());
      }
			if (GUILayout.Button("Ceci est un autre boutons", GUILayout.Width(200)))
      {
				showPopupContentAtCenter(new CreateLegendPopup());
      }
    }
  }
}
