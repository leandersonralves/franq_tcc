using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	public Animator m_animator;
	public bool takeOn = true;

	void Start ()
	{
		m_animator = GetComponentInChildren<Animator>();
		if(takeOn)
			m_animator.SetTrigger("TakeOn");
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		if(hit.CompareTag("Player") && hit.GetComponent<Items>().itemObjective)
			m_animator.SetTrigger("TakeOff");
	}
}
