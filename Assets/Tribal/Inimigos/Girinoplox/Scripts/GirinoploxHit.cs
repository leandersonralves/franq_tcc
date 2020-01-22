using UnityEngine;
using System.Collections;

public class GirinoploxHit : MonoBehaviour
{

	private float stompDmg;

	void Start(){
		stompDmg = gameObject.GetComponentInParent<GirinoploxControl> ().stompDmg;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" && !Skills.inDefense){
			coll.gameObject.GetComponent<Life>().Health -= stompDmg;
		}
	}
}

