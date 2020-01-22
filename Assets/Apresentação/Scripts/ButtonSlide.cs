using UnityEngine;
using System.Collections;

public class ButtonSlide : MonoBehaviour {
	public bool firstSelect = false;
	public GameObject slides;
	public float degree = 0f;
	public Transform shine;
	
	void Start ()
	{
		if(firstSelect)
			shine.position = transform.position;
	}
	
	public void OnMouseUpAsButton ()
	{
		if(shine != null)
			shine.position = transform.position;
		
		slides.SendMessage("Select", degree);
	}

	public void OnMouseOver ()
	{
		if(shine != null)
			shine.position = transform.position;
	}
}
