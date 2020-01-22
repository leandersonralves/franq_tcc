using UnityEngine;
using System.Collections;

public class Pedras : MonoBehaviour
{
	public GameObject[] pedras;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			foreach(GameObject pedra in pedras){
				pedra.transform.parent = null;
				pedra.AddComponent<Rigidbody2D>();
			}
			GameObject.Destroy(GetComponent<BoxCollider2D>());
			GameObject.Destroy(this);
		}
	}
}

