using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigme_Data : MonoBehaviour {
    public int  enigmaId;
	public string enigmaTitle;

	//public enum EnigmaType {QCM, ALGO, INPUT};
	public EnigmaType enigmaType = EnigmaType.INPUT;

	//public enum EnigmaDifficulty {Easy, Medium, Hard, Expert};
	public EnigmaDifficulty enigmaDifficulty = EnigmaDifficulty.Easy;
    public int enigmaMaxAttempts = 1;
    public float time;

	[TextArea(3,10)]
	public string enigmaDescription;

	public string enigmaEstimatedTime;

}
