using UnityEngine;
using System.Collections;

public class Boulder : MonoBehaviour {

	public GameObject boulder;
	public float force;
	public float dano;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D obj ){
		Debug.Log("tag = " + obj.gameObject.tag);
		if( obj.gameObject.tag == "Gas" ){
			boulder.AddComponent<Rigidbody2D>();
			boulder.rigidbody2D.AddForce(boulder.transform.right * force);
			GameObject.Destroy(this);
		}
	}
}
