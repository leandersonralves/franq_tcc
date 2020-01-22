using UnityEngine;
using System.Collections;

public class TextInfoSolid : MonoBehaviour {

	public Sprite[] sprites = new Sprite[]{};

	void OnTriggerEnter2D (Collider2D hit) {
		if(hit.tag == "Player")
		{
			if (Button.Liquify == KeyCode.DownArrow)
				HUDController.SetInfo(sprites[0]);
			else if (Button.Liquify == KeyCode.S)
				HUDController.SetInfo(sprites[1]);
		}

		GameObject.Destroy(this);
	}
}
