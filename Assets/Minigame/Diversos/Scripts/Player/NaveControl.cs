using UnityEngine;
using System.Collections;

public class NaveControl : MonoBehaviour
{
		public bool guiON;

	#region combustivel
	public static bool infiniteFuel = false;
		private float fuel = 100F;
		public float Fuel {
				get {
						return fuel;
				}
				set {
						if (value > 100F)
								fuel = 100F;
						else if (value < 0F)
								fuel = 0F;
						else
								fuel = value;
				}
		}
		public float fuelFrontConsume;
		public float fuelBackConsume;
	#endregion

	#region life
		private int life = 3;
		public static bool infiniteLife = false;
		public int Life {
				get {
						return life;
				}
				set {
						if (value > 3)
								life = 3;
						else if (value < 0)
								life = 0;
						else
								life = value;
				}
		}
	#endregion

	#region Rotation
		public float rotateSpeed;
		public float rotateLimit;
		public float rotateDelay;

		private float Rd;
		private float Rl;
		private float Ax;
	#endregion

	#region Propulsion
		public float pushForce;
		public float pushForceReverse;

		public GameObject nave;
		public GameObject direction;
		public GameObject fire;
		public GameObject retro_fire;
	#endregion

	#region Movement
		public float minSpeed;
		public float maxSpeed;
		public float mass;

		private float currentSpeed;
	#endregion

	#region Proximity
		public GameObject janelaAzul;
		public GameObject janelaVermelha;
	#endregion

	#region Gameplay
		public GameObject morte;
		public GameObject meteorboom;
		public GameObject cam;
		public GameObject nave_sprite;
		public GameObject prop_sprite;
		public bool isActive;
		private bool dead;
		private float deadTime;
		public GameObject exp1;
		public GameObject exp2;
	#endregion

		// Use this for initialization
		void Start ()
		{
				deadTime = 6F;
				isActive = false;
				mass = 1F;
				Rd = rotateDelay;
		}

		// Update is called once per frame
		void Update ()
		{
				if (isActive && !dead) {
						RotationControl ();
						PropulsionControl ();
				}

				DeadControl ();
		}

		// Para Fisica
		void FixedUpdate ()
		{
				MovementControl ();
				MassControl ();
		}

		private void DeadControl ()
		{
				if (dead) {
						deadTime -= Time.deltaTime;
						if (deadTime <= 0)
								Application.LoadLevel (Application.loadedLevel);
				}
		}

		private void MovementControl ()
		{
				if (nave.rigidbody2D.velocity.magnitude < minSpeed)
						nave.rigidbody2D.velocity = nave.rigidbody2D.velocity.normalized * minSpeed;
				if (nave.rigidbody2D.velocity.magnitude > maxSpeed)
						nave.rigidbody2D.velocity = nave.rigidbody2D.velocity.normalized * maxSpeed;

				currentSpeed = nave.rigidbody2D.velocity.magnitude;
		}

		private void MassControl ()
		{
				nave.rigidbody2D.mass = mass;
		}

		private void PropulsionControl ()
		{
			if (fuel > 0F || infiniteFuel) {
				
				if (Input.GetKey(KeyCode.Space)) {
					if( !fire.GetComponent<AudioSource>().isPlaying ) fire.GetComponent<AudioSource>().Play();
					fire.particleSystem.Play ();
					nave.rigidbody2D.AddForce (direction.transform.right * pushForce);
					if(!infiniteFuel)
						fuel -= fuelFrontConsume;
				}
				if (Input.GetKeyUp(KeyCode.Space)) {
					fire.GetComponent<AudioSource>().Stop();
				}

				/*if( Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) ){
					retro_fire.particleSystem.Play();
					nave.rigidbody2D.AddForce( -direction.transform.right * pushForceReverse );
					fuel -= fuelBackConsume;
				}*/

				if (fuel < 0F && !infiniteLife){
					fuel = 0F;
					if( fire.GetComponent<AudioSource>().isPlaying ) fire.GetComponent<AudioSource>().Stop();
				}

			}

		}

		private void RotationControl ()
		{
				Ax = Input.GetAxis ("Vertical") * -1f;
				Rl = rotateLimit;

				if (Ax != 0) {
						Rd = rotateDelay;

						Vector3 rc = transform.rotation.eulerAngles;			
						if (rc.z > 180f)
								rc.z -= 360f; 			
						rc.z += rotateSpeed * Time.deltaTime * Ax;			
						rc.z = Mathf.Clamp (rc.z, -Rl, Rl);
						transform.rotation = Quaternion.Euler (rc);

				} else {
						if (Rd > 0)
								Rd -= Time.deltaTime;
						else
								transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.identity, Time.deltaTime);
				}
		}

		void OnTriggerEnter2D (Collider2D obj)
		{
				if (obj.CompareTag ("Meteoro") && !dead) {
						GetComponent<AudioSource>().Play();
						janelaVermelha.SetActive (true);
						janelaAzul.SetActive (false);
				}
		}

		void OnTriggerExit2D (Collider2D obj)
		{	
				if (obj.CompareTag ("Meteoro") && !dead) {
						GetComponent<AudioSource>().Stop();
						janelaVermelha.SetActive (false);
						janelaAzul.SetActive (true);
				}
		}

		void OnCollisionEnter2D (Collision2D obj)
		{
				if (obj.collider.CompareTag ("Meteoro")) {
						exp1.GetComponent<AudioSource> ().Play ();
						GameObject b = GameObject.Instantiate (meteorboom, nave.transform.position, nave.transform.rotation) as GameObject;
						GameObject.Destroy (obj.gameObject);
						
						if(infiniteLife)
							return;
					
						if (life > 1)
							life -= 1;
						else
							Explodir ();
				}
		}

		public void Explodir ()
		{
				life = 0;
				GetComponent<AudioSource>().Stop();
				exp2.GetComponent<AudioSource> ().Play ();
				cam.GetComponent<MoveCameraMiniGame> ().isActive = false;
				isActive = false;
				dead = true;
				GameObject.Instantiate (morte, nave.transform.position, nave.transform.rotation);
				nave_sprite.SetActive (false);
				prop_sprite.SetActive (false);
				gameObject.GetComponent<CircleCollider2D> ().enabled = false;
		}

		void OnGUI ()
		{
				if (guiON) {

						GUI.Label (new Rect (25, 10, 300, 20), "Velocidade da Rotacao (" + rotateSpeed + ")");
						rotateSpeed = GUI.HorizontalSlider (new Rect (25, 30, 100, 30), rotateSpeed, 1F, 100F);
						GUI.Label (new Rect (25, 60, 300, 20), "Forca da Propulsao (" + pushForce + ")");
						pushForce = GUI.HorizontalSlider (new Rect (25, 80, 100, 30), pushForce, 1F, 100F);
						GUI.Label (new Rect (25, 110, 300, 20), "Velocidade Minima (" + minSpeed + ")");
						minSpeed = GUI.HorizontalSlider (new Rect (25, 130, 100, 30), minSpeed, 1F, 100F);
						GUI.Label (new Rect (25, 160, 300, 20), "Velocidade Maxima (" + maxSpeed + ")");
						maxSpeed = GUI.HorizontalSlider (new Rect (25, 180, 100, 30), maxSpeed, 1F, 100F);
						GUI.Label (new Rect (25, 210, 300, 20), "Massa da Nave (" + mass + ")");
						mass = GUI.HorizontalSlider (new Rect (25, 230, 100, 30), mass, 0.1F, 10F);
						GUI.Label (new Rect (25, 260, 300, 20), "Limite de Rotacao (" + rotateLimit + " graus)");
						rotateLimit = GUI.HorizontalSlider (new Rect (25, 280, 100, 30), rotateLimit, 0F, 180F);
						GUI.Label (new Rect (25, 360, 300, 20), "Tempo de Retorno do Propulsor (" + rotateDelay + "s)");
						rotateDelay = GUI.HorizontalSlider (new Rect (25, 380, 100, 30), rotateDelay, 1F, 10F);
						GUI.Label (new Rect (25, 410, 300, 20), "Forca de Propulsao Reversa (" + pushForceReverse + ")");
						pushForceReverse = GUI.HorizontalSlider (new Rect (25, 430, 100, 30), pushForceReverse, 10F, 1000F);

						GUI.Label (new Rect (25, 480, 300, 20), "Velocidade Atual (" + currentSpeed + ")");
						GUI.Label (new Rect (25, 500, 300, 20), "Aperte A para proxima tela");

				}
		}

}

