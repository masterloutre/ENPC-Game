using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PopupManager))]
[CanEditMultipleObjects]
public class PopupManagerEditor : Editor
{
    SerializedProperty onlyFeedBack;
    bool hidden = false;
    GameObject popup;


    void OnEnable()
    {
      onlyFeedBack = serializedObject.FindProperty("onlyFeedBack");
      popup = ((PopupManager)target).gameObject;
      hidden = popup.activeInHierarchy;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIContent label = new GUIContent();
        label.text = "Que le feedback";
        EditorGUILayout.PropertyField(onlyFeedBack, label);



        if(Application.isEditor){
          hidden = !(popup.activeInHierarchy);
          hidden = EditorGUILayout.Toggle("Cacher Le Popup", hidden);
          popup.SetActive(!hidden);
        }





        serializedObject.ApplyModifiedProperties();
    }

}
