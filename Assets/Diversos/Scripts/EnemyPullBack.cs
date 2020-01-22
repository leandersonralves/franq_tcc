using UnityEngine;
using System.Collections;

public class EnemyPullBack : MonoBehaviour {

	public float pullForce;

	private GameObject player;
	private Vector3 targetDir;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D obj)
	{
		if (obj.collider.gameObject.tag == "Player") {
			Debug.Log("player");

			if( player.transform.position.y >= transform.position.y ){
				targetDir = player.transform.position - transform.position;
				player.rigidbody2D.AddForce(targetDir.normalized * pullForce);
			}
		}
	}

}
