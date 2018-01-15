using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dropzone : MonoBehaviour, IDropHandler,IPointerEnterHandler, IPointerExitHandler {
	

	public Draggable.Slot typeOfItem = Draggable.Slot.DEFAULT;
	public Color defaultColor;


	void Start(){
		defaultColor = this.GetComponent<UnityEngine.UI.Image>().color;
	}
		

	public void OnPointerExit (PointerEventData eventData)
	{

		if (eventData.pointerDrag == null)
			return;

	}


	public void OnPointerEnter (PointerEventData eventData)
	{
		if (eventData.pointerDrag == null)
			return;
	}

	

	public void OnDrop( PointerEventData eventData){
		Draggable d = eventData.pointerDrag.GetComponent<Draggable> ();



		if (d != null) {			


			if (typeOfItem == d.typeOfItem ) {
				if (this.transform.childCount > 0) {
					Draggable d2 = this.transform.GetComponentInChildren<Draggable> ();
					d2.parentToReturnTo = d.parentToReturnTo;
					d2.transform.SetParent (d2.parentToReturnTo);
				}

				d.parentToReturnTo = this.transform;
			}


		}
	}


}
