using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour
{
	public float amountExpendLiquid = 0.05f;

	public float amountExpendBubble = 0.3f;
	public Vector3 positionBubble = Vector3.zero;

	private Animator m_animator;

	public bool isLiquid = false;
	private Cooldown coolDown;

	MovePlayer movePlayer;

	public LayerMask franq;
	public LayerMask franqAgachado;

	public static bool inDefense = false;

	void Start ()
	{
		coolDown = Cooldown.m_instance;

		m_animator = GetComponent<Animator>();

		if(GetComponent<MovePlayer>() != null)
			movePlayer = GetComponent<MovePlayer>();
	}

	void Update () {
		if(movePlayer != null && !movePlayer.UseSkills)
			return;

		if(Input.GetKeyDown(Button.Gas))
		{
			if (BubbleHydro.canThrow)
			{
				MovePlayer.LockPlayer(true, "Bubble");
				m_animator.SetTrigger("Bubble");
			}
			else
				BubbleHydro.Deactive();
		}

		if(!MovePlayer.canMove)
			return;

		if(Input.GetKeyDown(Button.Defense))
			m_animator.SetBool("Defense", true);
		if(Input.GetKeyUp(Button.Defense))
			m_animator.SetBool("Defense", false);

		if(m_animator.GetCurrentAnimatorStateInfo(0).length > 0)
			inDefense = m_animator.GetCurrentAnimationClipState(0)[0].clip.name.Contains("defense");

		if(Input.GetKeyDown (Button.Dash) && !m_animator.GetCurrentAnimationClipState(0)[0].clip.name.Contains("walk"))
			m_animator.SetTrigger("Dash");

		if (Input.GetKeyDown (Button.Liquify))
			StartCoroutine(Liquefying());
	}

	IEnumerator Liquefying ()
	{
		if(isLiquid || m_animator.GetCurrentAnimationClipState(0)[0].clip.name.Contains("liquid"))
			yield break;

		isLiquid = true;
		m_animator.SetBool("Liquefy", isLiquid);

		yield return new WaitForFixedUpdate();

		float counter = 0f;
		while(isLiquid)
		{
			counter += Time.deltaTime;
			if(!coolDown.Expend(amountExpendLiquid) || //caso acabe o cooldown.
			   (Input.GetKeyDown(Button.Up) || Input.GetKeyDown(Button.Jump)) || //Caso o jogador pressione para cima ou pulo.
			   (Input.GetKeyUp(Button.Down) && counter < 0.8f)
			   )
			{
				if(!ObstacleAbove)
				{
					isLiquid = false;
					m_animator.SetBool("Liquefy", isLiquid);
				}
			}

			yield return null;
		}
	}

	bool ObstacleAbove {
		get {
			Vector3 pos = transform.position;
			pos.y += 0.25f;
			RaycastHit2D[] hit = Physics2D.RaycastAll(pos, Vector2.up, 1.5f);
			for(int i = 0; i < hit.Length; i++)
				if(!hit[i].collider.CompareTag("Player"))
					return true;

			return false;
		}
	}

	void Liquid ()
	{
		gameObject.layer = 18;
	}

	void Solid ()
	{
		StartCoroutine(BackLayer());
	}

	IEnumerator BackLayer ()
	{
		while(hitDuto)
			yield return null;

		gameObject.layer = 15;
	}

	bool hitDuto = false;
	void OnCollisionStay2D (Collision2D hit)
	{
		if(!hitDuto && hit.collider.gameObject.layer == 17)
			hitDuto = true;
	}
	void OnCollisionExit2D (Collision2D hit)
	{
		if(hitDuto && hit.collider.gameObject.layer == 17)
			hitDuto = false;
	}

	void Bubble ()
	{
		if(coolDown.Expend(amountExpendBubble))
			BubbleHydro.Throw(transform.forward, transform.TransformPoint(positionBubble));
	}
}
