using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ChoiceQuestion))]
[CanEditMultipleObjects]
public class ChoiceQuestionEditor : Editor
{
    SerializedProperty text;
    SerializedProperty img;
    SerializedProperty answerList;
    SerializedProperty proSitId;

    void OnEnable()
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
        label.text = "Ennoncé";
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
/*
    public void displayQuestionEditor(ChoiceQuestion question){

			GUILayout.BeginVertical("box");
			Editor editor = Editor.CreateEditor((UnityEngine.Object)question);
			editor.DrawDefaultInspector();

			GUILayout.Label("Ennoncé de la question", EditorStyles.boldLabel);
			question.text = EditorGUILayout.TextArea(question.text,  GUILayout.Height(50));
			question.professionalSituationId = EditorGUILayout.IntField("Identifiant de la situation professionnelle", question.professionalSituationId);
			foreach(Answer answer in question.answerList){
				GUILayout.BeginVertical("box");


				answer.text = EditorGUILayout.TextField("Réponse", answer.text);
				answer.percent = EditorGUILayout.FloatField("Pourcentage de réussite associé à la réponse", answer.percent);

				//Editor editor = Editor.CreateEditor((UnityEngine.Object)answer);
				//Debug.Log("editor : " +editor);
				//editor.OnInspectorGUI();
				//editor.DrawDefaultInspector();

				GUILayout.EndVertical();
			}
			if (GUILayout.Button("Ajouter une réponse", GUILayout.Width(200))) {
	      addAnswer(question);
	    }


			deleteAnswerPopup(question);
			if (GUILayout.Button("Supprimer une réponse", GUILayout.Width(200))) {
	      deleteAnswer(question, selectedIndex);
	    }

			GUILayout.EndVertical();
		}
    */
}
