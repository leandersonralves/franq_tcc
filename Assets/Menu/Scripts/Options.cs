using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

	public GUISkin skin;

	float top;
	float left;
	float width;
	float height;

	public float l;
	public float t;
	public float h;
	public float w;

	bool isMute = false;

	void Start ()
	{
		Refresh();
		currentVolume = AudioListener.volume;
	}

	void Refresh ()
	{
		left = Screen.width * l;
		top =  Screen.height * t;
		width = Screen.width * w;
		height = Screen.height * h;
	}

	float currentVolume;
	bool vSync = true;

	void OnGUI ()
	{
		GUI.skin = skin;
		GUILayout.BeginArea(new Rect(left, top, width, height));
		
		#region VSYNC
		GUILayout.BeginHorizontal();
			vSync = GUILayout.Toggle(vSync, "VSync: ");
			if(vSync && QualitySettings.vSyncCount != 2)
				QualitySettings.vSyncCount = 2;
			else if (!vSync && QualitySettings.vSyncCount != 0)
				QualitySettings.vSyncCount = 0;
		GUILayout.EndHorizontal();
		#endregion

		#region MUTE
		GUILayout.BeginHorizontal();
			isMute = GUILayout.Toggle(isMute, "Mute: ");
			if(isMute && AudioListener.volume != 0f)
				AudioListener.volume = 0f;
			else if (!isMute && AudioListener.volume != currentVolume)
				AudioListener.volume = currentVolume;
		GUILayout.EndHorizontal();
		#endregion

		if(!isMute) {
		#region VOLUME
		GUILayout.BeginHorizontal();
			GUILayout.Label("Volume:");
			AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0f, 1f);
			currentVolume = AudioListener.volume;
		GUILayout.EndHorizontal();
		#endregion
		}

		GUILayout.EndArea();
	}
}
