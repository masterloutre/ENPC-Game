using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;


/**
 * Popup permettant d'ajouter un élément dans une énigme
 * Le gameObject est créé d'apres un prefab
 * Il est placé dans un groupe groupe particulier de la(les) scène(s) actuellement chargée(s)
 * Si ce groupe à un doublon, il est placé dans le premier trouvé
 */
public class CreateElementPopup : PopupWindowContent
{
  public float defaultButtonWidth = 200;
	public string name = "";
  public string errorMssg = "no message";
	public delegate GameObject CreationFunction();
  public CreationFunction createGO = null;
  public string parentName = "Enigma Content";


	//taille du popup
  public override Vector2 GetWindowSize(){
      return new Vector2(400, 350);
  }

  //Boutton de validation, prend en paramètre le delegate qui crée l'objet
  public virtual void validate(CreationFunction createGO){
    if (GUILayout.Button("Créer", GUILayout.Width(defaultButtonWidth))) {
       //crée l'objet
       GameObject GO = createGO();
       //Positionnement dans la hierarchie de la scène
       GameObject parent = GameObject.Find(parentName);
       if(parent){ GO.transform.SetParent(parent.transform, false);}
       //Selectionne l'objet dans l'éditeur
       Selection.activeObject = GO;
       //Sortir du popup en mettant le focus sur le vue de la scène
       EditorWindow.FocusWindowIfItsOpen(typeof(SceneView));
    }
  }

	///affichage du bouton OK et création de l'objet
  public void displayCreateButton(){
			//bouton OK
			if(createGO != null){
				validate(createGO);
			} else {
        displayErrorMssg();
      }
  }

  public void displayErrorMssg(){
    EditorGUILayout.BeginVertical("box");
    GUILayout.Label("Erreur --> " + errorMssg, EditorStyles.boldLabel);
    EditorGUILayout.EndVertical();
  }

  public override void OnGUI(Rect rect){

  }
}
