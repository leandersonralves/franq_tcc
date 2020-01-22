using UnityEngine;
using System.Collections;

public class FadeSprite : MonoBehaviour {

	public float speedFadeIn;
	public float speedFadeOut;

	public float delayBetweenFades;

	SpriteRenderer sprite;

	void Awake ()
	{
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}

	IEnumerator Start ()
	{
		sprite = GetComponent<SpriteRenderer>();
		float lerp = 0f;
		while(sprite.color.r < 0.99f && !Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.Escape))
		{
			lerp += Time.deltaTime * speedFadeIn;
			sprite.color = Color.Lerp(Color.black, Color.white, lerp);
			yield return null;
		}

		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
			delayBetweenFades = .1f;

		yield return new WaitForSeconds(delayBetweenFades);

		Color c = sprite.color;
		lerp = 0f;
		while(sprite.color.r > 0.01f && !Input.GetKeyDown(KeyCode.Return) && !Input.GetKeyDown(KeyCode.Escape))
		{
			lerp += Time.deltaTime * speedFadeOut;
			sprite.color = Color.Lerp(c, Color.black, lerp);
			yield return null;
		}
		
		GameObject.Destroy(gameObject);
	}
}
