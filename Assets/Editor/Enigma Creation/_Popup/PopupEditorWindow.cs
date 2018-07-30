using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.SceneManagement;

public class PopupEditorWindow : CreateEnigmaEditor
{
  bool hasAPopup = true;
  EnigmaSceneManager sceneManager;
  PopupManager popupManager;
  Editor popupEditor = null;
  bool showPopup = true;

  [MenuItem("Creation Enigme/Popup feedback et méthode")]
  static void Init()
  {
    EditorWindow window = EditorWindow.CreateInstance<PopupEditorWindow>();
    window.Show();
  }

  public void Awake(){
    EditorSceneManager.sceneOpened  += onSceneOpened;
    sceneManager = GameObject.Find("Managers").GetComponentInChildren<EnigmaSceneManager>();
  }

  public void OnDestroy(){
    EditorSceneManager.sceneOpened  -= onSceneOpened;
  }

  public void OnFocus(){
    sceneManager = GameObject.Find("Managers").GetComponentInChildren<EnigmaSceneManager>();
    createPopupEditor();
  }

  public void OnGUI(){
    EditorGUIUtility.labelWidth = 200;
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
    if (GUILayout.Button("Modifier les questions de méthode", GUILayout.Width(250))) {
        showPopupContentAtCenter(new SetMethodQuestionsPopup());
    }
    if (GUILayout.Button("Modifier les feedbacks", GUILayout.Width(250))) {
        showPopupContentAtCenter(new SetFeedBackPopup());
    }
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
