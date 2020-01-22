using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPoint : MonoBehaviour {

	protected static TriggerCheckpoint lastCheckPoint;
	public TriggerCheckpoint firstCheckpoint;

	public static Dictionary<int, TriggerCheckpoint> checkpoints = new Dictionary<int, TriggerCheckpoint>();

	void Awake ()
	{
		checkpoints.Clear();
	}

	void Start ()
	{
		lastCheckPoint = firstCheckpoint;
		SceneManager.OnDie += Respawn;
	}

	void Respawn ()
	{
		MovePlayer.LockPlayer(false, "Die");
		transform.position = lastCheckPoint.transform.position;
		MoveCamera.ForcePosition();
	}

	public static void ChangeCheckpoint (bool isNext)
	{
		int temp_checkpoint = 0;
		if(lastCheckPoint == null)
			lastCheckPoint = checkpoints[0];
		else
		{
			temp_checkpoint = lastCheckPoint.sequenceCheckPoint;
			if(isNext)
				temp_checkpoint++;
			else
				temp_checkpoint--;
		}

		temp_checkpoint = Mathf.Clamp(temp_checkpoint, 0, checkpoints.Count-1);
		lastCheckPoint = checkpoints[temp_checkpoint];

		SceneManager.ReloadScene();
	}
}
