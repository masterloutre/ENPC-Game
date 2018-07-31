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
		//garde la transformation d'échelle / taille originelle
		defaultTransform = this.transform.localScale;
	}

	//Au début de l'event de drag, initialise le champs parent avec le parent initial de l'object
	//rend l'objet enfant du canva pour ne pas qu'il se fasse masquer par d'autres objets pendant le drag
	//Bloque les nouveaux pointers Event
	//Change la couleur des dropzones du même typeOfItem
	public void OnBeginDrag (PointerEventData eventData)
	{
		//garde le parent originel du gameObject (la Dropzone de départ à priori)
		parentToReturnTo = this.transform.parent;

		this.transform.SetParent (this.transform.root);

		//empeche tous les objects du groupe de déclencher des pointerEvents
		this.GetComponent<CanvasGroup> ().blocksRaycasts = false;

		//change la couleur de toutes les dropzone du meme typeOfItem que le Draggable GameObject
		Dropzone[] zones = GameObject.FindObjectsOfType<Dropzone> ();
		foreach (Dropzone zone in zones)
		{
			if(this.typeOfItem == zone.typeOfItem)
				zone.GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
		}
	}

	//Pendant l'event OnDrag, l'objet suit la souris
	public void OnDrag (PointerEventData eventData)
	{
		this.transform.position = eventData.position;
	}

	//à la fin de l'event, l'objet retourne à son parent
	//les events sont de nouveau enregistrés sur le groupe
	//les dropzones reviennet à leur couleur de départ
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

	//Zoom sur le hover
	public void OnPointerEnter (PointerEventData eventData){
		if (eventData.pointerDrag != null)
			return;
		this.transform.localScale += new Vector3(0.1F, 0.1f, 0);
	}

//Dézoom apres le hover
	public void OnPointerExit (PointerEventData eventData){
		if (eventData.pointerDrag != null)
			return;
		this.transform.localScale =  defaultTransform;
	}

}
