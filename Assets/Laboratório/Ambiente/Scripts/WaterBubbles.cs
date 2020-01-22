using UnityEngine;
using System.Collections;

public class WaterBubbles : MonoBehaviour {

	Transform player;
	Transform m_transform;
	public float distanceMinimum = 1.5f;
	public float force = 10f;

	bool ready = false;

	IEnumerator Start () {
		m_transform = transform;
		particleSystem.renderer.sortingLayerID = 3;

		while(Singleton.player == null)
			yield return null;

		player = Singleton.player.transform;
		ready = true;
	}

	void Update () {
		if(!ready)return;

		if(Vector3.Distance(m_transform.position, player.position) < distanceMinimum)
			player.rigidbody2D.AddForce((player.position - m_transform.position).normalized * force);
	}
}
