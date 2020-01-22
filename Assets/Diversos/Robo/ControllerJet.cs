using UnityEngine;
using System.Collections;

public class ControllerJet : MonoBehaviour {

	ParticleSystem particle;
	Robo roboController;

	float emissionIdle = 5f;
	float emissionWalking = 8f;

	void Start ()
	{
		roboController = transform.parent.GetComponent<Robo>();

		particle = GetComponentInChildren<ParticleSystem>();

		particle.renderer.sortingLayerName = "ForeGround";
		particle.renderer.sortingOrder = 1;
	}

	void Update ()
	{
		switch(roboController.state)
		{
		case Robo.RobotState.Idle:
		case Robo.RobotState.Talking:
			if(particle.emissionRate != emissionIdle)
				particle.emissionRate = emissionIdle;
			break;

		case Robo.RobotState.FollowFranq:
			if(particle.emissionRate != emissionWalking)
				particle.emissionRate = emissionWalking;
			break;

		case Robo.RobotState.Checkpoint:
			particle.emissionRate = 0f;
			break;

		default:
			if(particle.emissionRate != emissionIdle)
				particle.emissionRate = emissionIdle;
			break;
		}
	}
}
