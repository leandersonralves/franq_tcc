using UnityEngine;
using System.Collections;

public class Cheat : MonoBehaviour
{

	public Transform go;

	void OnTriggerEnter2D( Collider2D obj ) {
		if(obj.CompareTag("Player")){
			obj.transform.position = go.position;
			GameObject.Destroy(gameObject);
		}
	}
}
