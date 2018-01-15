using UnityEngine;
using System.Collections;

public class PopupAnimation : MonoBehaviour {
	private float   rate;

	bool isActive = false;
	public float speed = 0.05f;

	private UnityEngine.UI.RawImage videoplayer;
	private UnityEngine.Video.VideoPlayer video;


	bool isShrinkRunning = false;
	bool isGrowRunning = false;

	void Start () {

		videoplayer = this.transform.GetComponentInChildren<UnityEngine.UI.RawImage> ();
		video = this.transform.GetComponentInChildren<UnityEngine.Video.VideoPlayer> ();
		if (videoplayer != null) {
			videoplayer.transform.localScale = new Vector3 (0, 0, 0);
		}

		//xlength = 0;
		//ylength = 0;
	}

	public void popupGrowAnimation()
	{
		if (video != null && !isGrowRunning && !isShrinkRunning) {			
			video.Play ();
			StartCoroutine ("GrowAnimation");
			isActive = true;
		}
			
	}


	public void popupShrinkAnimation(){
		if (video != null && !isShrinkRunning && !isGrowRunning) {
			StartCoroutine ("ShrinkAnimation");
			rate = 0;
			isActive = false;
		}
	}

	IEnumerator GrowAnimation()
	{		
		
					

			isGrowRunning = true;
			while (videoplayer.transform.localScale.x < 1) {
				AnimController (speed);
			
				yield return null;
			}
			StopCoroutine ("GrowAnimation");
			isGrowRunning = false;

	}

	IEnumerator ShrinkAnimation()
	{
		

			isShrinkRunning = true;
			while (videoplayer.transform.localScale.x > 0) {
				AnimController (-speed);

				yield return null;
			}
			StopCoroutine ("ShrinkAnimation");
			isShrinkRunning = false;
			video.Stop ();

	}

	void AnimController(float speed)
	{
		rate+=Time.deltaTime*speed;
		videoplayer.transform.localScale += new Vector3(rate ,rate,rate);

		if (videoplayer.transform.localScale.x < 0) {
			videoplayer.transform.localScale = new Vector3 (0, 0, 0);
		}
	}


}