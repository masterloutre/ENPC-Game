using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// script définissant comme "section de question" un GameObject, il crée un bouton de controle CaseScenarioPartIcon qui permet de masquer/afficher cette section
//[ExecuteInEditMode]
public class CaseScenarioPart : MonoBehaviour {
    public int id { get; private set; }
    public GameObject iconPrefab;
    public bool indiquerNumeroPartie;

    public CaseScenarioPartIcon icon;
    public bool conditional = false;
    public int[] chain;




    void Awake() {
        //id = Array.IndexOf(transform.parent.GetComponentsInChildren<CaseScenarioPart>(), this);
        id = transform.GetSiblingIndex();
        //EventManager.instance.AddListener<ChoiceQuestionEvent>(unseal);
    }
    private void OnDestroy()
    {
        //EventManager.instance.RemoveListener<ChoiceQuestionEvent>(unseal);
    }

    public void init() {
        print("Starting CaseScenarioPart: " + id);
        icon = new CaseScenarioPartIcon(this, iconPrefab);

    }

    public void locked(){
        foreach (int val in chain)
        {
            if (transform.parent.GetChild(val).GetComponent<CaseScenarioPart>().icon != null)
            {
                transform.parent.GetChild(val).GetComponent<CaseScenarioPart>().icon.hide();
            }
            else
            {
                print("erreur");
            }
        }
    }
/*
    public void unlock(ChoiceQuestionEvent e)
    {
        if (e.self == gameObject)
        {
            if (chain.Length != 0)
            {
                foreach (int val in chain)
                {
                    transform.parent.GetChild(val).GetComponent<CaseScenarioPart>().icon.show();
                }
            }
        }
    }

*/

public void unseal(){
  icon.show();
}

public void seal(){
  icon.hide();
  hide();
}

public void hide(){
  this.gameObject.SetActive(false);
}

public void show(){
  this.gameObject.SetActive(true);
}

    // override
	public override bool Equals(object obj)
	{

		CaseScenarioPart item = obj as CaseScenarioPart;

		if (item == null)
		{
			return false;
		}
		return this.id == item.id && this.iconPrefab == item.iconPrefab && this.gameObject.name == item.gameObject.name;
	}
	public override int GetHashCode()
	{
		return this.id.GetHashCode();
	}
}
