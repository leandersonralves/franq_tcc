using UnityEngine;
using System.Collections;

public class LoadLevelAnimation : MonoBehaviour {

	public string nameLevel = "minigame1";

	void Pass () {
		SceneManager.LoadLevel(nameLevel);
//		Application.LoadLevel(nameLevel);
	}
}
