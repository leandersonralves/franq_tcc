using UnityEngine;
using System.Collections;

public class Items : MonoBehaviour, IFeedback {

	Feedback feedback = Feedback.NoDiegetic;
	public Feedback Feedback {
		get{
			return feedback;
		}
	}

	public int fuel;
	public bool itemObjective = false;

	public string tagItemObjective = "ItemObjective";
	public string tagFuel = "ItemFuel";

	bool isEnable = true;

	GameObject robo;

	IEnumerator Start () {
		Singleton.feedback.Add((IFeedback)this);

		while(GameObject.FindGameObjectWithTag("Robo") == null)
			yield return null;

		robo = GameObject.FindGameObjectWithTag("Robo");
	}

	public void Enable () {isEnable = true;}

	public void Disable () {isEnable = false;}

	void OnTriggerEnter2D (Collider2D hit) {
		if(hit.CompareTag(tagItemObjective))
		{
			itemObjective = true;

			if(isEnable)
				GameObject.Destroy(hit.gameObject);
			else if (robo != null)
				robo.SendMessage("ToItem", hit.GetComponentInParent<Item>());
		}
		else if (hit.CompareTag(tagFuel))
		{
			fuel++;

			if(isEnable)
				GameObject.Destroy(hit.gameObject);
			else if (robo != null)
				robo.SendMessage("ToItem", hit.GetComponentInParent<Item>());
		}
	}
}
