using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {
	
	public string tagAnimatorDie = "Die";
	public string tagAnimatorHit;

	public float health = 100f;
	protected float initialHealth;
	
	protected Animator m_animator;
	protected bool hasAnimator = false;

	void Start ()
	{
		SceneManager.OnDie += Respawn;

		initialHealth = health;
		if(m_animator == null && GetComponent<Animator>() != null)
		{
			hasAnimator = true;
			m_animator = GetComponent<Animator>();
		}

		if(m_animator == null && GetComponentInChildren<Animator>() != null)
			m_animator = GetComponentInChildren<Animator>();
	}

	public float Health
	{
		set {
			if(value < health)
				Hit();

			health = value;

			if(health <= 0)
				Die();
			else if (health > initialHealth)
				health = initialHealth;
		}

		get {
			return health;
		}
	}

	public virtual void Hit ()
	{
		if(!string.IsNullOrEmpty(tagAnimatorHit))
		{
			m_animator.SetTrigger(tagAnimatorHit);
			Debug.Log("HITOU " + tagAnimatorHit);
		}
	}

	protected virtual void Die ()
	{
		m_animator.SetTrigger(tagAnimatorDie);
	}

	protected virtual void Respawn (){}
}