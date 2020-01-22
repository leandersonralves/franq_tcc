using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class AudioAnimation : MonoBehaviour {

	public AudioSource[] m_audioSource = new AudioSource[0]{};
	public AudioClip[] audios = new AudioClip[0]{};

	char[] separators = new char[]{'_'};

	public void Play (string clip_source) {
		string[] s = clip_source.Split(separators, System.StringSplitOptions.None);
		int clip = 0;
		int source = 0;

		if(!int.TryParse(s[0], out clip) || !int.TryParse(s[1], out source))
			return;

		if(m_audioSource[source].clip == null)
			m_audioSource[source].PlayOneShot(audios[clip]);
		else
			m_audioSource[source].Play();
	}
}
