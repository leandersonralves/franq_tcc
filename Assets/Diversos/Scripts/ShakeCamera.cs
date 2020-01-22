using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour
{

	public GameObject camera;
	public int shakeLoops;
	public float shakeForce;
	public float shakeDuration;
	public bool destroy;
	public bool lockPlayer;

	public bool evento;
	public GameObject pedra;

	private Vector3 startPosition;
	private int counter;
	private float timer;

	// Use this for initialization
	void Start ()
	{
		timer = 0F;
		counter = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		if(counter > 0){

			if(timer > 0F){
				timer -= Time.deltaTime;
			}else{
				timer = shakeDuration;

				float quakeAmt = Random.value * shakeForce * 2 - shakeForce; 

				if(counter%2 == 0) 	camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + quakeAmt, camera.transform.position.z);
				else 				camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - quakeAmt, camera.transform.position.z);
			}

			counter--;
			if( counter == 0 ){
				if(lockPlayer) MovePlayer.canMove = true;
				if(destroy) GameObject.Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D obj){
		if(obj.gameObject.tag == "Player"){
			StartShake();
		}
	}

	public void StartShake(){
		startPosition = camera.transform.position;
		counter = shakeLoops;

		if(lockPlayer) MovePlayer.canMove = false;
		
		if(evento){
			pedra.AddComponent<Rigidbody2D>();
			pedra.rigidbody2D.mass = 100F;
			pedra.rigidbody2D.fixedAngle = true;
		}
	}

}

