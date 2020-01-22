using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour, IFeedback {

	Feedback feedback = Feedback.Diegetic;
	public Feedback Feedback {
		get{
			return feedback;
		}
	}
	
	public GameObject colliderDiegetic;
	public GameObject colliderNoDiegetic;

	bool collected = false;

	void Start ()
	{
		Singleton.feedback.Add((IFeedback)this);
	}

	public void Collected () {
		colliderDiegetic.SetActive(false);
		colliderNoDiegetic.SetActive(false);
	}

	bool isEnable = true;
	public void Enable ()
	{
		isEnable = true;
		colliderDiegetic.SetActive(true);
		colliderNoDiegetic.SetActive(false);
	}

	public void Disable ()
	{
		isEnable = false;
		colliderDiegetic.SetActive(false);
		colliderNoDiegetic.SetActive(true);
	}
}
