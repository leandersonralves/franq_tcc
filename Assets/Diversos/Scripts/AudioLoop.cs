using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioLoop : MonoBehaviour {

	void Start ()
	{
		SceneManager.OnDie += Respawn;
	}

	void Respawn ()
	{
		audio.Stop();
		audio.Play();
	}
}
