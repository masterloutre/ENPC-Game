using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionManager : InteractiveUI
{
    
    

	public void InfoChangeState(GameObject go){
		go.SetActive (!go.activeSelf);
	}
    
    
    public void exitGameButton(){
        // ne fonctionne pas sur l'éditeur, uniquement en application
		Application.Quit ();
	}
    
}
