using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Interpolacao {
	EntrouMudou,
	AosPouquinhos
}

[RequireComponent(typeof(Collider2D))]
public class ChangeColor : MonoBehaviour {

	public Interpolacao interpolationMode;

	public float minDistance;

	public List<SpriteRenderer> sprites = new List<SpriteRenderer>();
	public Color colorTarget = Color.red;
	public float factorMultiply = 1f;
	List<Color> colorsDefault = new List<Color>();

	float distanceNormal;
	bool inTrigger = false;
	Transform m_transform;

	void Start () {
		m_transform = transform;

		for (int i = 0; i < sprites.Count; i++)
			colorsDefault.Add(sprites[i].color);
	}

	void Update () {
		if (!inTrigger || interpolationMode == Interpolacao.EntrouMudou)
			return;
		switch (interpolationMode) {
		case Interpolacao.AosPouquinhos:
			distanceNormal = Mathf.InverseLerp(minDistance, 0f, Vector3.Distance(m_transform.position, Singleton.player.transform.position)) * factorMultiply;
			for (int i = 0; i < sprites.Count; i++)
				sprites[i].color = Color.Lerp(colorsDefault[i], colorTarget, distanceNormal);
			break;
		}
	}

	void OnTriggerEnter2D (Collider2D hit) {
		if(hit.CompareTag("Player"))
		{
			if(interpolationMode == Interpolacao.EntrouMudou)
			{
				for (int i = 0; i < sprites.Count; i++)
					sprites[i].color = colorTarget;
			}
			else
				inTrigger = true;
		}
	}

	void OnTriggerExit2D (Collider2D hit) {
		if(hit.CompareTag("Player"))
		{
			inTrigger = false;

			for (int i = 0; i < sprites.Count; i++)
				sprites[i].color = colorsDefault[i];
		}
	}
}
