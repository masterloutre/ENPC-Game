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
public class CreateScenarioPartPopup : PopupWindowContent
{
	Texture2D textureAsset;
	string name = "ScenarioPartTest";
	string text = "";
	bool timed = false;
	bool conditional = false;
	ScenarioPartType type = ScenarioPartType.NONE;
	string[] typeOptionArray;
	delegate GameObject CreationFunction();


		//taille du popup
    public override Vector2 GetWindowSize(){
        return new Vector2(300, 250);
    }

		///affichage des champs et bouttons et assignement des variables
    public override void OnGUI(Rect rect){
				CreationFunction createGO = null;
        GUILayout.Label("Ajouter une partie de scénario", EditorStyles.boldLabel);
				name = EditorGUILayout.TextField("Nom du GameObject", name);
				type = displayDropDownType();
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

				//bouton OK
				if(createGO != null){
					validate(createGO);
				}


    }

		//Crée un GameObject à partir du prefab diagram et le place dans le groupe Schémas de la scène actuellement ouverte
		//Si plusieurs scènes d'énigmes sont ouvertes ils seront placé dans le premier groupe Schéma trouvé
		public  GameObject createObject(){
			//crée un sprite d'apres l'image selectionnée par l'utilisateur
			Sprite sp = Sprite.Create(textureAsset,new Rect(0,0,textureAsset.width,textureAsset.height),new Vector2(0.5f,0.5f));

			//création du gameObject
			GameObject diagramGO = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets/M4/Prefabs/Enigmas/diagram.prefab", typeof(GameObject)));

			//remplissage du game Object
			diagramGO.name = name;
			Image diagramImg = diagramGO.GetComponent<Image>();
			diagramImg.sprite = sp;

			//Positionnement dans la hierarchie de la scène
			GameObject parent = GameObject.Find("Schémas");
			if(parent){
				diagramGO.transform.SetParent(parent.transform, false);
			}
			return diagramGO;
		}

		public ScenarioPartType displayDropDownType(){
					return (ScenarioPartType)EditorGUILayout.Popup((int)type, typeOptionArray);
		}

		public override void OnOpen() {
			Converter<ScenarioPartType, string> stringConverter = new Converter<ScenarioPartType, string>(
				delegate(ScenarioPartType type) { return type.ToString(); }
			);
			typeOptionArray = Array.ConvertAll(
				new ScenarioPartType[4]{ScenarioPartType.NONE, ScenarioPartType.TEXT, ScenarioPartType.IMAGE, ScenarioPartType.QUESTION},
				stringConverter
				);
		}

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

		 void validate(CreationFunction createGO){
			if (GUILayout.Button("OK", GUILayout.Width(200))) {
				GameObject GO = createGO();
				//Positionnement dans la hierarchie de la scène
				GameObject parent = GameObject.Find("Parts");
				if(parent){
					GO.transform.SetParent(parent.transform, false);
				}
				//Selectionner l'objet
				Selection.activeObject = GO;
				EditorWindow.FocusWindowIfItsOpen(typeof(SceneView));
			}
		}

}
