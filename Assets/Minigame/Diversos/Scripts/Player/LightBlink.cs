using UnityEngine;
using System.Collections;

public class LightBlink : MonoBehaviour {

	public float blinkTime;
	private float counter;
	private bool lightOn;

	// Use this for initialization
	void Start () {
		lightOn = true;
		counter = blinkTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(counter <= 0F){
			lightOn = !lightOn;
			counter = blinkTime;

			if(lightOn) 	GetComponent<SpriteRenderer>().enabled = true;
			else 			GetComponent<SpriteRenderer>().enabled = false;
		}else{
			counter -= Time.deltaTime;
		}
	}
}
