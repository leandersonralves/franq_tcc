using UnityEngine;
using System.Collections;

public class Folha : MonoBehaviour {

	private Quaternion min;
	private Quaternion max;

	private Quaternion rotateTo;
	private Quaternion rotateFrom;

	public float speed;

	float currentTime = 0f;

	void Start(){
		min = Quaternion.Euler (new Vector3 (0,0,0));
		max = Quaternion.Euler (new Vector3 (0,0,75));

		rotateTo = Quaternion.identity;
		rotateFrom = Quaternion.identity;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			currentTime = 0f;
			rotateTo = max;
			rotateFrom = transform.rotation;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			currentTime = 0f;
			rotateTo = min;
			rotateFrom = transform.rotation;
		}		
	}

	void Update(){
		currentTime += Time.deltaTime * speed;
		transform.rotation = Quaternion.Lerp (rotateFrom, rotateTo, currentTime);
	}
}
