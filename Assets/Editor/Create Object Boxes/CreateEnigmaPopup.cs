using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Linq;

public class CreateEnigmaPopup : PopupWindowContent
{    
    EnigmaType type;
    int id = 7;
    public CreateEnigmaPopup(EnigmaType _type){
      type = _type;
      
    }
  
  
    public override Vector2 GetWindowSize()
    {
        return new Vector2(500, 400);
    }

    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Enigme : " + type, EditorStyles.boldLabel);
        id = EditorGUILayout.IntField("ID de l'énigme\n(doit correspondre à l'index unity entré en base de donnée)", id);
        GUILayout.Label("Cette fonctionnalité n'est pas encore disponible", EditorStyles.boldLabel);
        if (GUILayout.Button("Créer l'énigme", GUILayout.Width(200))) {
		      createEnigmaScene();
		    }
      
    }

    public override void OnOpen()
    {
        Debug.Log("Popup opened: " + this);
    }

    public override void OnClose()
    {
        Debug.Log("Popup closed: " + this);
    }
    
    public void createEnigmaScene(){
      string path = "Assets/Scenes/Templates Enigmes/Enigme "+type+".unity";
      string newPath = "Assets/Scenes/Enigmes/Enigma" + id+ ".unity";
      AssetDatabase.CopyAsset(path, newPath);  
      List<EditorBuildSettingsScene> buildList = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
      buildList.Add(new EditorBuildSettingsScene(newPath, true));
      EditorBuildSettings.scenes = buildList.ToArray();
      Scene newEnigmaScene = EditorSceneManager.OpenScene(newPath,  OpenSceneMode.Single);
    }
}
