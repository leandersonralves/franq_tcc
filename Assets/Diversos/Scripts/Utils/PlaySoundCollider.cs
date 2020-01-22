using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(BoxCollider2D))]
public class PlaySoundCollider : MonoBehaviour {

	public bool lockPlayer = false;

	private bool inPlaying = false;

	public bool keepingInTrigger = false;
	public float timeToMaintain = 2f;
	float currentTime = 0f;

	public string tagOfColliderHit = "Player";
	public float delay = 0f;
	public bool isOnce = false;
	bool wasPlayed = false;

	void Awake () {
		if(!collider2D.isTrigger)
			collider2D.isTrigger = true;
	}

//	void Update () {
//		if(!inPlaying)
//			return;
//
//		if (isOnce && !audio.isPlaying)
//			StartCoroutine (DestroyAudio());
//	}
//
//	IEnumerator DestroyAudio () {
//		yield return new WaitForSeconds (0.5f);
//		GameObject.Destroy(gameObject);
//	}

	void OnLevelWasLoaded () {
		inPlaying = false;
		otherPlaying = false;
		wasPlayed = false;
		currentTime = 0f;
	}

	void OnTriggerStay2D (Collider2D hit) {
		if(!keepingInTrigger || audio.isPlaying || (isOnce && wasPlayed))
			return;

		if(hit.CompareTag(tagOfColliderHit)){
			currentTime += Time.deltaTime;
			if(currentTime >= timeToMaintain)
				StartCoroutine(Play());
		}
	}

	void OnTriggerEnter2D (Collider2D hit) {
		if(keepingInTrigger || audio.isPlaying || (isOnce && wasPlayed))
			return;

		if(hit.CompareTag(tagOfColliderHit))
			StartCoroutine(Play());
	}

	private static bool otherPlaying = false;
	private bool enqueue = false;
	IEnumerator LockPlayer () {
		MovePlayer.LockPlayer(true, "Sound");
		while(enqueue)
			yield return null;

		MovePlayer.LockPlayer(false, "Sound");
	}

	IEnumerator Play () {
		if(audio.clip == null)
			yield break;

		wasPlayed = true;

		enqueue = true;
		if(delay!=0)
			yield return new WaitForSeconds(delay);

		if(lockPlayer)
			StartCoroutine(LockPlayer());

		while(otherPlaying)
			yield return null;

		audio.Play();
		otherPlaying = inPlaying = true;

		while(audio.isPlaying)
			yield return null;

		otherPlaying = enqueue = false;
	}
}