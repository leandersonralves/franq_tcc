using UnityEngine;
using System.Collections;

public class NavigationAgent : MonoBehaviour {

	public TargetType targetType = TargetType.Player;
	public enum TargetType {
		Player,
		CheckPoint,
		Item
	}


	NavMeshAgent agent;

	public Transform target;

	public Vector3 deltaFollowPlayer;
	public Vector3 deltaCheckpoint;
	public Vector3 deltaItem;

	Vector3 delta;

	void Start () {
		float z = deltaCheckpoint.z;
		deltaCheckpoint.z = deltaCheckpoint.y;
		deltaCheckpoint.y = z;

		z = deltaFollowPlayer.z;
		deltaFollowPlayer.z = deltaFollowPlayer.y;
		deltaFollowPlayer.y = z;

		z = deltaItem.z;
		deltaItem.z = deltaItem.y;
		deltaItem.y = z;

		agent = GetComponent<NavMeshAgent>();
		delta = deltaFollowPlayer;
		agent.SetDestination(target.TransformPoint(delta));
	}

	void Update () {
		if(target == null)
			return;

		if(target.IsRight() && (Mathf.Sign(delta.x) == 1f))
			delta.x = delta.x * -1f;
		else if (!target.IsRight() && (Mathf.Sign(delta.x) == -1f))
			delta.x = -delta.x * -1f;

		agent.SetDestination(target.TransformPoint(delta));
	}

	public void Stop ()
	{
		agent.Stop();
	}

	public void SetTarget (Transform targetToRobo, TargetType targetType) {
		target.GetComponent<ReplyTransform>().source = targetToRobo;
		switch(targetType)
		{
		case TargetType.CheckPoint:
			delta = deltaCheckpoint;
			break;
		case TargetType.Player:
			delta = deltaFollowPlayer;
			break;
		case TargetType.Item:
			delta = deltaItem;
			break;
		}
	}

	public void ForcePosition (Vector3 position)
	{
		agent.Warp(new Vector3(position.x, position.z, position.y));
	}

	public bool IsNear {
		get {
			if (Vector3.Distance(transform.position, target.TransformPoint(delta)) < 0.3f)
			{
				agent.Warp(target.TransformPoint(delta));
				return true;
			}
			else
				return false;
		}
	}
}
