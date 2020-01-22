using UnityEngine;
using System.Collections;

public class CabutploxControl : MonoBehaviour {

	private Animator anim;
	private float distance;

	public string atkTag;

	private GameObject player;
	public GameObject lanca;
	public GameObject atirar;
	public float force;
	private bool atirouLanca;

	public float life = 100F;

	public float shootAtk;	
	public float spearRotateSpeed = 2F;

	public float punchAtk;

	public float moveSpeed;

	public float dmgPerHit;
	public float pullHitForce;

	private bool morto;

	private enum States{
		Patrol,
		Shoot,
		Chase,
		Fight
	}

	private States state;

	// Use this for initialization
	void Start () {
		SceneManager.OnDie += Respawn;

		anim = GetComponent<Animator>();
		state = States.Patrol;
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
					case States.Shoot: Shoot(); break;
					case States.Chase: Chase(); break;
					case States.Fight: Fight(); break;
					default: Debug.Log("ERRO - DEFAULT"); break;
				}
			}
		}
	}

	/** Funçoes de Estados FSM **/

	private void Patrol(){
		if (distance <= 4) {
			state = States.Chase;
			return;
		}
		if (distance > 7 && distance <= 8 && !atirouLanca) {
			state = States.Shoot;
			return;
		}

	}

	private void Shoot(){
		//Debug.Log ("Atirou a lança");
	}

	private void Chase(){
		if (distance <= 1.5F) {
			state = States.Fight;
			return;
		}
		if (distance > 4) {
			state = States.Patrol;
			return;
		}

		float x = Direcao();
		if (x < 0) {
			transform.position += transform.right * moveSpeed * Time.deltaTime;
		} else {
			transform.position -= transform.right * moveSpeed * Time.deltaTime;
		}
	}

	private void Fight(){
		if (distance > 1.5F) {
			state = States.Chase;
			return;
		}
	}

	private void Die(){
		morto = true;
		anim.SetBool ("morto",true);
		anim.SetTrigger ("Morrer");
		GameObject.Destroy(rigidbody2D);
		GameObject.Destroy(gameObject.GetComponent<BoxCollider2D>());
	}

	void OnTriggerEnter2D( Collider2D obj ) {
		if( obj.CompareTag("Punch")){
			if(!morto){
				life -= dmgPerHit;
				anim.SetTrigger("Dano");
				rigidbody2D.AddForce(player.transform.right * player.transform.localScale.x * pullHitForce);
			}
		}
	}

	/** Funçoes de Utilidades **/

	private void UpdateDistance(){
		distance = Vector3.Distance(player.transform.position, transform.position);
		anim.SetFloat ("DistancePlayer",distance);

		float x = Direcao();
		transform.localScale = new Vector3(x,1,1);
		if (x > 0) {
			atirar.transform.rotation = Quaternion.Euler( new Vector3(0,0,150) );
		} else {
			atirar.transform.rotation = Quaternion.Euler( new Vector3(0,0,30) );
		}
	}

	private float Direcao(){
		float res = 1;
		float x = transform.position.x;
		if (player.transform.position.x <= x) {
			res = 1;
		} else {
			res = -1;
		}

		return res;
	}

	/** Funçoes ativadas via Animaçao **/

	public void AtirarLanca(){
		float x = Direcao();

		GameObject l = GameObject.Instantiate (lanca, atirar.transform.position, atirar.transform.rotation * Quaternion.Euler(new Vector3(0,0,-90))) as GameObject;
		l.rigidbody2D.AddForce (atirar.transform.right * force);
		l.GetComponent<Lanca> ().atk = shootAtk;
		l.GetComponent<Lanca> ().rotateSpeed = spearRotateSpeed * x;

		anim.SetBool ("AtirouLanca", true);
		atirouLanca = true;
		state = States.Patrol;
	}

	public void Dano(){
		distance = Vector3.Distance(player.transform.position, transform.position);

		if(distance <= 1.5F){
			GetComponent<Dano>().Ataque(punchAtk);
		}
	}

    public void Respawn(){

    }
}
