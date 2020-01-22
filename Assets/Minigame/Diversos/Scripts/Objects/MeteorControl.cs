using UnityEngine;
using System.Collections;

public class MeteorControl : MonoBehaviour
{

	public GameObject direcao;
	public float speed;
	public float lifetime;
	public bool burst;

	private bool destroy;

	// Use this for initialization
	void Start ()
	{
		gameObject.rigidbody2D.mass = 10F;
	}

	// Update is called once per frame
	void Update ()
	{
		if( speed != 0 ){
			if( !destroy && lifetime > 0 ){
				GameObject.Destroy (gameObject, lifetime);
				destroy = true;
			}

			if( burst ){
				gameObject.rigidbody2D.AddForce(direcao.transform.right * speed);
			}else{
				gameObject.rigidbody2D.velocity = direcao.transform.right * speed;
			}
		}
	}

}

