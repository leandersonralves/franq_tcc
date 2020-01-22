using UnityEngine;
using System.Collections;

public class SlidesRotate : MonoBehaviour {

	public float velocity = 0.6f;

	public float[] degrees = new float[]{};

	Transform m_transform;
	int selected = 0;

	void Start ()
	{
		m_transform = transform;
	}

	void Next ()
	{
		if(selected < degrees.Length-1)
		{
			selected++;
			Select(degrees[selected]);
		}
	}

	void Previous ()
	{
		if(selected > 0)
		{
			selected--;
			Select(degrees[selected]);
		}
	}

	float selectDegree;
	bool inRotating = false;
	void Select (float degree)
	{
		selectDegree = degree;
		if(!inRotating)
			StartCoroutine("Rotate");
	}
	
	float velocityRef = 0f;
	IEnumerator Rotate ()
	{
		inRotating = true;

		while(Mathf.Abs(m_transform.eulerAngles.y - selectDegree) > 0.005f)
		{
			Vector3 currentAngle = m_transform.eulerAngles;
			currentAngle.y = Mathf.SmoothDampAngle(m_transform.eulerAngles.y, selectDegree, ref velocityRef, velocity);
			m_transform.eulerAngles = currentAngle;
			yield return null;
		}

		inRotating = false;
	}

	void Force (float degree)
	{
		Vector3 currentAngle = transform.eulerAngles;
		currentAngle.y = degree;
		transform.eulerAngles = currentAngle;
	}
}
