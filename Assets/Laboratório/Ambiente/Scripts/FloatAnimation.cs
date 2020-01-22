using UnityEngine;
using System.Collections;

public class FloatAnimation : MonoBehaviour {

	public Vector3 positionA = Vector3.zero;
	public Vector3 positionB = Vector3.zero;
	public float velocity = 1f;

	private float randomInitial;
	private Transform m_transform;

	void Start () {
		m_transform = transform;
		randomInitial = Random.Range(0f, 10f);
	}

	void Update () {
		float interpolation = (Mathf.Cos((Time.timeSinceLevelLoad + randomInitial) * velocity) * .5f) + .5f;
		m_transform.localPosition = 
			Vector3.Lerp(
				positionA, 
				positionB,
				interpolation
			);
	}
}
