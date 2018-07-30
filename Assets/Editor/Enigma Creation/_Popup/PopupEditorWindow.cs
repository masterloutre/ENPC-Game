using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class PopupEditorWindow : CreateEnigmaEditor
{
  Editor popupEditor = null;

  [MenuItem("Creation Enigme/Popup feedback et méthode")]
  static void Init()
  {
    EditorWindow window = EditorWindow.CreateInstance<PopupEditorWindow>();
    window.Show();
  }

  public void Awake(){
    createPopupEditor();
  }

  public void OnDestroy(){
    Editor.DestroyImmediate(popupEditor);
  }

  void OnHierarchyChange()  {
    createPopupEditor();
  }


  public void OnGUI(){
    EditorGUIUtility.labelWidth = 200;
    EditorGUILayout.LabelField("Popup",  EditorStyles.boldLabel);
    if(popupEditor != null){
      popupEditor.OnInspectorGUI();
    } else {
      GUILayout.Label("Erreur : Il n'y a pas de component PopupManager dans la scène", EditorStyles.boldLabel);
    }
    if (GUILayout.Button("Modifier les questions de méthode", GUILayout.Width(250))) {
        showPopupContentAtCenter(new SetMethodQuestionsPopup());
    }
    if (GUILayout.Button("Modifier les feedbacks", GUILayout.Width(250))) {
        showPopupContentAtCenter(new SetFeedBackPopup());
    }
  }


  private void createPopupEditor(){
    if(popupEditor != null){
      Editor.DestroyImmediate(popupEditor);
    }
    PopupManager popupManager = GameObject.Find("Responsive Canvas").GetComponentInChildren<PopupManager>(true);
    popupEditor = Editor.CreateEditor(popupManager);
  }
}
