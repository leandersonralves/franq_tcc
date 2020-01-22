using UnityEngine;
using System.Collections;

public class PassaroploxControl : MonoBehaviour {

	private Animator anim; 
	
	public float speed;
	public float atkDist;
	public float atkPower;
	
	private Vector3 lookTarget;
	private Vector3 target;
	private Vector3 targetDir;

	private Transform player;
	
	public enum States
	{
		Patrol,
		Bite,
		Chase
	}
	public States state = States.Patrol;
	
	public float biteCD;
	private float cooldown;
	
	#region Patrol
	public Transform[] waypoints;
	private int currentWaypoint;
	#endregion
	
	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();
		player = GameObject.FindWithTag("Player").transform;
		cooldown = 0F;
		lookTarget = waypoints[currentWaypoint].position;
	}
	
	private void LookDirection(){
		
		float res = 1;
		if (lookTarget.x <= transform.position.x) {
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
		lookTarget = waypoints[currentWaypoint].position;
		
		if( cooldown <= 0F ){
			
			targetDir = player.position - transform.position;
			if(targetDir.magnitude < atkDist)
			{
				Vector3 pos = player.position;
				pos = new Vector3(pos.x, pos.y + 1, pos.z);

				target = pos;
				state = States.Chase;
				anim.SetBool("atacar",true);
				
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
		
		targetDir = target - transform.position;

		if(targetDir.magnitude >= (atkDist + 0.5F) || targetDir.magnitude <= 1F )
		{
			cooldown = biteCD;
			anim.SetBool("atacar",false);
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
		Debug.Log(coll.gameObject.tag);

		if (coll.gameObject.tag == "Player" && !Skills.inDefense){
			coll.gameObject.GetComponent<Life>().Health -= atkPower;
		}
		cooldown = biteCD;
		state = States.Patrol;
		anim.SetBool("atacar",false);		
	}
}
