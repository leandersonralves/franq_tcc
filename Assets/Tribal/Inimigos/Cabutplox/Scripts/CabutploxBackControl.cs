using UnityEngine;
using System.Collections;

public class CabutploxBackControl : MonoBehaviour
{

	private Animator anim;

	private Vector3 shotPosition;
	private Vector3 shotRotation;

	public GameObject lanca;
	public float shootAtk;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();

		shotPosition = new Vector3( transform.position.x, transform.position.y + 4, 0 );
		shotRotation = new Vector3( 0, 0, 180 );
	}

	public void Atacar(){
		anim.SetBool("atirar", true);
	}

	public void Atirar(){
		GameObject l = GameObject.Instantiate( lanca, shotPosition, Quaternion.Euler(shotRotation) ) as GameObject;
		l.GetComponent<Lanca> ().atk = shootAtk;
		l.GetComponent<Lanca> ().rotateSpeed = 0;

		anim.SetBool("atirar", false);
	}

	public void SetShotX( float x ){
		shotPosition.x = x;
	}

}

