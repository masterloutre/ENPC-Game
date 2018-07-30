using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

public enum ScenarioPartType { NONE, TEXT, IMAGE, QUESTION};

/**
 * Popup permettant d'ajouter une partie d'énoncé dans une énigme
 * Le gameObject est créé d'apres le prefab InfoPart, FormulaPart ou une variante de QuestionPart
 * Il est placé dans le groupe Scenario/Parts de la scène actuelle
 * @type {[type]}
 */
public class CreateScenarioPartPopup : CreateElementPopup
{
	Texture2D textureAsset;
	bool enigmaTypeError = false;
	string text = "";
	bool timed = false;
	bool conditional = false;
	ScenarioPartType type = ScenarioPartType.NONE;
	string[] typeOptionArray;


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(300, 250);
    }

		//initialisation
		public override void OnOpen() {
			//création de la liste d'options du dropdown des types
			Converter<ScenarioPartType, string> stringConverter = new Converter<ScenarioPartType, string>(
				delegate(ScenarioPartType type) { return type.ToString(); }
			);
			typeOptionArray = Array.ConvertAll(
				new ScenarioPartType[4]{ScenarioPartType.NONE, ScenarioPartType.TEXT, ScenarioPartType.IMAGE, ScenarioPartType.QUESTION},
				stringConverter
			);
			name = "ScenarioPart";
			parentName = "Parts";
			if(GameObject.Find("Managers").GetComponentInChildren<CaseValidation>() == null){
				createGO = null;
				enigmaTypeError = true;
				errorMssg = "Attention, vous n'avez pas ouvert une énigme de type Study Case";

			}

		}

		//dropdown
		public ScenarioPartType displayDropDownType(){
					return (ScenarioPartType)EditorGUILayout.Popup((int)type, typeOptionArray);
		}

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
        GUILayout.Label("Ajouter une partie de scénario", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom du GameObject", name);
				type = displayDropDownType();

				if(enigmaTypeError){
					createGO = null;
					displayCreateButton();
					return;
				}

				switch (type){
					case ScenarioPartType.TEXT :
						{
							text = EditorGUILayout.TextArea(text, GUILayout.Height(50));
							createGO = createTextScenarioPart;
						}
						break;
					case ScenarioPartType.IMAGE :
						{
							textureAsset = (Texture2D)EditorGUILayout.ObjectField("selectionez une image", textureAsset, typeof(Texture2D), true);
							createGO = createImageScenarioPart;
						}
						break;
					case ScenarioPartType.QUESTION :
						{
							timed = EditorGUILayout.Toggle("Question en temps limité", timed);
							if(timed){
								conditional = EditorGUILayout.Toggle("La question cache les parties de scénario suivantes", conditional);
							} else {
								conditional = false;
							}
							createGO = createQuestionScenarioPart;
						}
						break;
					case ScenarioPartType.NONE :
						return;
					default :
						break;
				}

				displayCreateButton();
    }


		/****CREATION DES OBJETS *****/

		GameObject createImageScenarioPart(){
			GameObject GO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/StudyCase/FormulaPart.prefab", typeof(GameObject)));
			GO.name = name;
			if(textureAsset != null){
				Sprite sp = Sprite.Create(textureAsset,new Rect(0,0,textureAsset.width,textureAsset.height),new Vector2(0.5f,0.5f));
				GO.GetComponent<Image>().sprite = sp;
			}
			return GO;
		}

		GameObject createTextScenarioPart(){
			GameObject GO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/StudyCase/InfoPart.prefab", typeof(GameObject)));
			GO.name = name;
			GO.GetComponent<Text>().text = text;
			return GO;
		}

		GameObject createQuestionScenarioPart(){
			string prefabName;
			if(timed && conditional){ prefabName = "QuestionPartConditional"; }
			else if (timed) { prefabName = "QuestionPartTimer"; }
			else { prefabName = "QuestionPart"; }
			GameObject GO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/StudyCase/" + prefabName + ".prefab", typeof(GameObject)));
			GO.name = name;
			return GO;
		}

}
