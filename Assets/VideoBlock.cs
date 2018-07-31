using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoBlock : MonoBehaviour {
	GameObject target;
	public string url;

	void Awake(){
		target = GameObject.FindWithTag("Video Player Popup");
	}

	// Use this for initialization
	void Start () {
		if(target == null){
			gameObject.SetActive(false);
			return;
		}
		target.GetComponentInChildren<RawImage>(true).gameObject.SetActive(true);
		PopupAnimation popup = target.GetComponentInChildren<PopupAnimation>(true);
		if(popup != null){
			gameObject.GetComponent<Button>().onClick.AddListener( delegate () {
				setVideoPlayerUrl();
				popup.popupGrowAnimation();
				gameObject.SetActive(false);
				});
		}
	}

	// Update is called once per frame
	void Update () {

		//gameObject.GetComponent<EventTrigger>().OnPointerClick()
	}

	public void setVideoPlayerUrl(){
		VideoPlayer player = target.GetComponentInChildren<VideoPlayer>(true);
		if(player != null){
			player.url = url;
		}
	}
}
