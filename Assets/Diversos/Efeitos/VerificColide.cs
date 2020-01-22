using UnityEngine;
using System.Collections;

public class VerificColide : MonoBehaviour
{
	void OnTriggerStay2D( Collider2D obj ) {
		Debug.Log(obj.tag);
	}
}

