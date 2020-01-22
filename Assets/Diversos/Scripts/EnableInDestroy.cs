using UnityEngine;
using System.Collections;

public class EnableInDestroy : MonoBehaviour {

	public GameObject[] itensToRelease;
	public bool positionOfDestroyed = true;
	public Vector3 positionToRelease;

	void Start ()
	{
		for(int i = 0; i < itensToRelease.Length; i++)
			itensToRelease[i].SetActive(false);
	}

	void OnDestroy ()
	{
		for(int i = 0; i < itensToRelease.Length; i++)
		{
			if(itensToRelease[i] != null)
				itensToRelease[i].SetActive(true);
		}

		if(positionOfDestroyed)
			return;

		for(int i = 0; i < itensToRelease.Length; i++)
		{
			if(itensToRelease[i] != null)
				itensToRelease[i].transform.position = positionToRelease;
		}
	}
}
