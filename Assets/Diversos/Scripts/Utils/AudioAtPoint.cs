using UnityEngine;
using System.Collections;

public class AudioAtPoint : MonoBehaviour {

	public string tagCollider = "Player";
	public AudioClip sample;

	public bool enter = true;

	public bool stay = false;
	public float delayStay = 1f;

	public bool exit = false;

	void OnTriggerEnter2D (Collider2D hit) {
		if(!enter) return;
		if(!hit.CompareTag(tagCollider)) return;

		AudioSource.PlayClipAtPoint(sample, hit.transform.position);
	}

	float time = 0f;
	void OnTriggerStay2D (Collider2D hit) {
		if(!stay) return;
		if(!hit.CompareTag(tagCollider)) return;
		
		time += Time.deltaTime;
		if(time >= delayStay)
			AudioSource.PlayClipAtPoint(sample, hit.transform.position);
	}

	void OnTriggerExit2D (Collider2D hit) {
		if(!hit.CompareTag(tagCollider)) return;
		time = 0f;
		if(!exit) return;

		AudioSource.PlayClipAtPoint(sample, hit.transform.position);
	}
}
