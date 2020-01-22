using UnityEngine;
using System.Collections;

public class TentaculoRController : MonoBehaviour {

	public GameObject octopus;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Sair(){
		octopus.GetComponent<OctopusController>().FimAtaqueR();
	}
}
