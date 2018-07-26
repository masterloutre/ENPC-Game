using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.SceneManagement;

public abstract class CreateEnigmaEditor : EditorWindow
{
  bool hasAPopup = true;
  EnigmaSceneManager sceneManager;
  PopupManager popupManager;
  Editor popupEditor = null;
  bool showPopup = true;

  static void Init()
  {
    EditorWindow window = EditorWindow.CreateInstance<CreateEnigmaEditor>();
    window.Show();
  }

  public void Awake(){
    EditorSceneManager.sceneOpened  += onSceneOpened;
    sceneManager = GameObject.Find("Managers").GetComponentInChildren<EnigmaSceneManager>();
  }

  public void OnDestroy(){
    EditorSceneManager.sceneOpened  -= onSceneOpened;
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
  }

  public void OnFocus(){
    sceneManager = GameObject.Find("Managers").GetComponentInChildren<EnigmaSceneManager>();
    createPopupEditor();
  }

  public virtual void generalSettingsGUI(){
    EditorGUIUtility.labelWidth = 200;
    EditorGUILayout.BeginVertical("box");
    EditorGUILayout.LabelField("Popup",  EditorStyles.boldLabel);
    if(popupEditor != null && popupEditor.target != null){
      popupEditor.OnInspectorGUI();
      GameObject popup = ((PopupManager)popupEditor.target).gameObject;
      showPopup = popup.activeInHierarchy;
      showPopup = EditorGUILayout.Toggle("Afficher Le Popup", showPopup);
      popup.SetActive(showPopup);

    } else {
      GUILayout.Label("Erreur : Il n'y a pas de component PopupManager dans la scène ou bien le jeu est en mode play", EditorStyles.boldLabel);
    }


    /*
    bool popupCurrentlyExists = (GameObject.Find("Answer Popup") != null)? true: false;
    if(sceneManager == null){
      GUILayout.Label("Erreur : Il n'y a pas de component EnigmaSceneManager dans la scène", EditorStyles.boldLabel);
    } else {
      hasAPopup = popupCurrentlyExists;
      hasAPopup = EditorGUILayout.Toggle("Activer Le Popup", hasAPopup);
      if((hasAPopup && !popupCurrentlyExists) || (!hasAPopup && popupCurrentlyExists)){
        sceneManager.activatePopup(hasAPopup);
      }
    }
    */

    EditorGUILayout.EndVertical();

  }

  private void onSceneOpened(Scene scene, OpenSceneMode mode){
    sceneManager = GameObject.Find("Managers").GetComponentInChildren<EnigmaSceneManager>();
    createPopupEditor();
  }

  private void createPopupEditor(){
    if(popupEditor != null){
      Editor.DestroyImmediate(popupEditor);
    }
    PopupManager popupManager = GameObject.Find("Responsive Canvas").GetComponentInChildren<PopupManager>(true);
    popupEditor = Editor.CreateEditor(popupManager);
  }
}
