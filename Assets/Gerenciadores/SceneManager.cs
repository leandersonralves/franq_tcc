using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {
	public delegate void PlayerDie ();
	public static event PlayerDie OnDie;

	public static SceneManager instance;

	public float fadeSpeed = 1f;
	Color c = Color.black;

	void Start ()
	{
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
		c.a = 0f;
		guiTexture.color = c;
//		if(instance != null)
//		{
//			GameObject.Destroy(gameObject);
//			return;
//		}
		
		instance = this;
	}

	void OnLevelWasLoaded ()
	{
		OnDie = null;
		StartCoroutine(Fade(string.Empty, false));
	}

	public static void ReloadScene ()
	{
		instance.StartCoroutine(instance.Fade(string.Empty, true, true));
	}

	public static void LoadLevel (string levelName)
	{
		instance.StartCoroutine(instance.Fade(levelName, true));
	}

	IEnumerator Fade (string levelName, bool isIn, bool reload = false)
	{
		Color colorTarget = Color.black;
		Color colorCurrent = guiTexture.color;
		if(!isIn)
			colorTarget = c;
		
		float lerp = 0f;
		while(lerp < 1f)
		{
			lerp += Time.deltaTime * fadeSpeed;
			guiTexture.color = Color.Lerp (colorCurrent, colorTarget, lerp);
			yield return null;
		}

		if(!string.IsNullOrEmpty(levelName))
		{
			Application.LoadLevel(levelName);
			float v = AudioListener.volume;
			AudioListener.volume = 0f;
			while(Application.isLoadingLevel)
				yield return null;

			AudioListener.volume = 1f;
		}
		else if (reload && OnDie != null)
		{
			OnDie();
			StartCoroutine(Fade(string.Empty, false));
		}
	}
}
