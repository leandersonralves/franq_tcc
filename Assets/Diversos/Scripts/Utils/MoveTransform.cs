using UnityEngine;
using System.Collections;

public class MoveTransform : MonoBehaviour {

	public Vector3 target;
	public float velocityMove = 1f;

	Transform m_transform;

	void Start () {
		m_transform = transform;
	}

	private Vector3 initialPosition;
	private float currentTime = 0f;
	void Move () {
		initialPosition = m_transform.position;
		StartCoroutine(Moving());
	}

	IEnumerator Moving () {
		while(Vector3.Distance(target, m_transform.position) > 0.0005f) {
			m_transform.position = Vector3.Lerp(initialPosition, target, currentTime);
			currentTime += Time.deltaTime * velocityMove;
			yield return null;
		}
	}
}
