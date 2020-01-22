using UnityEngine;
using System.Collections;

public class TriggerCheckpoint : CheckPoint {

	GameObject robo;

	public Vector3 forcePosRobot = Vector3.zero;
	public int sequenceCheckPoint;

	public bool followRobot = false;
	bool passed = false;

	void Awake (){}

	IEnumerator Start ()
	{
		if(!checkpoints.ContainsKey(sequenceCheckPoint))
			checkpoints.Add(sequenceCheckPoint, this);
		else
			checkpoints[sequenceCheckPoint] = this;

		while(GameObject.FindGameObjectWithTag("Robo") == null)
			yield return null;

		robo = GameObject.FindGameObjectWithTag("Robo");
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		if(passed && this == lastCheckPoint) return;

		if(hit.CompareTag("Player"))
		{
			hit.rigidbody2D.velocity = Vector3.zero;
			passed = true;
			lastCheckPoint = this;

			if(followRobot && robo != null)
				robo.SendMessage("ToCheckpoint", this);
		}
	}
}
