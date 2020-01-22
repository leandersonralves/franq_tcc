using UnityEngine;
using System.Collections;

public class PlayAnimationExplosion : MonoBehaviour {

	public float minimumDistance = 8f;
	public float forceExplosion = 100f;

	bool inPlaying = false;
	bool ready = false;

	IEnumerator Start () {
		while(Singleton.player == null)
			yield return null;

		ready = true;
	}

	void Update () {
		if(inPlaying || !ready)
			return;

		if(Vector3.Distance(transform.position, Singleton.player.transform.position) < minimumDistance)
			GetComponent<Animator>().SetTrigger("Boom");
	}

	void Push () {
		Vector3 direction = (Singleton.player.transform.position - transform.position).normalized;
		direction.z = direction.y = 0;
		Singleton.player.rigidbody2D.AddForce(direction * forceExplosion, ForceMode2D.Impulse);
	}
}
