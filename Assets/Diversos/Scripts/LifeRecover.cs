using UnityEngine;
using System.Collections;

public class LifeRecover : MonoBehaviour {

	public float recoverTime;
	private float timer;

	public float recoverAmount;

	// Use this for initialization
	void Start () {
		timer = recoverTime;
	}

	void OnTriggerStay2D(Collider2D obj){
		if(obj.gameObject.tag == "Player"){
			if(timer >= 0F){
				timer -= Time.deltaTime;
			}else{
				obj.gameObject.GetComponent<LifePlayer>().Health += recoverAmount;
				timer = recoverTime;
			}
		}
	}
}
