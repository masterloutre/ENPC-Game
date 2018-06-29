using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
/* default unity value : 240*90, scale 1 anchor center fontsize 20*/
public class AnswerBlock
{
    Vector2 pos;
    string text;
    float width, height;
    public GameObject go;
    public AnswerBlock()
    {

    }
    public AnswerBlock(GameObject prefab,string msg,Vector2 coord)
    {
        text=msg;
        pos = coord;
        go = GameObject.Instantiate(prefab,coord,new Quaternion(0,0,0,0));
        width= go.GetComponentInChildren<Image>().gameObject.GetComponent<RectTransform>().rect.width;
        height = go.GetComponentInChildren<Image>().gameObject.GetComponent<RectTransform>().rect.height;

        go.GetComponentInChildren<Text>().text = msg;
        go.GetComponent<RectTransform>().anchoredPosition = pos;

    }
    public void parent(GameObject par)
    {
        go.transform.SetParent(par.transform,false);
    }
    public void script(UnityEngine.Events.UnityAction method)
    {
        go.GetComponent<Button>().onClick.RemoveAllListeners();
        go.GetComponent<Button>().onClick.AddListener(method);
        

    }
    public void moveto(Vector2 coord)
    {
        go.GetComponent<RectTransform>().anchoredPosition = coord;
        pos = coord;
    }
    public void moveby(Vector2 coord)
    {
        go.GetComponent<RectTransform>().anchoredPosition += coord;
        pos += coord;
    }
    public void scaleto(float large,float haut)
    {
        width = large;
        height = haut;
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        
    }
    public void scaleby(float large, float haut)
    {
        width *= large;
        height *= haut;
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
}