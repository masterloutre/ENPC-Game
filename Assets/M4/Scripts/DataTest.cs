using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTest : MonoBehaviour {

	void OnGUI()
   {
       if(GUI.Button(new Rect(100, 100, 150, 30), "Plus de temps"))
       {
           DataControl.control.temps += 10;
       }
       if(GUI.Button(new Rect(100, 140, 150, 30), "Moins de temps"))
       {
           DataControl.control.temps -= 10;
       }
       if(GUI.Button(new Rect(100, 180, 150, 30), "Plus de réponses"))
       {
           DataControl.control.bonnes_reponses += 10;
       }
       if(GUI.Button(new Rect(100, 220, 150, 30), "Moins de réponses"))
       {
           DataControl.control.bonnes_reponses -= 10;
       }
       if(GUI.Button(new Rect(100, 260, 150, 30), "Save"))
       {
           DataControl.control.Save();
       }
       if(GUI.Button(new Rect(100, 300, 150, 30), "Load"))
       {
           DataControl.control.Load();
       }
   }
}
