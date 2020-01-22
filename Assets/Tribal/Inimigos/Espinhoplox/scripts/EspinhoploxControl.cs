using UnityEngine;
using System.Collections;

public class EspinhoploxControl : MonoBehaviour {

	#region Patrol
	public Transform[] waypoints;
	private int currentWaypoint;
	#endregion

	public float speed;
	public float atkPower;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = waypoints[currentWaypoint].position - transform.position;
		
		if(dir.magnitude > 0.2F)
		{
			rigidbody2D.velocity = dir.normalized * speed;
		}
		else
		{
			currentWaypoint++;
			if(currentWaypoint >= waypoints.Length)
			{
				currentWaypoint = 0;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" && !Skills.inDefense)
			coll.gameObject.GetComponent<Life>().Health -= atkPower;
	}
}
