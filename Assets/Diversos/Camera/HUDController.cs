using UnityEngine;
using System;
using System.Collections;

public class HUDController : MonoBehaviour, IFeedback {

	static HUDController m_instance;

	const Feedback feedback = Feedback.NoDiegetic;
	public Feedback Feedback {
		get {
			return feedback;
		}
	}

	public SpriteRenderer textInfo;

	public Sprite[] textInfoTextures;

	public Material lifeMaterial;
	public Material coolMaterial;

	bool enable;

	void Awake ()
	{
		RecalculatePosition();
		m_instance = this;
	}

	void RecalculatePosition ()
	{
		float height = (float)Screen.currentResolution.height;
		float width = (float)Screen.currentResolution.width;
		float aspectRatio =  width/height;
		
		Vector3 local = transform.localPosition;
		local.x = -(aspectRatio * 1.0687f);
		transform.localPosition = local;
	}

	void Start ()
	{
		Singleton.feedback.Add((IFeedback)this);
		RecalculatePosition();
	}

	void Update ()
	{
		if(!enable)
			return;

		float l = 1 - (LifePlayer.m_instance.Health / 100f);
		lifeMaterial.SetFloat("_Cutoff", l);

		float c = l + (1-Cooldown.current);
		if(c > 1) c = 1f;
		coolMaterial.SetFloat("_Cutoff", c);
	}

	public void Enable ()
	{
		enable = true;
		transform.GetChild(0).gameObject.SetActive(enable);
	}

	public void Disable ()
	{
		enable = false;
		transform.GetChild(0).gameObject.SetActive(enable);
	}

	public static void SetInfo (Sprite sprite)
	{
		m_instance.Info(sprite);
	}

	bool inErase = false;
	public void Info (Sprite sprite)
	{
		textInfo.enabled = true;
		textInfo.sprite = sprite;

		StartCoroutine(EraseInfo());
	}

	IEnumerator EraseInfo () {
		inErase = true;
		yield return new WaitForSeconds(2f);

		if(inErase)
			textInfo.enabled = false;
		inErase = false;
	}
}
