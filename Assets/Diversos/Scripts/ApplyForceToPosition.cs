using UnityEngine;
using System.Collections;

public class ApplyForceToPosition : MonoBehaviour {

	public TimeToApply applyForce = TimeToApply.ReachPositionInitial;
	public enum TimeToApply {
		UntilTarget,
		ReachPositionInitial
	}

	public bool stopReachTarget = true;

	public Vector3 targetPosition;
	public float force;
	public float distanceToAct;

	Rigidbody2D m_rigidbody;
	Transform m_transform;

	Vector3 initialPosition;
	bool inStopping = false;

	void Start () {
		m_rigidbody = rigidbody2D;
		m_transform = transform;
		initialPosition = m_transform.position;
	}

	void Update () {
		if(m_rigidbody == null)
			return;

		switch(applyForce)
		{
		case TimeToApply.ReachPositionInitial:
			if(Vector3.Distance(m_transform.position, initialPosition) < distanceToAct)
				m_rigidbody.AddForce((targetPosition - m_transform.position).normalized * force, ForceMode2D.Impulse);
			break;

		case TimeToApply.UntilTarget:
			if(Vector3.Distance(m_transform.position, targetPosition) > distanceToAct)
				m_rigidbody.AddForce((targetPosition - m_transform.position).normalized * force, ForceMode2D.Impulse);
			break;
		}

		if(!inStopping && stopReachTarget && Vector3.Distance(m_transform.position, targetPosition) < 0.05f)
		{
			m_rigidbody.velocity = Vector3.zero;
			StartCoroutine(InStopping());
		}
	}

	IEnumerator InStopping ()
	{
		inStopping = true;
		while(Vector3.Distance(m_transform.position, targetPosition) < 0.1f)
			yield return null;

		inStopping = false;
	}
}
