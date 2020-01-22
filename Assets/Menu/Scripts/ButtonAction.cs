using UnityEngine;
using System.Collections;

public enum Action {
	LoadLevel,
	Exit
}

public class ButtonAction : MonoBehaviour {

	public Transform shine;
	public Transform handFranq;
	public Action action;
	public string loadLevel;

	void OnMouseUpAsButton ()
	{
		if(string.IsNullOrEmpty(loadLevel) && action == Action.LoadLevel)
			return;

		Pressed();
	}

	void Act ()
	{
		switch(action)
		{
		case Action.LoadLevel:
			SceneManager.LoadLevel(loadLevel);
//			Application.LoadLevel(loadLevel);
			break;
			
		case Action.Exit:
			Application.Quit();
			break;
		}
	}

	public void Pressed ()
	{
		StartCoroutine(PressedCoroutine());
	}

	IEnumerator PressedCoroutine ()
	{
		if(handFranq != null)
		{
			float t = 0f;
			Color c = shine.renderer.material.color;
			Vector3 p = handFranq.position;

			while(c.a > 0.01)
			{
				c.a -= Time.deltaTime * 2f;
				shine.renderer.material.color = c;

				p.x -= Time.deltaTime * 0.5f;
				p.y -= Time.deltaTime * 0.5f;

				handFranq.position = p;

				yield return null;
			}
		}
		Act();
	}

	void OnMouseEnter ()
	{
		Over();
	}

	public void Over ()
	{
		if(shine != null)
			shine.position = transform.position;

		if(handFranq == null)
			return;

		Vector3 p = transform.position;
		p.z = handFranq.position.z;
		handFranq.position = p;
	}
}
