using UnityEngine;
using System.Collections;

public class ControlEye : LookMe {
	bool isInitialPosition = false;
	public Transform centerPivot;
	Vector3 initialPosition;

	public float radius = 0.7f;

	Transform player;

	private bool ready = false;

	void Awake ()
	{
		m_transform = transform;
		initialPosition = m_transform.localPosition;
		centerPivot = m_transform.parent;
		radius = Vector3.Distance(centerPivot.position, m_transform.position);
	}

	IEnumerator Start ()
	{
		while(Singleton.player == null)
			yield return null;

		player = Singleton.player.transform;

		ready = true;
	}

	void Update ()
	{
		if(!ready) return;

		if(target == null) {
			if(!isInitialPosition) {
				m_transform.position = centerPivot.position + centerPivot.right * (radius * Mathf.Sign(centerPivot.parent.localScale.x));//Vector3.Lerp(m_transform.localPosition, initialPosition, Time.deltaTime * 5f);

				if(Vector3.Distance(m_transform.localPosition, initialPosition) <= 0.0001f)
					isInitialPosition = true;
			}
		}
		else
		{
			if(isInitialPosition)
				isInitialPosition = false;

			Vector3 direction = (target.Position - centerPivot.position).normalized;

			m_transform.position = Vector3.Lerp(m_transform.position, (centerPivot.position + direction * radius), Time.deltaTime * 5f);
//			m_transform.position = centerPivot.position + direction * radius;
		}
	}
}
