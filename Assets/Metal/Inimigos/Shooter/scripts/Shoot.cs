using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public float speed;
	public float dmg;

	// Use this for initialization
	void Start () {
		GameObject.Destroy (gameObject, 8);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * speed * Time.deltaTime;
	}
}
