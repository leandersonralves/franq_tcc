using UnityEngine;
using System.Collections;

public class Geyser : MonoBehaviour {

	ParticleSystem m_particle;

	public float damage = 5f;
	
	public float timeToEmission = 1.3f;
	float currentTime = 0f;

	void Start () {
		m_particle = GetComponentInChildren<ParticleSystem>();
	}

	void Update () {
		if (!m_particle.isPlaying) {
			currentTime += Time.deltaTime;
			if(currentTime > timeToEmission) {
				m_particle.Play();
				currentTime = 0f;
			}
		}
	}

	void OnTriggerStay2D (Collider2D hit) {
		if(!m_particle.isPlaying)
			return;

		if(hit.CompareTag("Player") && !Skills.inDefense)
			hit.GetComponent<Life>().Health -= damage * Time.deltaTime;
	}
}
