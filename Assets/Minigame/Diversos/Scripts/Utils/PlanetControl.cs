using UnityEngine;
using System.Collections;

public class PlanetControl : MonoBehaviour
{

	public GameObject particles;

	void OnMouseOver() {
		Debug.Log("over");
		//particles.SetActive(true);
		//transform.localScale = new Vector3(maxScale,maxScale,maxScale);
	}

	void OnMouseExit() {
		Debug.Log("out");
		//particles.SetActive(false);
		//transform.localScale = new Vector3(1,1,1);
	}
}

