using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ChoiceQuestion))]
[CanEditMultipleObjects]
public class ChoiceQuestionEditor : Editor
{
    public SerializedProperty text;
    public SerializedProperty img;
    public SerializedProperty answerList;
    public SerializedProperty proSitId;

    public void OnEnable()
    {
        text = serializedObject.FindProperty("text");
        proSitId = serializedObject.FindProperty("professionalSituationId");
        answerList = serializedObject.FindProperty("answerList");
        img = serializedObject.FindProperty("img");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUIContent label = new GUIContent();

        EditorGUILayout.Space ();
        EditorGUILayout.Space ();
        EditorGUILayout.LabelField("Question",  EditorStyles.boldLabel);
        label.text = "Enoncé";
        EditorGUILayout.PropertyField(text, label,  GUILayout.Height(50));
        label.text = "Image";
        label.tooltip = "L'image n'est pas obligatoire";
        EditorGUILayout.PropertyField(img, label);
        label.text = "Identifiant de la situation professionnelle";
        label.tooltip = "";
        EditorGUILayout.PropertyField(proSitId, label);
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Réponses",  EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(answerList, true);
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space ();
        serializedObject.ApplyModifiedProperties();
    }
}
