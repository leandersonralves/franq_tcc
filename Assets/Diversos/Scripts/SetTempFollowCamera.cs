using UnityEngine;
using System.Collections;

public class SetTempFollowCamera : MonoBehaviour {

	public string tagCollider = "Player";
	public bool once = true;

	public float timeToFollow = 1f;
	public float velocityToTarget = 0.5f;

	public Transform target;

	void OnTriggerEnter2D (Collider2D hit)
	{
		if(hit.CompareTag(tagCollider))
			MoveCamera.SetTarget(target, timeToFollow, velocityToTarget);
	}

	void OnTriggerExit2D (Collider2D hit)
	{
		if(hit.CompareTag(tagCollider) && once)
			GameObject.Destroy(gameObject);
	}

	void OnCollisionEnter2D (Collision2D hit)
	{
		if(hit.collider.CompareTag(tagCollider))
			MoveCamera.SetTarget(target, timeToFollow, velocityToTarget);
	}
	
	void OnCollisionExit2D (Collision2D hit)
	{
		if(hit.collider.CompareTag(tagCollider) && once)
			GameObject.Destroy(gameObject);
	}
}
