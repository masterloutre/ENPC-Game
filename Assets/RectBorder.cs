using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class RectBorder : MonoBehaviour {
	public GameObject borderPrefab;
	public Color color;
	[Range(0,1)]
	public float widthMultiplicator;
	private GameObject borderGO;

	// Use this for initialization
	void Start () {
		borderGO = GameObject.Instantiate(borderPrefab, transform.parent, false);
		borderGO.transform.SetAsFirstSibling();
		Rect borderRect = borderGO.GetComponent<RectTransform>().rect;
		borderRect.x = borderRect.x * (widthMultiplicator + 1); 
		borderRect.y = borderRect.x * (widthMultiplicator + 1); 
		//borderRect.size = gameObject.GetComponent<RectTransform>().rect.size * (widthMultiplicator + 1); 
		Image borderImg = borderGO.GetComponent<Image>();
		borderImg.color = color;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.isEditor){
			if(borderGO == null){
				borderGO = GameObject.Instantiate(borderPrefab, transform.parent, false);
				borderGO.transform.SetAsFirstSibling();
			}
			Rect borderRect = borderGO.GetComponent<RectTransform>().rect;
			borderRect.x = borderRect.x * (widthMultiplicator + 1); 
			borderRect.y = borderRect.x  * (widthMultiplicator + 1); 
			//borderRect.size = gameObject.GetComponent<RectTransform>().rect.size * (widthMultiplicator + 1); 
			Image borderImg = borderGO.GetComponent<Image>();
			borderImg.color = color;
			
		}
	}
}
