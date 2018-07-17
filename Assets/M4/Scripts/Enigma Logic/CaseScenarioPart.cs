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

    private CaseScenarioPartIcon icon;
    public int[] chain;




    void Awake(){
		id = Array.IndexOf(transform.parent.GetComponentsInChildren<CaseScenarioPart> (), this);
	}


	void Start () {
        //not used
		CaseScenarioPartIcon icon = new CaseScenarioPartIcon (this, iconPrefab);
        foreach(int val in chain)
        {
            transform.parent.GetChild(val).GetComponent<CaseScenarioPart>().icon.hide();
        }
	}

    public void unlock()
    {
        if (chain.Length != 0)
        {
            foreach (int val in chain)
            {
                transform.parent.GetChild(val).GetComponent<CaseScenarioPart>().icon.show();
            }
        }
    }

    // ask the others to hide themselves
	public void show(){
		foreach (CaseScenarioPart scenarioPart in transform.parent.GetComponentsInChildren<CaseScenarioPart>()) {
			scenarioPart.hide ();
		}
		gameObject.SetActive (true);
	}
    // hide itself
	public void hide(){
		gameObject.SetActive (false);
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
