using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour {

	static Singleton singleton;
	
	GameObject playerGameObject;
	LifePlayer lifePlayer;

	void Awake () {
		if(singleton == null)
		{
			GameObject.DontDestroyOnLoad(gameObject);
			singleton = this;
		}
		else
			GameObject.Destroy(gameObject);
	}

	#region DADOS DO JOGADOR
	public static GameObject player
	{
		get {
			if(singleton.playerGameObject == null)
				singleton.playerGameObject = GameObject.FindWithTag("Player");

			return singleton.playerGameObject;
		}
	}

	public static float LifePlayer
	{
		get {
			if(singleton.lifePlayer == null)
			{
				if(player == null)
					return 100f;

				singleton.lifePlayer = player.GetComponent<LifePlayer>();
			}

			if(singleton.lifePlayer != null)
				return singleton.lifePlayer.health;
			else
				return 0f;
		}
	}
	#endregion

	#region AUDIO AND SOUND
	AudioManager audioManager = null;
	public static AudioManager audio {
		get {
			if(!singleton.audioManager && singleton.GetComponentInChildren<AudioManager>())
				singleton.audioManager = singleton.GetComponentInChildren<AudioManager>();

			return singleton.audioManager;
		}
	}
	#endregion

	#region FEEDBACK MANAGER
	FeedbackManager feedbackManager = null;
	public static FeedbackManager feedback {
		get {
			if(singleton.feedbackManager == null && singleton.GetComponentInChildren<FeedbackManager>() != null)
				singleton.feedbackManager = singleton.GetComponentInChildren<FeedbackManager>();

			return singleton.feedbackManager;
		}
	}
	#endregion
}
