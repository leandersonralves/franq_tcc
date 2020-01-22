using UnityEngine;
using System.Collections;

public class Robo : MonoBehaviour {

	public NavigationAgent navAgent;

	public InCamera roboInCamera;

	public RobotState state;
	public enum RobotState {
		Idle,
		FollowFranq,
		Grab,
		Talking,
		CollectItem,
		Checkpoint,
		Wait
	}

	Item itemCollected;

	public Transform sourceNavigation;
	private Transform m_transform;
	private Animator m_animator;
	private Vector3 beforePosition;
	public float velocityAngle = 20f;
	public float velocityWalk = 0.1f;

	public static Robo instanceRobo;
	
	Vector3 smoothDampVelocity;

	void Start () {
		instanceRobo = this;

		m_transform = transform;

		
		if(navAgent == null)
			navAgent = m_transform.parent.GetComponent<NavigationAgent>();

		beforePosition = m_transform.position;
		m_animator = GetComponentInChildren<Animator>();
	}

	void Update () {
		switch(state){
		case RobotState.FollowFranq:
			m_transform.position = new Vector3(
					sourceNavigation.position.x, 
					sourceNavigation.position.z, 
					sourceNavigation.position.y
				);

			Vector3 directionMove = (m_transform.position - beforePosition).normalized;

			if(directionMove.x > 0)
				m_transform.Turn(true);
			else if (directionMove.x < 0)
				m_transform.Turn(false);

			beforePosition = m_transform.position;
			break;

		case RobotState.Idle:
			if(!m_animator.GetBool("Idle"))
				m_animator.SetBool("Idle", true);
			break;

		case RobotState.Talking:
			if(!m_animator.GetBool("Talking"))
				m_animator.SetBool("Talking", true);

			if(audio != null && !audio.isPlaying)
				ToFollow();

			break;

		case RobotState.Checkpoint:
			if(navAgent.IsNear)
			{
				if(!unlockPlayer)
				{
					navAgent.SetTarget(null, NavigationAgent.TargetType.CheckPoint);
					unlockPlayer = true;
					m_animator.SetTrigger("Checkpoint");
					MovePlayer.LockPlayer(false, "CheckPoint");
				}
				else if (timeCheckpoint > 1.7f)
				{
					ToFollow();
				}
				else
					timeCheckpoint += Time.deltaTime;
			}
			else
			{
				m_transform.position = new Vector3(
					sourceNavigation.position.x, 
					sourceNavigation.position.z, 
					sourceNavigation.position.y
					);
			}
			break;

		case RobotState.CollectItem:
			m_transform.position = new Vector3(
				sourceNavigation.position.x,
				sourceNavigation.position.z, 
				sourceNavigation.position.y
				);

			if(navAgent.IsNear && !isUpItem)
			{
				m_animator.SetTrigger("CollectItem");
				StartCoroutine(UpItem());
			}
			break;
		}
	}

	void ToFollow ()
	{
		m_animator.SetTrigger("Idle");
		state = RobotState.FollowFranq;
		navAgent.SetTarget(Singleton.player.transform, NavigationAgent.TargetType.Player);
	}

	void ToItem (Item item)
	{
		StartCoroutine(WaitItem(item));
	}

	IEnumerator WaitItem (Item item)
	{
		while(state != RobotState.FollowFranq)
			yield return null;

		itemCollected = item;
		itemCollected.Collected();
		navAgent.SetTarget(item.transform, NavigationAgent.TargetType.Item);
		state = RobotState.CollectItem;
	}

	float timeCheckpoint = 0f;
	bool unlockPlayer = false;
	void ToCheckpoint (TriggerCheckpoint checkpoint)
	{
		if(!roboInCamera.inView)
		{
//			navAgent.transform.position = checkpoint.forcePosRobot;
			navAgent.ForcePosition(checkpoint.forcePosRobot);
		}

		StartCoroutine(WaitCheckpoint());
	}

	IEnumerator WaitCheckpoint ()
	{
		while(state != RobotState.FollowFranq)
			yield return null;

		unlockPlayer = false;
		timeCheckpoint = 0f;
		MovePlayer.LockPlayer(true, "CheckPoint");
		navAgent.SetTarget(Singleton.player.transform,	NavigationAgent.TargetType.CheckPoint);
		state = RobotState.Checkpoint;
	}



	bool isUpItem = false;
	IEnumerator UpItem ()
	{
		if(itemCollected == null) yield break;

		isUpItem = true;
		navAgent.SetTarget(null, NavigationAgent.TargetType.CheckPoint);

		itemCollected.transform.parent = m_transform;
		Vector3 posInitial = itemCollected.transform.localPosition;
		SpriteRenderer sprite = itemCollected.GetComponent<SpriteRenderer>();
		Color currentColor = sprite.color;
		Color targetColor = currentColor;
		targetColor.a = 0f;
		float lerp = 0f;
		while(lerp <= 1f)
		{
			itemCollected.transform.localPosition = Vector3.Lerp(posInitial, Vector3.zero, lerp);
			lerp += Time.deltaTime * 0.2f;
			sprite.color = Color.Lerp(currentColor, targetColor, lerp);
			yield return null;
		}
		GameObject.Destroy(itemCollected.gameObject);

		ToFollow();
		isUpItem = false;
	}
}
