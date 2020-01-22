using UnityEngine;
using System.Collections;

public class PeixeploxControl : MonoBehaviour {

	private Animator anim;

	private Transform target;

	public float speed;
	public float atkDist;
	public float atkPower;

	private Transform lookTarget;

	public enum States
	{
		Patrol,
		Bite,
		Chase
	}
	public States state = States.Patrol;

	public float biteCD;
	private float cooldown;

	private Vector3 targetDir;

	#region Patrol
	public Transform[] waypoints;
	private int currentWaypoint;
	#endregion

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();
		target = GameObject.FindWithTag("Player").transform;
		cooldown = 0F;
		lookTarget = waypoints[currentWaypoint];
	}

	private void LookDirection(){

		float res = 1;
		if (lookTarget.position.x <= transform.position.x) {
			res = 1;
		} else {
			res = -1;
		}

		transform.localScale = new Vector3(res,1,1);

	}
	
	// Update is called once per frame
	void Update () {
		LookDirection();

		switch(state)
		{
			case States.Patrol: 	PatrolState();		break;
			case States.Chase: 		ChaseState();		break;
			case States.Bite: 		BiteState();		break;
			default: Debug.LogError("BUG: NPC should never be on default!"); break;
		}
	}

	private void PatrolState()
	{
		lookTarget = waypoints[currentWaypoint];

		if( cooldown <= 0F ){

			targetDir = target.position - transform.position;
			if(targetDir.magnitude < atkDist)
			{
				state = States.Chase;
				anim.SetBool("seguir",true);

				return;
			}

		}else{
			cooldown -= Time.deltaTime;
		}
		
		Vector3 dir = waypoints[currentWaypoint].position - transform.position;
		
		if(dir.magnitude > 1)
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

	private void ChaseState()
	{
		lookTarget = target;

		targetDir = target.position - transform.position;
		if(targetDir.magnitude >= (atkDist + 0.5F))
		{
			anim.SetBool("seguir",false);
			state = States.Patrol;
			return;
		}
		rigidbody2D.velocity = targetDir.normalized * speed;
	}

	private void BiteState()
	{
		state = States.Patrol;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player"){
			if(!Skills.inDefense)
				coll.gameObject.GetComponent<Life>().Health -= atkPower;
			cooldown = biteCD;
			state = States.Bite;
			anim.SetBool("seguir",false);
		}
	}
}
