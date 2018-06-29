using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour {
	public Text sliderText;
	
	public void displaySliderValue(){
		int value = (int)gameObject.GetComponent<Slider>().value;
		sliderText.text = value.ToString();
	}
}
