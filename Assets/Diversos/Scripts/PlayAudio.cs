using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour {

	void Play () {
		if(audio.clip != null)
			audio.Play();
	}
}
