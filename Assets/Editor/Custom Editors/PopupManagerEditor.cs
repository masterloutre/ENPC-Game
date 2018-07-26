using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PopupManager))]
[CanEditMultipleObjects]
public class PopupManagerEditor : Editor
{
    SerializedProperty onlyFeedBack;


    void OnEnable()
    {
        onlyFeedBack = serializedObject.FindProperty("onlyFeedBack");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIContent label = new GUIContent();
        label.text = "Que le feedback";
        EditorGUILayout.PropertyField(onlyFeedBack, label);

        serializedObject.ApplyModifiedProperties();
    }

}
