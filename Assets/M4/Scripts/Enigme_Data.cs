using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigme_Data : MonoBehaviour {
	public string enigmaTitle;

	public enum EnigmaType {QCM, ALGO, INPUT};
	public EnigmaType enigmaType = EnigmaType.INPUT;

	public enum EnigmaDifficulty {Easy, Medium, Hard, Expert};
	public EnigmaDifficulty enigmaDifficulty = EnigmaDifficulty.Easy;



	[TextArea(3,10)]
	public string enigmaDescription;

	public string enigmaEstimatedTime;



}
