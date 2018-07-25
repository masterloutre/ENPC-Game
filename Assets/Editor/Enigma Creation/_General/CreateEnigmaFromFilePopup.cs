using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateEnigmaFromFilePopup : PopupWindowContent
{    
    EnigmaType type;
    public CreateEnigmaFromFilePopup(EnigmaType _type){
      type = _type;
      
    }
  
  
    public override Vector2 GetWindowSize()
    {
        return new Vector2(500, 400);
    }

    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Enigme : " + type, EditorStyles.boldLabel);
        GUILayout.Label("Cette fonctionnalité n'est pas encore disponible", EditorStyles.boldLabel);
      
    }

    public override void OnOpen()
    {
        Debug.Log("Popup opened: " + this);
    }

    public override void OnClose()
    {
        Debug.Log("Popup closed: " + this);
    }
}
