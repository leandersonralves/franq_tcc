using UnityEngine;
using System.Collections;

public class ParticleSystemLayer : MonoBehaviour
{
	public string layer;
	void Start (){
		GetComponent<ParticleRenderer>().renderer.sortingLayerName = layer;
	}
}