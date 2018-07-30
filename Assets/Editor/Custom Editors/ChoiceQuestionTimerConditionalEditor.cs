using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChoiceQuestionTimerConditional))]
[CanEditMultipleObjects]
public class ChoiceQuestionTimerConditionalEditor : ChoiceQuestionTimerEditor
{

    void OnEnable()
    {
        base.OnEnable();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        /*
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
        label.text = "Timer (secondes)";
        EditorGUILayout.PropertyField(time, label);

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Réponses",  EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(answerList, true);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space ();
        serializedObject.ApplyModifiedProperties();
        */
    }
}
