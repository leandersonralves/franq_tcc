using UnityEngine;
using System.Collections;

public class ChangeParent : MonoBehaviour {

	public string tagColliderTarget = "Player";

	void OnCollisionEnter2D (Collision2D hit)
	{
		if(hit.collider.CompareTag(tagColliderTarget))
			hit.transform.parent = transform;
	}

	void OnCollisionExit2D (Collision2D hit)
	{
		if(hit.collider.CompareTag(tagColliderTarget))
			hit.transform.parent = null;
	}
}
