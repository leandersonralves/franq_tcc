using UnityEngine;
using System.Collections;

public class PullPlataform : MonoBehaviour {

	private Vector3 start;
	private Vector3 end;
	private Vector3 target;

	public bool move = false;

	public float speed = 2F;
	public float moveDelta;

	// Use this for initialization
	void Start () {
		start = transform.position;
		end = new Vector3(start.x, start.y - moveDelta, start.z);

		target = end;
	}
	
	// Update is called once per frame
	void Update () {
		if(move){
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target, step);

			if(transform.position.y == end.y){
				target = start;
			}

			if(transform.position.y == start.y){
				move = false;
				target = end;
			}
		}
	}
}
