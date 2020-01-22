using UnityEngine;
using System.Collections;

public class PlayAnimationTrigger : MonoBehaviour {

	public string nameSetTrigger;
	public float interval = 4f;

	Animator m_animator;

	IEnumerator Start () {
		while(GetComponent<Animator>() == null)
			yield return null;

		m_animator = GetComponent<Animator>();
		StartCoroutine(SetTrigger());
	}

	IEnumerator SetTrigger () {
		yield return new WaitForSeconds(interval);

		m_animator.SetTrigger(nameSetTrigger);
		StartCoroutine(SetTrigger());
	}
}
