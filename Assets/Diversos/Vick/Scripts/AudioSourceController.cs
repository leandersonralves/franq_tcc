using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum QueueSound {
	DrVic,
	Ambient
}

[RequireComponent(typeof(AudioSource))]
public class AudioSourceController : MonoBehaviour {

	AudioSource source;
	List<AudioClipStore> queues = new List<AudioClipStore>();

	void Start () {
		source = GetComponent<AudioSource>();
	}

	void Update () {
		if(queues.Count < 1)
			return;

		if(queues[0].waitPrevious && source.isPlaying)
			return;

		source.clip = queues[0].audioClip;
		source.Play();

		if(queues[0].once)
		{
			AudioClipStore cache_storeclip = queues[0];
			queues.RemoveAt(0);
			GameObject.Destroy(cache_storeclip);
		}
		else
			queues.RemoveAt(0);
	}

	void OnTriggerEnter2D (Collider2D hit) {
		queues.Add(hit.GetComponent<AudioClipStore>());

		if(hit.GetComponent<AudioClipStore>().once)
			GameObject.Destroy(hit.gameObject);
	}
}
