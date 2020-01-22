using UnityEngine;
using System.Collections;

public class SetLayerRender : MonoBehaviour {

	public string layerName;
	public int layerID = int.MaxValue;

	void Start () {
		if(!string.IsNullOrEmpty(layerName))
			renderer.sortingLayerName = layerName;

		if(layerID != int.MaxValue)
			renderer.sortingLayerID = layerID;
	}
}
