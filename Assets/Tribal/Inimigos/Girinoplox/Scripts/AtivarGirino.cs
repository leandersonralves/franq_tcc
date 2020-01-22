using UnityEngine;
using System.Collections;

public class AtivarGirino : MonoBehaviour {

	public GameObject girino;

	void OnTriggerEnter2D( Collider2D obj ) {
		if(obj.CompareTag("Player")){
			girino.SetActive(true);
			GameObject.Destroy(gameObject);
		}
	}

}
