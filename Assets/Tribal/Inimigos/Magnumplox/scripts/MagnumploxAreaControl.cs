using UnityEngine;
using System.Collections;

public class MagnumploxAreaControl : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D obj ){
		if( obj.gameObject.tag == "boulder" ){
			obj.gameObject.rigidbody2D.AddForce(transform.right * -250F);
			anim.SetTrigger("morto");
			GameObject.Destroy(this);
		}
		if( obj.gameObject.tag == "Player" ){
			anim.SetTrigger("inimigo");
		}
	}
}
