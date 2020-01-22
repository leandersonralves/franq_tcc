using UnityEngine;
using System.Collections;

public class MoveSidebySide : MonoBehaviour {

	public Vector3[] points = new Vector3[2];
	public float smoothTime = 1f;
	public float velocity = 20f;
	private int currentTarget;
	private Vector3 currentVelocity;

	void Update () {
		if(!canMove) return;

//		transform.position = Vector3.SmoothDamp(
//			transform.position, 
//			points[currentTarget],
//			ref currentVelocity,
//			smoothTime);
//
		rigidbody2D.AddForce(
			(points[currentTarget] - transform.position).normalized * velocity,
			ForceMode2D.Force);

		if(Vector3.Distance(transform.position, points[currentTarget]) < 0.01f)
			ChangePoint();
	}

	void ChangePoint () {
		if(currentTarget == 0)
			currentTarget = 1;
		else
			currentTarget = 0;
	}

	public bool canMove = true;
	void OnCollisionEnter2D (Collision2D hit) {
		if(canMove)return;

//		if(hit.gameObject.tag == "Player")
//			rigidbody2D.isKinematic = false;
	}
}
