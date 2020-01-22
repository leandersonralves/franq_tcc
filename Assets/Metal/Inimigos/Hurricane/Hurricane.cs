using UnityEngine;
using System.Collections;

public class Hurricane : MonoBehaviour {

	public Vector3 pointGrab;

	public Vector3[] pointsPatrol;

	public float forceUp = 10f;

	public Vector3[] pointsGrabbed = new Vector3[]{new Vector3(0f,-2.8f,0f), new Vector3(0f,-0.5f,0f)};

	public float velocityShotUp = 1f;
	public float velocityWalk = 1f;

	Transform m_transform;
//	Animator m_animator;
	Transform m_player;

	public State state = State.Patrol;
	public enum State {
		Captured,
		Patrol
	}

	bool ready = false;

	IEnumerator Start ()
	{
		m_transform = transform;

		while(Singleton.player == null)
			yield return null;

		m_player = Singleton.player.transform;
		ready = true;
	}

	void Update ()
	{
		if(!ready) return;

		switch(state)
		{
		case State.Patrol:
//			if(!m_animator.GetBool("Patrol"))
//				m_animator.SetBool("Patrol", true);
			Patrol();
			break;

		case State.Captured:
//			if(m_animator.GetBool("Patrol"))
//				m_animator.SetBool("Patrol", false);
			Captured();
			break;
		}
	}

	int indexWalk = 0;
	Vector3 velWalk = Vector3.zero;
	void Patrol ()
	{
		#region INTERPOLAÇAO COM O ESTADO DA POSIÇAO INICIAL
//		if(Vector3.Distance(m_transform.position, pointsPatrol[indexWalk]) < 0.05f)
//			indexWalk = indexWalk == 0 ? 1 : 0;
//
//		float lerpAngle = Time.deltaTime * velocityWalk * (10 - Vector3.Distance(m_transform.position, pointsPatrol[indexWalk])) * 0.1f;
//		m_transform.position = Vector3.Lerp(m_transform.position, pointsPatrol[indexWalk], lerpAngle);
		#endregion
		#region INTERPOLAÇAO LINEAR SEM O INICIAL
//		float lerpAngle = Mathf.Sin(Time.time * velocityWalk) * .5f + .5f;
//		m_transform.position = Vector3.Lerp(pointsPatrol[0], pointsPatrol[1], lerpAngle);
		#endregion
		#region USANDO SMOOTHDAMP                              
		if(Vector3.Distance(m_transform.position, pointsPatrol[indexWalk]) < 0.05f)
			indexWalk = indexWalk == 0 ? 1 : 0;

		float lerpAngle = Time.deltaTime * velocityWalk * (10 - Vector3.Distance(m_transform.position, pointsPatrol[indexWalk])) * 0.1f;
		m_transform.position = Vector3.SmoothDamp(m_transform.position, pointsPatrol[indexWalk], ref velWalk, velocityWalk, 2f);
		#endregion
	}

	void Captured ()
	{
		m_transform.position = Vector3.Lerp(m_transform.position, pointGrab, Time.deltaTime * velocityWalk);

		if(Vector3.Distance(m_transform.position, pointGrab) < 0.5f)
		{
			state = State.Patrol;

			m_player.transform.parent = null;
			m_player.rigidbody2D.velocity = Vector2.zero;
			m_player.rigidbody2D.isKinematic = false;
			m_player.rigidbody2D.AddForce(Vector2.up * forceUp, ForceMode2D.Impulse);
			MovePlayer.LockPlayer(false, "Hurricane");
		}
		else
		{
			#region INTERPOLAÇAO CONSIDERANDO A POSIÇAO ATUAL
//			if(Vector3.Distance(m_player.localPosition, pointsGrabbed[indexGrabbed]) < 0.00005f)
//				indexGrabbed = indexGrabbed == 0 ? 1 : 0;
//			
//			m_player.localPosition = Vector3.Lerp(m_player.localPosition, pointsGrabbed[indexGrabbed], Time.deltaTime * velocityShotUp);
			#endregion
			#region INTERPOLAÇAO LINEAR SEM O INICIAL
			float lerpAngle = Mathf.Abs(Mathf.Sin(Time.time * velocityShotUp));
			m_player.localPosition = Vector3.Lerp(pointsGrabbed[1], pointsGrabbed[0], lerpAngle);
			#endregion
			#region INTERPOLAÇAO USANDO SMOOTHDAMP
//			if(Vector3.Distance(m_player.localPosition, pointsGrabbed[indexGrabbed]) < 0.00005f)
//				indexGrabbed = indexGrabbed == 0 ? 1 : 0;
//			
//			m_player.localPosition = Vector3.SmoothDamp(m_player.localPosition, pointsGrabbed[indexGrabbed], ref velShotUp, velocityShotUp, 30f);
			#endregion
		}
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		if(!ready) return;

		if(hit.CompareTag("Player"))
		{
			state = State.Captured;
			MovePlayer.LockPlayer(true, "Hurricane");
			m_player.parent = m_transform;

			m_player.rigidbody2D.velocity = Vector2.zero;
			m_player.rigidbody2D.isKinematic = true;
		}
	}
}
