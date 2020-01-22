using UnityEngine;
using System.Collections;

public class ParticleCollider : MonoBehaviour {

	public string tagCollider = "Player";
	public ParticleSystem particle_system;

	public bool positionContact = true;

	public bool enter = true;

	public bool stay = false;
	public float delayStay = 1f;

	public bool exit = false;

	void OnTriggerEnter2D (Collider2D hit) {
		if(!enter) return;
		if(!hit.CompareTag(tagCollider)) return;

		if(positionContact)
			particle_system.transform.position = hit.transform.position;

		particle_system.Play();
	}

	float time = 0f;
	void OnTriggerStay2D (Collider2D hit) {
		if(!stay) return;
		if(!hit.CompareTag(tagCollider)) return;
		
		time += Time.deltaTime;
		if(time >= delayStay)
		{
			if(positionContact)
				particle_system.transform.position = hit.transform.position;

			particle_system.Play();
		}
	}

	void OnTriggerExit2D (Collider2D hit) {
		if(!hit.CompareTag(tagCollider)) return;
		time = 0f;
		if(!exit) return;

		if(positionContact)
			particle_system.transform.position = hit.transform.position;

		particle_system.Play();
	}
}
