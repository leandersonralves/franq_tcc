using UnityEngine;
using System.Collections;

public class ChamandoFuncao : MonoBehaviour {

	public bool chamarFuncao = false;
	public string nomeFuncao = string.Empty;

	void Update () {
		if(!chamarFuncao || string.IsNullOrEmpty(nomeFuncao))
			return;

		gameObject.SendMessage(nomeFuncao, SendMessageOptions.DontRequireReceiver);
	}
}
