using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour
{

	public float pullForce;

	private Vector3 targetDir;
	private GameObject player;
	private float deathTime = 10F;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update ()
	{
		if(deathTime <= 0F){
			player.GetComponentInChildren<NaveControl>().Explodir();
			GameObject.Destroy(this);
		}
	}

	void OnTriggerStay2D( Collider2D obj ) {
		if( obj.CompareTag("Player")){
			GameObject player = obj.gameObject;

			targetDir = transform.position - player.transform.position;
			player.rigidbody2D.AddForce(targetDir.normalized * pullForce);

			deathTime -= Time.deltaTime;
		}
	}

	void OnTriggerExit2D( Collider2D obj ) {
		if( obj.CompareTag("Player")){
			deathTime = 5F;
		}
	}

}

