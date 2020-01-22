using UnityEngine;
using System.Collections;

public class StirploxControl : MonoBehaviour {

	private Animator anim;

	public Transform pos;

	public int pressMax;
	private int press;
	private bool right;

	private bool fechado;
	private float count;
	public float dmgTime;
	public float dmgPerSec;

	private bool canEat = true;

	void Start(){
		anim = GetComponentInParent<Animator>();
		press = pressMax;
		count = dmgTime;
	}

	void OnTriggerEnter2D( Collider2D obj ) {
		if(obj.CompareTag("Player")){
			if(canEat){
				obj.rigidbody2D.velocity = Vector2.zero;
				obj.transform.position = pos.position;
				fechado = true;

				anim.SetTrigger("fechar");
				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Game Layer Front";
				Debug.Log("fecha");
				MovePlayer.canMove = false;

				canEat = false;
			}
		}
	}

	void OnTriggerStay2D( Collider2D obj ) {
		if(obj.CompareTag("Player")){
			if(right){
				if(Input.GetKeyUp(Button.Right)){
					anim.SetTrigger("mexer");
					press--;
					right = false;
				}
			}else{
				if(Input.GetKeyUp(Button.Left)){
					anim.SetTrigger("mexer");
					press--;
					right = true;
				}
			}

			if(press == 0){
				press = pressMax;
				anim.SetTrigger("abrir");
				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Game Layer";
				Debug.Log("abre");
				MovePlayer.canMove = true;
				fechado = false;

				StartCoroutine(TurnCanEat());
			}

			if(fechado){
				if(count >= 0){
					count -= Time.deltaTime;
				}else{
					count = dmgTime;
					GetComponentInParent<Dano>().Ataque(dmgPerSec);
				}
			}
		}
	}

	IEnumerator TurnCanEat () {
		yield return new WaitForSeconds(2f);

		canEat = true;
	}
}