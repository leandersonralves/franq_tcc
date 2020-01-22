using UnityEngine;
using System.Collections;

public class ReplyTransform : MonoBehaviour {

	Transform m_transform;
	public Transform source;
	public bool replyRotation = false;

	void Start () {
		m_transform = transform;
	}

	void Update () {
		if(source == null)
			return;

		m_transform.position = new Vector3(
				source.position.x,
				source.position.z, 
				source.position.y
			);

		if(replyRotation)
		{
			m_transform.eulerAngles = new Vector3(
				source.eulerAngles.x,
				source.eulerAngles.z, 
				(1f - (source.localScale.x * .5f + .5f)) * 180f
			);
		}
	}
}
