using UnityEngine;
using System.Collections;

public class GirinoCabutploxControl : MonoBehaviour
{

	public float dmgPerPunch;

	private float life;

	void Update(){
		life = GetComponentInParent<GirinoploxControl> ().life;

		float r = life / 100;
		Color rgb = new Color (1, r, r, 1);

		GetComponent<SpriteRenderer> ().color = rgb;

		//Debug.Log (rgb);
	}

	public void AtirarLanca(){
		gameObject.GetComponentInParent<GirinoploxControl> ().AtirarLanca ();
	}

	void OnTriggerEnter2D( Collider2D obj ) {
		Debug.Log (obj.tag);
		if(obj.CompareTag("Punch")){
			gameObject.GetComponentInParent<GirinoploxControl> ().life -= dmgPerPunch;
		}
	}

}

