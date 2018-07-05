using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour, InteractiveUI {


	float coeffHover = 1.5f;

	public void InfoChangeState(GameObject go){
		go.SetActive (!go.activeSelf);
	}
    
    public void zoomOnHover(GameObject go){
        Vector3 res = go.transform.localScale;
        res.Scale(new Vector3(coeffHover, coeffHover, coeffHover));
        go.transform.localScale=res;

	}

	public void dezoom(GameObject go){
        Vector3 res = go.transform.localScale;
        res.Scale(new Vector3(1/coeffHover, 1/coeffHover, 1/coeffHover));
        go.transform.localScale = res;
    }

	public void exitGameButton(){
        // ne fonctionne pas sur l'éditeur, uniquement en application
		Application.Quit ();
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        zoomOnHover(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
