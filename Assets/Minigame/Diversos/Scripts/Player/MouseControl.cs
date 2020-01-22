using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour
{
	public Transform handFranq;
	
	void Start()
	{
		Screen.showCursor = false;
	}

	void Update ()
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = handFranq.position.z;
		handFranq.transform.position = pos;
	}
}

