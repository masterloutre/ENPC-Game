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
        id = transform.GetSiblingIndex();

    }
    private void OnDestroy()
    {
    }

    public void createIcon() {
        icon = new CaseScenarioPartIcon(this, iconPrefab);
    }

public void unseal(){
  icon.show();
}

public void seal(){
  icon.hide();
  hide();
}

public void hide(){
  this.gameObject.SetActive(false);
  icon.unglow();
}

public void show(){
  this.gameObject.SetActive(true);
  icon.glow();
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
