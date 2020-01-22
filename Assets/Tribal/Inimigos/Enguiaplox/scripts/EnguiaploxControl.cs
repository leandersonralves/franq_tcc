using UnityEngine;
using System.Collections;

public class EnguiaploxControl : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D obj ){
		if( obj.gameObject.tag == "Player" ){
			anim.SetTrigger("perto");
		}
	}

}
