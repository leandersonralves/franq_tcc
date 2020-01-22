using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerTheme : MonoBehaviour {

	public Animator[] objectsThemes = new Animator[]{};

	public GameObject startMenu;
	SpriteRenderer[] startSprites;

	public GameObject slideMenu;
	SpriteRenderer[] slideSprites;

	Dictionary<string, Animator> themes = new Dictionary<string, Animator>();
	public string actualTheme = string.Empty;

	void Awake ()
	{
		for(int i = 0; i < objectsThemes.Length; i++)
			themes.Add(objectsThemes[i].gameObject.name, objectsThemes[i]);
	}

	void Start ()
	{
		slideMenu.SetActive(true);
		startSprites = startMenu.GetComponentsInChildren<SpriteRenderer>();
		slideSprites = slideMenu.GetComponentsInChildren<SpriteRenderer>();
		slideMenu.SetActive(false);

		if(!string.IsNullOrEmpty(actualTheme))
			themes[actualTheme].SetTrigger("Show");
	}

	void GoTheme (string theme)
	{
		themes[theme].SetTrigger("Show");

		if(!string.IsNullOrEmpty(actualTheme))
			themes[actualTheme].SetTrigger("Hide");

		actualTheme = theme;

		SlideMenu();
	}

	void PreviousSlide ()
	{
		if(!string.IsNullOrEmpty(actualTheme))
			themes[actualTheme].SendMessage("Previous");
	}

	void NextSlide ()
	{
		if(!string.IsNullOrEmpty(actualTheme))
			themes[actualTheme].SendMessage("Next");
	}

	void StartMenu ()
	{
		StartCoroutine(Enable(startMenu, startSprites));
		StartCoroutine(Disable(slideMenu, slideSprites));
	}

	void SlideMenu ()
	{
		StartCoroutine(Enable(slideMenu, slideSprites));
		StartCoroutine(Disable(startMenu, startSprites));
	}

	IEnumerator Disable (GameObject menu, SpriteRenderer[] sprites)
	{
		float lerp = 0f;
		Color c = Color.white;
		c.a = 0f;
		while(lerp < 1f)
		{
			lerp += Time.deltaTime * 4f;
			for(int i = 0; i < sprites.Length; i++)
				sprites[i].color = Color.Lerp(Color.white, c, lerp);

			yield return null;
		}

		menu.SetActive(false);
	}

	IEnumerator Enable (GameObject menu, SpriteRenderer[] sprites)
	{
		menu.SetActive(true);

		float lerp = 0f;
		Color c = sprites[0].color;
		c.a = 0f;

		for(int i = 0; i < sprites.Length; i++)
			sprites[i].color = c;

		while(lerp < 1f)
		{
			lerp += Time.deltaTime * 4f;
			for(int i = 0; i < sprites.Length; i++)
				sprites[i].color = Color.Lerp(c, Color.white, lerp);

			yield return null;
		}
	}
}
