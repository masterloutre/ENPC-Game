/*
using UnityEngine;
using UnityEditor;
//[CustomEditor(typeof(Answer))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor
{
    SerializedProperty text;
    SerializedProperty percent;

    void OnEnable()
    {
        text = serializedObject.FindProperty("text");
        percent = serializedObject.FindProperty("percent");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(text);
        EditorGUILayout.PropertyField(percent);
        serializedObject.ApplyModifiedProperties();
    }
}
*/
