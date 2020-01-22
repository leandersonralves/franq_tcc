using UnityEngine;
using System.Collections;

public class TentaculoLController : MonoBehaviour {

	public GameObject octopus;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Sair(){
		octopus.GetComponent<OctopusController>().FimAtaqueL();
	}
}
