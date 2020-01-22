using UnityEngine;
using System.Collections;

public class ActiveMeteor : MonoBehaviour
{
	public GameObject[] meteoros;
	public bool burst;
	public float speed;

	void OnTriggerEnter2D( Collider2D obj ) {
		if( obj.CompareTag("Player")){

			foreach(GameObject meteoro in meteoros){
				if( burst ) meteoro.GetComponent<MeteorControl>().burst = true;
				meteoro.GetComponent<MeteorControl>().speed = speed;
			}

			GameObject.Destroy(gameObject);
		}
	}
}

