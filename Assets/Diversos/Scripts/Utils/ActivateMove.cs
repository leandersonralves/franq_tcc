using UnityEngine;
using System.Collections;

public class ActivateMove : MonoBehaviour {

	public bool act = false;
	public string nameMethod = string.Empty;
	private bool activate = false;
	public GameObject sendMessage;

	void Update () {
		if(!act || activate || string.IsNullOrEmpty(nameMethod))
			return;

		activate = true;
		sendMessage.SendMessage(nameMethod, SendMessageOptions.DontRequireReceiver);
	}
}
