using UnityEngine;
using System.Collections;

public class MultiMeteorControl : MonoBehaviour
{

	public float speed;
	private Component[] meteoros;

	// Use this for initialization
	void Start ()
	{	
		meteoros = GetComponentsInChildren<MeteorControl>();
		foreach(MeteorControl meteoro in meteoros){
			meteoro.speed = speed;
		}
	}

}

