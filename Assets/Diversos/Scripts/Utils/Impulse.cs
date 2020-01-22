using UnityEngine;
using System.Collections;

public class Impulse : MonoBehaviour {

	public string tagCollider = "Player";
	public float force = 100f;
	public bool isGradual = true;
	public float timeToMax = 2f;

	float timeInTrigger = 0f;

	void OnTriggerStay2D (Collider2D hit) {
		if(hit.CompareTag(tagCollider))
		{
			if(timeInTrigger < timeToMax)
				timeInTrigger += Time.deltaTime;

			if(!isGradual)
				hit.rigidbody2D.AddForce(transform.right * ((timeInTrigger / timeToMax) * force * Time.deltaTime), ForceMode2D.Force);
			else
				hit.rigidbody2D.AddForce(transform.right * ((timeInTrigger / timeToMax) * force * Time.deltaTime), ForceMode2D.Impulse);
		}
	}

	void OnTriggerExit2D (Collider2D hit) {
		if(hit.CompareTag(tagCollider))
			timeInTrigger = 0f;
	}
}
