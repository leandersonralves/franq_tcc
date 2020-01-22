using UnityEngine;
using System.Collections;

public class Cooldown : MonoBehaviour {
	public float timeToRecupair = 3f;
	public float delayToRecupair = 1.3f;

	public static float current = 1f;
	public static bool canRecupair = true;
	private float currentDelay = 0f;
	private float factorRecuperation;

	public bool infiniteCooldown = false;

	public static Cooldown m_instance;

	void Awake ()
	{
		m_instance = this;
	}

	void Start ()
	{
		SceneManager.OnDie += Respawn;

		factorRecuperation = 1 / timeToRecupair;
	}

	void Update ()
	{
		if(current == 1f || !canRecupair)
			return;

		if(currentDelay < delayToRecupair){
			currentDelay += Time.deltaTime * factorRecuperation;
			return;
		}

		current += Time.deltaTime * factorRecuperation;

		if(current > 1) {
			current = 1f;
			currentDelay = 0f;
		}
	}

	public bool Expend (float factor)
	{
		if(infiniteCooldown)
			return true;

		float valueExpend = factor * Time.deltaTime;
		currentDelay = 0f;

		if(current < valueExpend)
			return false;

		current -= valueExpend;

		return true;
	}

	void Respawn ()
	{
		current = 1f;
		currentDelay = 0f;
	}
}
