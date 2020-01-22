using UnityEngine;
using System.Collections;

public class SetStateGameObject : MonoBehaviour
{
	public GameObject gameObjectToControl;
	public bool hasInitialAnimation = true;

	public bool isEncubadora = false;

	void Start ()
	{
		if(isEncubadora)
		{
			MoveCamera.SetTarget(transform, 10f, 10f);
		}
	}

	public void SetState (int state)
	{
		if(state == 0)
			gameObjectToControl.SetActive(false);
		else if (hasInitialAnimation)
			gameObjectToControl.SetActive(true);
	}
}
