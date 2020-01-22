using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {
	public string nameLevel;
	void LoadLevel () {
		if (!string.IsNullOrEmpty (nameLevel))
		{
			SceneManager.LoadLevel(nameLevel);
//			Application.LoadLevel (nameLevel);
		}
	}
}
