using UnityEngine;
using System.Collections;

public class LoopRotation : MonoBehaviour {

	public float step = 60f;
	public float velocity = 250f;
	Transform m_transform;

	void Start () {
		m_transform = transform;
	}
	float rotZ = 0f;
	void Update () {
		rotZ -= (Time.deltaTime * velocity);
		m_transform.eulerAngles = new Vector3(0, 0, rotZ - (rotZ%step));
	}
}
