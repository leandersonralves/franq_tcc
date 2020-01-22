using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum AudioList {
	no_Sound,
	speech_AreYouWell,
	speech_Franq,
	speech_GetOut,
	speech_ListenMe,
	speech_Quickly,
	speech_TimeRunningOut,
	tutorial_CantStop,
	tutorial_Dash,
	tutorial_DontGiveUp,
	tutorial_Fight2,
	tutorial_Float,
	tutorial_Gas,
	tutorial_GoAhead,
	tutorial_Jump,
	tutorial_Capsule,
	tutorial_JumpPlataform,
	tutorial_Liquify,
	tutorial_Liquify2,
	tutorial_Success,
	tutorial_Success2,
	tutorial_Success3,
	tutorial_Success4,
	tutorial_Success5,
	tutorial_Walk,
	tutorial_Wrong,
	tutorial_Wrong2,
}

public class AudioManager : MonoBehaviour {

	public Dictionary<string, AudioClip> dialogues = new Dictionary<string, AudioClip>(){};
	
	AudioSource m_audioSource;
	public AudioList audioPlaying = AudioList.no_Sound;

	void Awake () {
		string[] audio_list = Enum.GetNames(typeof(AudioList));
		for(int i = 1; i < audio_list.Length; i++)
		{
			AudioClip audioClip = Resources.Load("Audio/Dialogues/" + audio_list[i]) as AudioClip;
			dialogues.Add(audio_list[i], audioClip);
		}

		m_audioSource = gameObject.AddComponent<AudioSource>();
		m_audioSource.clip = null;
		m_audioSource.loop = false;
		m_audioSource.playOnAwake = false;
	}


	#region ENFILEIRA O AUDIO
	public void PlayQueue (AudioList which, bool highPriority = true) {
		if(highPriority || (!highPriority && !m_audioSource.isPlaying))
		{
			audioPlaying = which;
			PlayQueueInstance(which.ToString());
		}
	}
	public void PlayQueue (string which) {
		try {
			audioPlaying = (AudioList)Enum.Parse(typeof(AudioList), which);
		}catch(Exception e){
			Debug.Log("Erro ao parsear o enum AudioList in AudioManager   " + e.Message);
		}

		PlayQueueInstance(which);
	}
	protected void PlayQueueInstance (string which) {
		string currentClip = string.Empty;
		if(m_audioSource.clip != null)
			currentClip = m_audioSource.clip.name;

		if(which != currentClip)
			StartCoroutine(PlayQueueBackground(currentClip, which));
	}
	
	IEnumerator PlayQueueBackground (string currentAudio, string nextAudio) {
		while(m_audioSource.clip != null && m_audioSource.clip.name == currentAudio && m_audioSource.isPlaying)
			yield return null;
		m_audioSource.clip = dialogues[nextAudio];
		m_audioSource.Play();

		while(m_audioSource.isPlaying && m_audioSource.clip.name == nextAudio)
			yield return null;

		audioPlaying = AudioList.no_Sound;
	}
	#endregion


	#region TOCA O AUDIO INSTANTANEAMENTE
	public void Play (AudioList which) {
		audioPlaying = which;
		PlayInInstance(which.ToString());
	}
	public void Play (string which) {
		PlayInInstance(which);
	}

	protected void PlayInInstance (string which) {
		m_audioSource.clip = dialogues[which];
		m_audioSource.Play();
	}
	#endregion
}
