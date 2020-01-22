using UnityEngine;
using System.Collections;

public class WindControl : MonoBehaviour {

	public AnimationClip animacao;
	private int index;
	private float animationInterval;
	private float ai;

	// Use this for initialization
	void Start () {
		animationInterval = (animacao.length * 4);
		ai = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(ai <= 0F){
			TurnOff();
			index = Random.Range(0, (transform.childCount - 1));
			transform.GetChild(index).gameObject.SetActive(true);
			ai = animationInterval;
		}else{
			ai -= Time.deltaTime;
		}
	}

	private void TurnOff(){
		foreach(Transform child in transform){
			child.gameObject.SetActive(false);
		}
	}

}
