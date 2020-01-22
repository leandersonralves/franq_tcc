using UnityEngine;
using System.Collections;

public class TentaculosController : MonoBehaviour {

	public GameObject octopus;
	private Animator anim;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Sair(){
		octopus.GetComponent<OctopusController>().FimAtaqueT();
	}

	public void Matar(){
		anim.SetTrigger("Atacar");
	}

	void OnTriggerEnter2D( Collider2D obj ) {
		Debug.Log("trigger = " + obj.name);
		if(obj.CompareTag("Ground")){
			if(obj.GetComponent<PullPlataform>()){
				if(obj.GetComponent<PullPlataform>().move == false){
					obj.GetComponent<PullPlataform>().move = true;
				}
			}
		}
	}
}
