using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler {
	
	public Transform parentToReturnTo = null;

	Vector3 defaultTransform ;
	public enum Slot {DEFAULT, BLOCK, BLOCK_START, BLOCK_END, BLOCK_MIDDLE};
	public Slot typeOfItem = Slot.DEFAULT;


	void Start(){
		defaultTransform = this.transform.localScale;
	}
		
	public void OnBeginDrag (PointerEventData eventData)
	{

		parentToReturnTo = this.transform.parent;
		this.transform.SetParent (this.transform.parent.parent);

		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;

		Dropzone[] zones = GameObject.FindObjectsOfType<Dropzone> ();
		foreach (Dropzone zone in zones)
		{
			if(this.typeOfItem == zone.typeOfItem) 
				zone.GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
		}
	}

	public void OnDrag (PointerEventData eventData)
	{
		this.transform.position = eventData.position;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		this.transform.SetParent (parentToReturnTo);


		this.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		Dropzone[] zones = GameObject.FindObjectsOfType<Dropzone> ();
		foreach (Dropzone zone in zones)
		{
			if(this.typeOfItem == zone.typeOfItem) 
				zone.GetComponent<UnityEngine.UI.Image>().color = zone.defaultColor;
		}
			
	}

	public void OnPointerEnter (PointerEventData eventData){
		if (eventData.pointerDrag != null)
			return;
		this.transform.localScale += new Vector3(0.1F, 0.1f, 0);
	}

	public void OnPointerExit (PointerEventData eventData){
		if (eventData.pointerDrag != null)
			return;
		this.transform.localScale =  defaultTransform;
	}

}
