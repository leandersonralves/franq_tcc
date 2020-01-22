using UnityEngine;
using System.Collections;

public class ShooterControl : MonoBehaviour {

	private Animator anim;
	private float distance;
	
	private GameObject player;
	
	public float life = 100F;
	
	public float punchAtk;
	public GameObject shoot;
	public GameObject atirar;
	
	public float moveSpeed;
	
	private bool morto;
	
	private enum States{
		Patrol,
		Chase
	}
	
	private States state;

	#region Patrol
	public Transform[] waypoints;
	private int currentWaypoint;
	#endregion

	private Transform lookTarget;
	private Transform target;

	public float shootCD;
	private float cooldown;
	
	void Start () {
		anim = GetComponent<Animator>();
		state = States.Patrol;

		cooldown = shootCD;
		lookTarget = waypoints[currentWaypoint];
		
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(!morto){
			if (life <= 0) Die ();
			else{
				UpdateDistance ();
				switch(state){
				case States.Patrol: Patrol(); break;
				case States.Chase: Chase(); break;
				default: Debug.Log("ERRO - DEFAULT"); break;
				}
			}
		}
	}
	
	/** Funçoes de Estados FSM **/
	
	private void Patrol(){

		lookTarget = waypoints[currentWaypoint];

		float distP = Vector3.Distance(player.transform.position, transform.position);

		if( cooldown <= 0F ){

			if (distP <= 8F) {
				state = States.Chase;
				anim.SetTrigger("shoot");
				return;
			}
			
		}else{
			cooldown -= Time.deltaTime;
		}

		float dist = Vector3.Distance(lookTarget.position, transform.position);

		if(dist > 2.5F)
		{
			float x = Direcao();
			if (x < 0) {
				transform.position += transform.right * moveSpeed * Time.deltaTime;
			} else {
				transform.position -= transform.right * moveSpeed * Time.deltaTime;
			}
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

	public void Atirar(){
		float x = Direcao();
		if (x < 0) {
			GameObject l = GameObject.Instantiate (shoot, atirar.transform.position, atirar.transform.rotation * Quaternion.Euler(new Vector3(0,0,180))) as GameObject;
		} else {
			GameObject l = GameObject.Instantiate (shoot, atirar.transform.position, atirar.transform.rotation * Quaternion.Euler(new Vector3(0,0,0))) as GameObject;
		}

		cooldown = shootCD;
		state = States.Patrol;
	}
	
	private void Chase(){
		lookTarget = player.transform;
		//chase
	}
	
	private void Die(){
		morto = true;
		anim.SetBool ("morto",true);
		anim.SetTrigger("morreu");
		GameObject.Destroy(GetComponent<Rigidbody2D>());
		GameObject.Destroy(GetComponent<BoxCollider2D>());
	}
	
	void OnTriggerEnter2D( Collider2D obj ) {
		if( obj.CompareTag("Punch")){
			life -= 40F;
		}
	}
	
	/** Funçoes de Utilidades **/
	
	private void UpdateDistance(){
		distance = Vector3.Distance(lookTarget.position, transform.position);
		
		float x = Direcao();
		transform.localScale = new Vector3(x,1,1);
	}
	
	private float Direcao(){
		float res = 1;
		float x = transform.position.x;
		if (lookTarget.position.x <= x) {
			res = 1;
		} else {
			res = -1;
		}
		
		return res;
	}
}
