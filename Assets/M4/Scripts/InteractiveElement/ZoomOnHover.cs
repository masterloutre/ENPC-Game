using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomOnHover : InteractiveUI, IPointerEnterHandler, IPointerExitHandler
{

    public float coeffHover = 1;

    private void zoomOnHover(GameObject go)
    {
        Vector3 res = go.transform.localScale;
        res.Scale(new Vector3(coeffHover, coeffHover, coeffHover));
        go.transform.localScale = res;

    }

    private void dezoom(GameObject go)
    {
        Vector3 res = go.transform.localScale;
        res.Scale(new Vector3(1 / coeffHover, 1 / coeffHover, 1 / coeffHover));
        go.transform.localScale = res;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        zoomOnHover(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dezoom(gameObject);
    }
}
