using UnityEngine;
using System.Collections;

public class OctopusController : MonoBehaviour {

	public GameObject player;
	public GameObject cam;
	public float speed;

	public GameObject olho;
	public GameObject tentaculoL;
	public GameObject tentaculoR;
	public GameObject tentaculos;

	private Animator animOlho;
	private Animator animTL;
	private Animator animTR;
	private Animator animTentaculos;

	private bool emCena = false;

	private bool ataqueTentaculoL = false;
	private bool ataqueTentaculoR = false;
	private bool ataqueTentaculos = false;

	public float cooldown;
	private float timer;

	// Use this for initialization
	void Start () {
		SceneManager.OnDie += ResetPosition;

		animOlho = olho.GetComponent<Animator>();
		animTL = tentaculoL.GetComponent<Animator>();
		animTR = tentaculoR.GetComponent<Animator>();
		animTentaculos = tentaculos.GetComponent<Animator>();

		timer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		SetPosition ();

		if(emCena){

			if(timer > 0F){
				timer -= Time.deltaTime;
			}else{
				// ataca caso n esteja executando outra açao
				if(!ataqueTentaculoL && !ataqueTentaculoR && !ataqueTentaculos){

					timer = cooldown;

					int r = (int) Random.Range(0,30);

					if(r <= 5){ // ataque L
						AtaqueL();
					}else if(r > 5 && r <= 10){ // ataque R
						AtaqueR();
					}else{ // puxar
						AtaqueT();
					}

				}

			}

		}else{
			timer = cooldown;
		}
	}

	private void SetPosition(){
		float step = speed * Time.deltaTime;
		Vector3 target = new Vector3 ( transform.position.x + speed , cam.transform.position.y, transform.position.z );
		transform.position = target;
	}

	public void ResetPosition(){
		transform.position = new Vector3 ( player.transform.position.x - 40F , transform.position.y, transform.position.z );
	}

	void OnTriggerEnter2D( Collider2D obj ) {
		if(obj.CompareTag("Player")){
			AtaqueL();
			AtaqueR();
			AtaqueT();
		}
	}

	// Funçoes para animaçao
	public void AtaqueL(){
		ataqueTentaculoL = true;
		animTL.SetTrigger("Atacar");
	}

	public void FimAtaqueL(){
		ataqueTentaculoL = false;
	}

	public void AtaqueR(){
		ataqueTentaculoR = true;
		animTR.SetTrigger("Atacar");
	}
	
	public void FimAtaqueR(){
		ataqueTentaculoR = false;
	}

	public void AtaqueT(){
		ataqueTentaculos = true;
		animTentaculos.SetTrigger("Atacar");
	}
	
	public void FimAtaqueT(){
		ataqueTentaculos = false;
	}

	public void Entrar(){
		if(!emCena) animOlho.SetTrigger("Entrar");
	}

	public void Entrando(){
		emCena = true;
	}

	public void Sair(){
		if(emCena) animOlho.SetTrigger("Sair");
	}
	
	public void Saindo(){
		emCena = false;
	}
}
