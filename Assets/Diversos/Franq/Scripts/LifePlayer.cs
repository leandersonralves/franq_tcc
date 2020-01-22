using UnityEngine;
using System.Collections;

public class LifePlayer : Life {

	public Animator animatorEye;
	public bool infiniteLife = false;

	public static LifePlayer m_instance;

	void Awake ()
	{
		m_instance = this;
	}

	protected override void Respawn ()
	{
		health = initialHealth;
	}

	public override void Hit ()
	{
		if(!string.IsNullOrEmpty(tagAnimatorHit))
			animatorEye.SetTrigger(tagAnimatorHit);
	}

	protected override void Die ()
	{
		if(infiniteLife || m_animator.GetBool("InDie")) return;

		MovePlayer.LockPlayer(true, "Die");
//		m_animator.SetTrigger(tagAnimatorDie);
		m_animator.SetBool(tagAnimatorDie, true);
	}

	void FinishedDie () 
	{
		SceneManager.ReloadScene();
	}
}
