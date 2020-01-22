using UnityEngine;
using System.Collections;

public class ApplyForceTime : MonoBehaviour {

	public Vector3 positionTarget;
	public float timeToApply;
	public float force;

	float currentTime = 0f;

	Rigidbody2D m_rigidbody;
	Transform m_transform;

	void Start () {
		m_rigidbody = rigidbody2D;
		m_transform = transform;
	}

	void Update () {
		if(m_rigidbody == null)
			return;

		currentTime += Time.deltaTime;

		if(currentTime >= timeToApply)
		{
			currentTime = 0f;
			m_rigidbody.AddForce((positionTarget - m_transform.position).normalized * force);
		}
	}
}
