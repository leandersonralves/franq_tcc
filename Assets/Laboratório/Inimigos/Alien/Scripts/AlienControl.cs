using UnityEngine;
using System.Collections;

public class AlienControl : MonoBehaviour {

	private Animator anim;
	private float distance;
	
	private GameObject player;
	
	public float life = 100F;
	
	public float punchAtk;
	
	public float moveSpeed;

	private bool morto;
	
	private enum States{
		Patrol,
		Chase,
		Fight
	}
	
	private States state;

	bool ready = false;

	IEnumerator Start () {
		anim = GetComponent<Animator>();
		state = States.Patrol;

		while(Singleton.player == null)
			yield return null;

		player = Singleton.player;
		ready = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!ready) return;

		if (life <= 0 && !morto) Die ();
		
		UpdateDistance ();
		switch(state){
			case States.Patrol: Patrol(); break;
			case States.Chase: Chase(); break;
			case States.Fight: Fight(); break;
			default: Debug.Log("ERRO - DEFAULT"); break;
		}
	}
	
	/** Funçoes de Estados FSM **/
	
	private void Patrol(){
		if (distance <= 4) {
			state = States.Chase;
			return;
		}		
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
		anim.SetTrigger("morreu");
	}
	
	void OnTriggerEnter2D( Collider2D obj ) {
		if( obj.CompareTag("Punch")){
			life -= 40F;
		}
	}
	
	/** Funçoes de Utilidades **/
	
	private void UpdateDistance(){
		distance = Vector3.Distance(player.transform.position, transform.position);
		anim.SetFloat ("distancia",distance);
		
		float x = Direcao();
		transform.localScale = new Vector3(x,1,1);
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

	/** via animaçao **/

	public void Dano(){
		if(!ready) return;

		distance = Vector3.Distance(player.transform.position, transform.position);
		
		if(distance <= 1.6F && !Skills.inDefense){
			player.GetComponent<LifePlayer>().Health -= punchAtk;
		}
	}
	
	public void Morreu(){
		GameObject.Destroy(rigidbody2D);
		GameObject.Destroy(gameObject.GetComponent<BoxCollider2D>());
		GameObject.Destroy(this);
	}
}
