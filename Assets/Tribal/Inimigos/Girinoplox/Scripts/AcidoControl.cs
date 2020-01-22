using UnityEngine;
using System.Collections;

public class AcidoControl : MonoBehaviour
{

	public float speed;
	public float atk;

	void Start(){
	}

	// Update is called once per frame
	void Update ()
	{
		//transform.Translate( transform.right * speed * Time.deltaTime );
	}

	void OnCollisionEnter2D(Collision2D obj){
		//Debug.Log("colidiu");
		if (obj.gameObject.tag == "Player") {

			if(!Skills.inDefense)
				obj.gameObject.GetComponent<Life>().Health -= atk;

			GameObject.Destroy(gameObject);
		}
		if (obj.gameObject.tag == "Ground") {
			Debug.Log("chao");
			GameObject.Destroy(gameObject);
		}

	}
}

