using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayControl : MonoBehaviour {

    GameObject enigmes;
    int indexcurrentenigma,maxenigma;

	// Use this for initialization
	void Start () {
        
        //enigmes = GameObject.Find("Enigmes Panel");

        //indexcurrentenigma = 0;
        //maxenigma = enigmes.transform.childCount;

        //for (indexcurrentenigma =0;indexcurrentenigma< enigmes.transform.childCount; indexcurrentenigma++)
        //{
        //    print(enigmes.transform.GetChild(indexcurrentenigma).name);
        //    enigmes.transform.GetChild(indexcurrentenigma).transform.gameObject.SetActive(false);
        //}
        //indexcurrentenigma = 3;//change to numero de l'enigme par où on entre
        //print(enigmes.transform.GetChild(indexcurrentenigma).name);
        //enigmes.transform.GetChild(indexcurrentenigma).transform.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void display(GameObject window)
    {
        window.SetActive(true);
    }
    public void hide(GameObject window)
    {
        window.SetActive(false);
    }
    public void nextEnigma()
    {
        indexcurrentenigma++;
        if(indexcurrentenigma >= maxenigma)
        {
            indexcurrentenigma = 0;
        }

        for (int i = 0; i < maxenigma; i++)
        {
            if (i != indexcurrentenigma)
            {
                enigmes.transform.GetChild(i).transform.gameObject.SetActive(false);
            }   
        }
        
        enigmes.transform.GetChild(indexcurrentenigma).transform.gameObject.SetActive(true);

    }
}
