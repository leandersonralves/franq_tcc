using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {
	
	MovieTexture intro;
	public string levelLoad;

	IEnumerator Start () {
		Screen.showCursor = false;
		Screen.lockCursor = true;
		intro = renderer.material.mainTexture as MovieTexture;

		if(intro == null)
			yield break;
		
		while(!intro.isReadyToPlay)
			yield return null;
		
		intro.Play();
		audio.Play();
	}
	
	void Update ()
	{
		if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
			LoadLevel();

		if(intro == null)
			return;
		
		if(!intro.isPlaying)
			LoadLevel();
	}

	void LoadLevel ()
	{
		intro.Stop();
		audio.Stop();
		renderer.material.color = Color.black;

		SceneManager.LoadLevel(levelLoad);
//		Application.LoadLevel(levelLoad);
	}
}
