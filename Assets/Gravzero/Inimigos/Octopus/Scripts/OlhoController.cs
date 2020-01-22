using UnityEngine;
using System.Collections;

public class OlhoController : MonoBehaviour {

	public GameObject octopus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Entrar(){
		octopus.GetComponent<OctopusController>().Entrando();
	}

	public void Sair(){
		octopus.GetComponent<OctopusController>().Saindo();
	}
}
