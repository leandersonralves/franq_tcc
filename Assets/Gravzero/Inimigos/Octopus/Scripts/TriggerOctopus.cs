using UnityEngine;
using System.Collections;

public class TriggerOctopus : MonoBehaviour {

	public GameObject octopus;

	public GameObject tentaculos;

	public int acao; // 1 = entrar, 2 = sair, 3 = matar

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D obj ) {
		if(obj.CompareTag("Player")){

			if(acao == 1){
				octopus.GetComponent<OctopusController>().Entrar();
			}else if(acao == 2){
				octopus.GetComponent<OctopusController>().Sair();
			}else if(acao == 3){
				tentaculos.GetComponent<TentaculosController>().Matar();
			}

		}
	}
}
