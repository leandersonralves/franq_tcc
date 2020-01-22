using UnityEngine;
using System.Collections;

public class Lanca : MonoBehaviour {
	
	private GameObject player;

	public float rotateSpeed;
	private bool canRotate;

	public float atk;

	private Vector3 impacto;
	private Quaternion rotacao;
	public float fallTime;
	private float fall;

	private bool hit;

	public float destroy;

	bool catchFranq = false;

	// Use this for initialization
	void Start () {
		GameObject.Destroy (gameObject,destroy);
		fall = 0F;
		impacto = new Vector3 (-0.03F, 0.98F, 0F);
		canRotate = true;
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(hit){ // bateu
			if(catchFranq){
				if(fall >= 0F){
					fall -= Time.deltaTime;
					transform.position = player.transform.TransformPoint(impacto);
				}else{
					//if( gameObject.GetComponent<Rigidbody2D>() == null ) gameObject.AddComponent<Rigidbody2D>();
					GameObject.Destroy(gameObject);
				}
			}
		}else{ // n bateu
			if (canRotate) {
				rigidbody2D.AddTorque(rotateSpeed * Time.deltaTime);
				rotacao = transform.rotation;
			} 
		}

	}

	void OnCollisionEnter2D(Collision2D obj){
		if(!hit){
			if (obj.gameObject.tag == "Player") {
				catchFranq = true;
				GetComponent<Dano>().Ataque(atk);
			}

			hit = true;
			canRotate = false;

			fall = fallTime;

			rigidbody2D.velocity = Vector2.zero;

			gameObject.layer = 11; //ignorefranq
			GameObject.Destroy (gameObject.GetComponent<Dano>());
			GameObject.Destroy (gameObject.rigidbody2D);

		}
		transform.rotation = rotacao;
	}
}
