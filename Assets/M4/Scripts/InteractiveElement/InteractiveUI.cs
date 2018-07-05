using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface InteractiveUI : IPointerDownHandler, IPointerUpHandler
{
    new void OnPointerDown(PointerEventData eventData);

    new void OnPointerUp(PointerEventData eventData);

    
	
}
