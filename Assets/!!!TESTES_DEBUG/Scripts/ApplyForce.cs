using UnityEngine;
using System.Collections;

public class ApplyForce : MonoBehaviour {

	public bool applyForce = true;

	public Vector3 directionForce;
	public float force;
	Rigidbody2D m_rigidbody;

	void Start () {
		m_rigidbody = rigidbody2D;
	}

	void Update () {
		if(m_rigidbody == null)
			return;

		if(applyForce)
		{
			applyForce = false;
			m_rigidbody.AddForce(directionForce * force);
		}
	}
}
