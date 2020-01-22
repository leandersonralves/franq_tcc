using UnityEngine;
using System.Collections;

public class PlayAnimationTriggerAnother : MonoBehaviour {

	public Animator animatorWithAnimation;
	public string nameSetTrigger;
	public string tagCollider;

	public bool playEnter;
	public bool playExit;

	public bool positionToHited = true;

	public bool isTriggerCollider = true;

	void OnTriggerEnter2D (Collider2D hit) {
		if(!isTriggerCollider || !playEnter) return;
		if(!hit.CompareTag(tagCollider)) return;

		if(positionToHited)
			animatorWithAnimation.transform.position = hit.transform.position;

		animatorWithAnimation.SetTrigger(nameSetTrigger);
	}

	void OnTriggerExit2D (Collider2D hit) {
		if(!isTriggerCollider || !playExit) return;
		if(!hit.CompareTag(tagCollider)) return;

		if(positionToHited)
			animatorWithAnimation.transform.position = hit.transform.position;

		animatorWithAnimation.SetTrigger(nameSetTrigger);
	}

	void OnCollisionEnter2D (Collision2D hit) {
		if(isTriggerCollider || !playEnter) return;
		if(!hit.collider.CompareTag(tagCollider)) return;

		if(positionToHited)
			animatorWithAnimation.transform.position = hit.transform.position;

		animatorWithAnimation.SetTrigger(nameSetTrigger);
	}
	
	void OnCollisionExit2D (Collision2D hit) {
		if(isTriggerCollider || !playExit) return;
		if(!hit.collider.CompareTag(tagCollider)) return;

		if(positionToHited)
			animatorWithAnimation.transform.position = hit.transform.position;

		animatorWithAnimation.SetTrigger(nameSetTrigger);
	}
}
