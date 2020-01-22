using UnityEngine;
using System.Collections;

public class FeedbackFranq : MonoBehaviour, IFeedback{

	const Feedback feedback = Feedback.Diegetic;
	public Feedback Feedback {
		get {
			return feedback;
		}
	}

	bool isEnable = true;
	Material materialFranq;

	public GameObject heartFranq;
	Material materialHeart;

	void Awake () {
		materialFranq = renderer.sharedMaterial;
		materialHeart = heartFranq.renderer.material;
	}

	void Start () {
		Singleton.feedback.Add((IFeedback)this);
	}

	void Update () {
		if(!isEnable)
			return;

		materialFranq.SetFloat("_Factor", 1 - Cooldown.current);// = Color.Lerp(lowHidrogenio, highHidrogenio, Cooldown.current);

		float rgbHeart = LifePlayer.m_instance.Health / 100f;
		materialHeart.color = Color.Lerp(Color.red, Color.yellow, rgbHeart);
	}

	public void Enable (){
		isEnable = true;
		heartFranq.SetActive(isEnable);
	}

	public void Disable (){
		isEnable = false;
		heartFranq.SetActive(isEnable);
		materialFranq.SetFloat("_Factor", 0);
	}
}
