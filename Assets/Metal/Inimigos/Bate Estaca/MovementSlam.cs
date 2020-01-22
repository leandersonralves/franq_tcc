using UnityEngine;
using System.Collections;

public class MovementSlam : MonoBehaviour {

	public float delayInitial = 1.5f;
	public float delayDown = 1.5f;
	public float delayUp = 1.5f;

	Transform m_transform;
	Animator m_animator;

	public bool act = false;

	IEnumerator Start ()
	{
		m_transform = transform;
		m_animator = GetComponentInChildren<Animator>();

		while (!act)
			yield return null;

		yield return new WaitForSeconds(delayInitial);
		StartCoroutine(TimeDown());
	}

	IEnumerator TimeDown () {
		yield return new WaitForSeconds(delayDown);
		m_animator.SetTrigger("Down");

		yield return new WaitForSeconds(delayUp);
		m_animator.SetTrigger("Up");
		StartCoroutine(TimeDown());
	}
}
