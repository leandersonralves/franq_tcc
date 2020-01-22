using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	static MoveCamera moveCamera;

	public Transform follow;
	Transform player;

	public Vector3 delta = new Vector3(3.1f, 1.2f, -6f);
	public bool isFollow = true;

	public float velocityY = 0.5f;
	public float velocityX = 0.5f;

	public float radiusDetectPath = 1f;

	public LayerMask layerColliderPath;

	Transform m_transform;

	void Awake () {
		moveCamera = this;
	}

	void Start () {
		m_transform = transform;
		if(!follow)
			follow = Singleton.player.transform;

		m_transform.position = follow.position + delta;
	}

//	void FixedUpdate ()
//	{
//		if(!isFollow || follow == null)
//			return;
//		
//		Move(Time.fixedDeltaTime);
//	}

	void Update ()
	{
		if(!isFollow || follow == null)
			return;
		if(follow.rigidbody2D == null || (follow.rigidbody2D != null && follow.rigidbody2D.velocity.sqrMagnitude < 0.1f))
			Move(Time.deltaTime);
	}

	void FixedUpdate ()
	{
		if(!isFollow || follow == null)
			return;

		if(follow.rigidbody2D == null || (follow.rigidbody2D != null && follow.rigidbody2D.velocity.sqrMagnitude > 0.1f))
			Move(Time.fixedDeltaTime);
	}

	void Move (float d)
	{
		Vector3 target = follow.position;
		
		target.z += delta.z;
		target.y += delta.y;
		
		if(follow.CompareTag("Player"))
		{
			if(player == null)
				player = follow;
			
			if(follow.IsRight())
				target.x += delta.x;
			else
				target.x -= delta.x;
		}

		target = LimitCamera(target);
		
		target.x = Mathf.Lerp(m_transform.position.x, target.x, velocityX * d);
		target.y = Mathf.Lerp(m_transform.position.y, target.y, velocityY * d);

		m_transform.position = target;
	}

	Vector3 LimitCamera (Vector3 target) {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(follow.transform.position, radiusDetectPath, layerColliderPath);

		for(int i = 0; i < colliders.Length; i++)
		{
			if(colliders[i].GetComponent<LimitCamera>() != null)
			{
				LimitCamera limit = colliders[i].GetComponent<LimitCamera>();
				if(limit.direction == Direction.Horizontal)
				{
					switch(limit.type)
					{
					case TypeLimit.Lock:
						target.y = colliders[i].transform.position.y;
						break;

					case TypeLimit.Maximum:
						target.y = Mathf.Clamp(target.y, 
						                       float.NegativeInfinity, 
						                       colliders[i].transform.position.y);
						break;

					case TypeLimit.Minimun:
						target.y = Mathf.Clamp(target.y, 
						                       colliders[i].transform.position.y, 
						                       float.PositiveInfinity);
						break;
					}
				}
				else
				{
					switch(limit.type)
					{
					case TypeLimit.Lock:
						target.x = colliders[i].transform.position.x;
						break;
						
					case TypeLimit.Maximum:
						target.x = Mathf.Clamp(target.x, 
						                       float.NegativeInfinity, 
						                       colliders[i].transform.position.x);
						break;
						
					case TypeLimit.Minimun:
						target.x = Mathf.Clamp(target.x, 
						                       colliders[i].transform.position.x, 
						                       float.PositiveInfinity);
						break;
					}
				}
			}
		}
		return target;
	}

	public static void GoToPlayer ()
	{
		if(moveCamera.player == null)
			moveCamera.player = GameObject.FindWithTag("Player").transform;
		
		moveCamera.follow = moveCamera.player;
	}

	public static void ForcePosition ()
	{
		Vector3 target = moveCamera.follow.position;
		target.z += moveCamera.delta.z;
		target.y += moveCamera.delta.y;
		moveCamera.m_transform.position = target;
	}

	public static void SetTarget (Transform target, float timeToFollow, float velocity)
	{
		moveCamera.StartCoroutine(moveCamera.SetTemp(target, timeToFollow, velocity));
	}

	IEnumerator SetTemp (Transform target, float timeToFollow, float velocity)
	{
		Transform temp_follow = follow;
		follow = target;

		float temp_velocityX = velocityX;
		float temp_velocityY = velocityY;

		velocityX = velocityY = velocity;

		Vector3 temp_delta = delta;
		delta.x = 0f;
		delta.y = 0f;

		while(Vector3.Distance(m_transform.position - delta, follow.position) > 3.5f)
			yield return null;

		float time = 0f;
		while(time < timeToFollow){
			time += Time.deltaTime;
			yield return null;
		}

		follow = temp_follow;
		delta = temp_delta;
		velocityX = temp_velocityX;
		velocityY = temp_velocityY;
	}



	public static Transform Follow
	{
		set {
			moveCamera.follow = value;
		}
	}
}
