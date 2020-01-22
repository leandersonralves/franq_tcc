using UnityEngine;
using System.Collections;

public class GirinoploxControl : MonoBehaviour {

	public float maxCooldown;
	private float cooldown;

	public float maxAirCooldown;
	private float airCooldown;
	private bool airChange;

	private Vector3 shadowPosition;
	public GameObject shadow;
	private float shadowAlpha;
	public float increase;
	private float floorY;
	public GameObject groundCollider;
	public GameObject hitCollider;
	public float stompDmg;

	private GameObject player;
	private Animator anim;
	public GameObject cabut;
	private Animator cabutAnim;

	public float jumpForce;
	public int maxJumps;
	private int jumps;

	public float maxLancaCooldown;
	private float lancaCooldown;
	public float lancaDmg;
	public float lancaForce;
	public float lancaForceRange;
	public GameObject lanca;
	public GameObject atirar;
	public float spearRotateSpeed = 2F;

	public GameObject spit;
	public Transform mouth;
	public float spitForce;
	public float spitAtk;
	public int spitChance; // 1 a 100
	public float spitCooldown;
	private float spitCD;

	public float life;

	public GameObject plataforma;
	public GameObject plataformaTrigger;

	private enum States{
		Idle,
		JumpUp,
		Air,
		JumpDown,
		Spit
	}
	
	private States state;

	// Use this for initialization
	void Start () {
		cooldown = maxCooldown;
		airCooldown = maxAirCooldown;
		lancaCooldown = maxLancaCooldown;
		spitCD = spitCooldown;

		player = GameObject.FindWithTag("Player");

		anim = GetComponent<Animator>();
		cabutAnim = cabut.GetComponentInChildren<Animator>();

		floorY = shadow.transform.position.y;

		shadowPosition = new Vector3(0,0,0);

		state = States.Idle;

		jumps = 1;

		shadowAlpha = 0F;
		shadow.GetComponent<SpriteRenderer>().color = new Color(0F,0F,0F,shadowAlpha);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (state);

		switch(state){
			case States.Idle: Idle(); break;
			case States.JumpUp: JumpUp(); break;
			case States.Air: Air(); break;
			case States.Spit: Spit(); break;
			case States.JumpDown: JumpDown(); break;
			default: Debug.Log("ERRO - DEFAULT"); break;
		}

	}

	void Die(){
		Debug.Log("acabou a vida");
		cooldown = maxCooldown;
		lancaCooldown = maxLancaCooldown;
		spitCD = spitCooldown;
		state = States.JumpUp;
		return;
	}

	void Idle(){
		if (life <= 0) Die ();
		else{
			if( cooldown >= 0 ){
				cooldown -= Time.deltaTime;

				if( lancaCooldown >= 0 ){
					lancaCooldown -= Time.deltaTime;
				}else{
					cabutAnim.SetBool("atacar",true);
					lancaCooldown = maxLancaCooldown;
				}

				if(spitCD <= 0F){

					int r = Random.Range(1,100);

					if(r <= spitChance){
						cooldown = spitCooldown - 1F;
						lancaCooldown = maxLancaCooldown;
						spitCD = spitCooldown;

						anim.SetTrigger("acido");
						state = States.Spit;
					}

				}else spitCD -= Time.deltaTime;

			}else{
				cooldown = maxCooldown;
				lancaCooldown = maxLancaCooldown;
				spitCD = spitCooldown;
				state = States.JumpUp;
			}

		}
	}

	void Spit(){
		//Debug.Log("spit state");
	}

	void JumpUp(){
		anim.SetBool("jump",true);

		if (life > 0){
			SetShadowPosition();
			shadow.transform.parent = null;
			shadow.transform.rotation = Quaternion.identity;
			shadow.transform.position = shadowPosition;
			shadow.GetComponent<SpriteRenderer>().enabled = true;
		}

		groundCollider.SetActive (false);
		hitCollider.SetActive (false);

		state = States.Air;
	}

	void Air(){
		if( airCooldown >= 0 ){
			if( airChange ){
				shadowAlpha += increase;
				shadow.GetComponent<SpriteRenderer>().color = new Color(0F,0F,0F,shadowAlpha);
			}else{
				airCooldown -= Time.deltaTime;
			}
		}else{
			if( life <= 0 ){ 
				plataforma.SetActive(true);
				plataformaTrigger.SetActive(true);
				GameObject.Destroy(gameObject); //morreu
			}
			airChange = true;
			airCooldown = maxAirCooldown;

			transform.position = new Vector3(shadowPosition.x, transform.position.y, transform.position.z);

			float dir = Direcao();
			transform.localScale = new Vector3(dir,1,1);
			if (dir > 0) {
				atirar.transform.rotation = Quaternion.Euler( new Vector3(0,0,150) );
				mouth.rotation = Quaternion.Euler( new Vector3(0,0,220) );
			} else {
				atirar.transform.rotation = Quaternion.Euler( new Vector3(0,0,30) );
				mouth.rotation = Quaternion.Euler( new Vector3(0,0,320) );
			}

			groundCollider.SetActive (true);
			hitCollider.SetActive (true);
		}
	}

	void JumpDown(){
		float j = Random.Range(1,100);

		//Debug.Log("life="+life+" rand="+j+" jumps="+jumps+" maxj="+maxJumps);

		if( j > life && jumps <= maxJumps ){
			jumps++;
			state = States.JumpUp;
		}else{
			jumps = 1;
			state = States.Idle;
		}
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground"){
			anim.SetBool("jump",false);
			state = States.JumpDown;
			shadowAlpha = 0;
			shadow.GetComponent<SpriteRenderer>().color = new Color(0F,0F,0F,shadowAlpha);
			shadow.GetComponent<SpriteRenderer>().enabled = false;
			shadow.transform.rotation = Quaternion.identity;
			shadow.transform.parent = transform;
			airChange = false;
		}
	}
	
	private void SetShadowPosition(){
		shadowPosition.x = player.transform.position.x;
		shadowPosition.y = floorY;
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

	public void forcaCima(){
		rigidbody2D.AddForce( transform.up * jumpForce );
	}

	public void AtirarLanca(){
		float x = Direcao();
		float p = Random.Range (lancaForce - lancaForceRange, lancaForce + lancaForceRange);
		
		GameObject l = GameObject.Instantiate (lanca, atirar.transform.position, atirar.transform.rotation * Quaternion.Euler(new Vector3(0,0,-90))) as GameObject;
		l.rigidbody2D.AddForce (atirar.transform.right * p);
		l.GetComponent<Lanca> ().atk = lancaDmg;
		l.GetComponent<Lanca> ().rotateSpeed = spearRotateSpeed * x;

		cabutAnim.SetBool("atacar",false);
	}

	public void CuspirAcido(){
		GameObject s = GameObject.Instantiate (spit, mouth.position, mouth.rotation * Quaternion.Euler(new Vector3(0,0,180))) as GameObject;
		//s.rigidbody2D.AddForce (mouth.right * spitForce, ForceMode2D.Impulse);
		//s.GetComponent<AcidoControl>().speed = spitForce;
		s.GetComponent<AcidoControl>().atk = spitAtk;
		s.rigidbody2D.AddForce( mouth.right * 1000F );

		state = States.Idle;
	}
	
}
