using UnityEngine;
using System.Collections.Generic;

public class FeedbackManager : MonoBehaviour {

	public static FeedbackManager instance;

	public static Feedback type = Feedback.Diegetic;

	private List<IFeedback> feedbackDiegetic = new List<IFeedback>();
	private List<IFeedback> feedbackNoDiegetic = new List<IFeedback>();

	void Awake ()
	{
		if(instance != null)
		{
			GameObject.Destroy(gameObject);
			return;
		}

		instance = this;
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.F1))
			ChangeType();
	}

	void OnLevelWasLoaded ()
	{
		feedbackDiegetic.Clear();
		feedbackNoDiegetic.Clear();
	}

	private void ChangeType () {
		if(type == Feedback.Diegetic) {
			type = Feedback.NoDiegetic;

			for(int i = 0; i < feedbackDiegetic.Count; i++)
				feedbackDiegetic[i].Disable();

			for(int i = 0; i < feedbackNoDiegetic.Count; i++)
				feedbackNoDiegetic[i].Enable();
		}
		else {
			type = Feedback.Diegetic;

			for(int i = 0; i < feedbackDiegetic.Count; i++)
				feedbackDiegetic[i].Enable();
			
			for(int i = 0; i < feedbackNoDiegetic.Count; i++)
				feedbackNoDiegetic[i].Disable();
		}
	}

	public void Add (IFeedback feedback) {
		if(feedback.Feedback == Feedback.Diegetic) {
			feedbackDiegetic.Add(feedback);

			if(type == Feedback.Diegetic)
				feedback.Enable();
			else
				feedback.Disable();
		}
		else {
			feedbackNoDiegetic.Add(feedback);

			if(type == Feedback.NoDiegetic)
				feedback.Enable();
			else
				feedback.Disable();
		}
	}
}
