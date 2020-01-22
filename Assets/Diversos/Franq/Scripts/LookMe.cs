using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookMe : MonoBehaviour {

	static Transform player;
	private bool isMe = false;

	private static List<Transform> lookMe = new List<Transform>();
	protected static LookMe target = null;
	protected Transform m_transform;
	
	public float rangeToLook = 10f;

	public bool isActive = true;
	private bool readyLook = false;

	IEnumerator Start () {
		while(Singleton.player == null)
			yield return null;

		player = Singleton.player.transform;
		m_transform = transform;
		lookMe.Add(m_transform);
		readyLook = true;
	}

	void Update () {
		if(!isActive || !readyLook || (target != null && target.Equals(m_transform)))
			return;

		if(DistanceOfPlayer < rangeToLook && FrontPlayer) {
			if ((target != null && DistanceOfPlayer < target.DistanceOfPlayer)) {
				target.isMe = false;
				
				target = this;
				isMe = true;
			}
			else if (target == null) {
				target = this;
				isMe = true;
			}
		}
		else if (isMe) {
			target = null;
			isMe = false;
		}
	}

	public void Deactived () {
		isActive = false;
		target = null;
	}

	public float DistanceOfPlayer {
		get {
			if(player == null || m_transform == null)
				return 0f;
			else
				return Vector3.Distance(m_transform.position, player.position);
		}
	}

	private bool FrontPlayer {
		get {
			float directions = Vector3.Dot((m_transform.position - player.position).normalized, Vector3.right);

			if((directions >= 0.1f && player.IsRight()) || 
			   (directions <= -0.1f && !player.IsRight()))
				return true;
			else
				return false;
		}
	}

	public Vector3 Position {
		get {
			if(m_transform == null)
				return transform.position;
			else
				return m_transform.position;
		}
	}

//	void OnDrawGizmos () {
//		if(gameObject.name != "eye")
//			Gizmos.DrawWireSphere(transform.position, rangeToLook);
//	}
}
