using UnityEngine;
using System.Collections;

public class Vento : MonoBehaviour {

	public GameObject vento;
	public float tempoVento;
	private float tempo;

	// Use this for initialization
	void Start () {
		tempo = 0F;
	}
	
	// Update is called once per frame
	void Update () {
		if(tempo > 0F){
			vento.SetActive(true);
			tempo -= Time.deltaTime;
		}else{
			vento.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D obj){
		if(obj.gameObject.tag == "Player"){
			tempo = tempoVento;
		}
	}
}
