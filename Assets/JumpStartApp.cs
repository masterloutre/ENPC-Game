using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpStartApp : MonoBehaviour {
	public GameObject appPrefab;

	// Use this for initialization
	public void Awake() {
		if(FindObjectsOfType<GlobalManager>().Length == 0){
			Debug.Log ("Ap needs to jump start!!!");
			Instantiate (appPrefab, new Vector3(0, 0, 0), Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
