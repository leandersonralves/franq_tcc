using UnityEngine;
using System.Collections;

public class ParticleSystemLayerMiniGame : MonoBehaviour
{

	public string layer;
	public bool defineID = false;
	public int id;

	// Use this for initialization
	void Start ()
	{
		particleSystem.renderer.sortingLayerName = layer;
		if (defineID)
			particleSystem.renderer.sortingLayerID = id;
	}
}

