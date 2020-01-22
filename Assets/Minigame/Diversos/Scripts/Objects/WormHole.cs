using UnityEngine;
using System.Collections;

public class WormHole : MonoBehaviour
{

	public Transform destination;

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerEnter2D( Collider2D obj ) {
		if( obj.CompareTag("Player")){
			obj.gameObject.transform.position = destination.position;
		}
	}

}

