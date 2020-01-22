using UnityEngine;
using System.Collections;

public class Robola : MonoBehaviour {
	
	public enum StateRobola {
		Rolling,
		Idle
	}
	public StateRobola state = StateRobola.Idle;

	public MovementSlam[] movementSlams;

	public float timeToRolling = 2f;
	float currentTimeToRolling = 0f;

	public float timeToIdle = 2f;
	float currentTimeToIdle = 0f;

	public float force = 300f;

	Rigidbody2D m_rigidbody;
	Transform m_transform;
	Animator m_animator;

	int direction = -1;

	Dano damage;
	Life life;

	void Start ()
	{
		damage = GetComponent<Dano>();
		life = GetComponent<Life>();

		m_transform = transform;
		m_rigidbody = rigidbody2D;
		m_animator = GetComponentInChildren<Animator>();
		switch(state)
		{
		case StateRobola.Idle:
			m_animator.SetTrigger("Desencolher");
			damage.enabled = false;
			break;
			
		case StateRobola.Rolling:
			m_animator.SetTrigger("Encolher");
			damage.enabled = false;
			break;
		}
	}

	void Update ()
	{
		switch(state)
		{
		case StateRobola.Idle:
			if(Singleton.player.transform.position.x > m_transform.position.x)
				if(!m_transform.IsRight())
					m_transform.Turn(true);
			else if (Singleton.player.transform.position.x < m_transform.position.x)
				if(m_transform.IsRight())
					m_transform.Turn(false);

			currentTimeToRolling += Time.deltaTime;
			if(currentTimeToRolling >= timeToRolling)
			{
				currentTimeToRolling = 0f;
				state = StateRobola.Rolling;
				m_animator.SetTrigger("Encolher");
				damage.enabled = true;
				ApplyForce();
			}
			break;

		case StateRobola.Rolling:
			currentTimeToIdle += Time.deltaTime;
			if(currentTimeToIdle >= timeToIdle)
			{
				currentTimeToIdle = 0f;
				state = StateRobola.Idle;
				damage.enabled = false;
			}
			break;
		}
	}

	void OnTriggerStay2D (Collider2D hit)
	{
		switch(state)
		{
		case StateRobola.Idle:
			if (hit.tag == "ApplyForce" && !m_rigidbody.isKinematic)
			{
				m_rigidbody.isKinematic = true;
				m_transform.eulerAngles = Vector3.zero;
				m_animator.SetTrigger("Desencolher");
			}

			if(hit.CompareTag("Punch"))
				life.Health -= 50f * Time.deltaTime;

			break;

		case StateRobola.Rolling:
			if (hit.tag == "ApplyForce" && m_rigidbody.velocity.magnitude < 0.3f)
			{
				ApplyForce();
			}
			break;
		}
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		switch(state)
		{
		case StateRobola.Rolling:
			if (hit.tag == "ApplyForce")
				ApplyForce();
			break;
		}
	}

	void ApplyForce ()
	{
		if(m_rigidbody.isKinematic)
			m_rigidbody.isKinematic = false;
		Debug.Log("Apply Force");
		m_rigidbody.AddForce (Vector2.right * direction * force * Time.deltaTime, ForceMode2D.Force);
		direction *= -1;
	}

	void OnDestroy ()
	{
		for(int i = 0; i < movementSlams.Length; i++)
		{
			if(movementSlams[i] != null)
				movementSlams[i].act = true;
		}
	}
}
