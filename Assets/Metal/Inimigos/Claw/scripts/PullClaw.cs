using UnityEngine;
using System.Collections;

public class PullClaw : MonoBehaviour {

	public float speed;
	public float pullForce;

	// Use this for initialization
	void Start () {
		GameObject.Destroy (gameObject, 8);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D obj){
		if (obj.gameObject.tag == "Player") {
			obj.gameObject.rigidbody2D.AddForce( transform.right * pullForce * -1);
		}
	}
}
