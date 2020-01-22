using UnityEngine;
using System.Collections;

public class MagnumploxControl : MonoBehaviour {

	private GameObject target;
	private float distance;

	public float punchAtk;

	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Atacar(){
		distance = Vector3.Distance(target.transform.position, transform.position);
		
		if(distance <= 10F && !Skills.inDefense){
			target.GetComponent<Life>().Health -= punchAtk;
		}
	}

	public void Morrer(){
		GameObject.Destroy(GetComponent<BoxCollider2D>());
		GameObject.Destroy(rigidbody2D);
		GameObject.Destroy(this);
	}
}
