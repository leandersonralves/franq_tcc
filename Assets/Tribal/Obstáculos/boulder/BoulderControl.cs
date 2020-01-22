using UnityEngine;
using System.Collections;

public class BoulderControl : MonoBehaviour {

	private GameObject target;

	public bool hitPlayer;

	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D obj){
		if (obj.gameObject.tag == "Player" && !Skills.inDefense) {
			if(hitPlayer) target.GetComponent<Life>().Health -= 200F;
		}
	}
}
