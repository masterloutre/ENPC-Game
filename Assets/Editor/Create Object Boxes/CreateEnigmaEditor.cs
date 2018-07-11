using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class CreateEnigmaEditor : EditorWindow
{

  static void Init()
  {
    EditorWindow window = EditorWindow.CreateInstance<CreateEnigmaEditor>();
    window.Show();
  }

	Rect placeAtCenter(Vector2 popupContentSize){
		//centre de la fenetre unity en coordonnées d'écran
		Rect editorCenter = Extensions.GetEditorMainWindowPos();
		editorCenter.Set(editorCenter.width/2, editorCenter.height/2, 0, 0);

		//position de la fenetre d'éditeur en coordonnées d'écran
		Rect windowPos = this.position;
		//centre de la fenetre unity en coordonnées relative à la fenetre d'éditeur
		Rect relativeCenter = new Rect(editorCenter.x - windowPos.x, editorCenter.y - windowPos.y, popupContentSize.x, popupContentSize.y);
		//décalage pour que le milieu du popup soit au milieu de l'écran
		relativeCenter.Set(relativeCenter.x - relativeCenter.width/2, relativeCenter.y - relativeCenter.height/2, 0, 0);

		return relativeCenter;
	}

	public virtual void showPopupContentAtCenter(PopupWindowContent popupContent){
		Rect position = placeAtCenter(popupContent.GetWindowSize());
		PopupWindow.Show(position, popupContent);
	}

  public virtual void showPopupContentAtCenter(EditorWindow popupWindow){
    
    popupWindow.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
    popupWindow.ShowPopup();
    }

  public virtual void OnGUI()
  {

    {
      GUILayout.Label("Creation d'énigme", EditorStyles.boldLabel);

    }
  }
}
