using UnityEngine;
using System.Collections;

public class ActivateCollider : MonoBehaviour
{

	public bool destroyObjectHited = false;

	public string tagColliderContact = string.Empty;
	public string functionName = string.Empty;
	public GameObject objectReceiver;

	public bool once = true;
	bool sended = false;

	void Awake ()
	{
		if(string.IsNullOrEmpty (tagColliderContact) || string.IsNullOrEmpty (functionName) || objectReceiver == null)
			GameObject.Destroy (this);
	}

	void OnCollisionEnter2D (Collision2D hit)
	{
		if(collider2D.isTrigger)
			return;

		Send(hit.collider);
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		if(!collider2D.isTrigger)
			return;

		Send(hit);
	}

	void Send (Collider2D hit)
	{
		if(once && sended)
			return;
		
		if(hit.CompareTag(tagColliderContact))
		{
			sended = true;
			if(!string.IsNullOrEmpty(functionName))
				objectReceiver.SendMessage(functionName);
			if(destroyObjectHited)
				GameObject.Destroy(hit.gameObject);
		}
	}
}
