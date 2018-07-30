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
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIContent label = new GUIContent();
        label.text = "Que le feedback";
        EditorGUILayout.PropertyField(onlyFeedBack, label);

        hidden = !(popup.activeInHierarchy);
        hidden = EditorGUILayout.Toggle("Cacher Le Popup", hidden);
        popup.SetActive(!hidden);

        


        serializedObject.ApplyModifiedProperties();
    }

}
