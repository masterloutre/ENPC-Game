using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Linq;


/**
 * Pop up permettant de créer une énigme.
 * La scène de l'énigme est créée avec l'id entré par l'utilisateur (attention à ne pas écraser une autre énigme) dans le dossier Enigmes
 * Elle est directment ouverte dans l'éditeur et ajoutée à la liste des builds
 * @type {[type]}
 */
public class CreateEnigmaPopup : PopupWindowContent
{    
    //type d'énigme
    EnigmaType type;
    //index unity de l'énigme (doit correspondre à l'index entré dans la bdd)
    int id = 0;
    
    
    public CreateEnigmaPopup(EnigmaType _type){
      type = _type;
    }
  
    //taille de la fenetre de pop up
    public override Vector2 GetWindowSize()  {
        return new Vector2(500, 400);
    }

    //affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Enigme : " + type, EditorStyles.boldLabel);
        id = EditorGUILayout.IntField("ID de l'énigme\n(doit correspondre à l'index unity entré en base de donnée)", id);
        GUILayout.Label("Attention si vous entrez un ID déjà utilisé vous allez remplacer l'énigme lui étant associé par un template vierge!\n Pensez à vérifier l'ID avant de valider.", EditorStyles.boldLabel);
        if (GUILayout.Button("Créer l'énigme", GUILayout.Width(200))) {
		      createEnigmaScene();
		    }
      
    }
    
    //création de la scène d'énigme
    public void createEnigmaScene(){
      //chemin du template (en fct du type d'énigme)
      string path = "Assets/Scenes/Templates Enigmes/Enigme "+type+".unity";
      //chemin de l'énigme crée (en fct de l'ID renseigné)
      string newPath = "Assets/Scenes/Enigmes/Enigma" + id+ ".unity";
      //copy du template au nouveau chemin
      AssetDatabase.CopyAsset(path, newPath);  
      //Ajout de la nouvelle scène au build settings
      List<EditorBuildSettingsScene> buildList = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
      buildList.Add(new EditorBuildSettingsScene(newPath, true));
      EditorBuildSettings.scenes = buildList.ToArray();
      //Ouverture de la nouvelle scène dans l'éditeur
      Scene newEnigmaScene = EditorSceneManager.OpenScene(newPath,  OpenSceneMode.Single);
    }
}
