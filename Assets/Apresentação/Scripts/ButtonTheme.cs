using UnityEngine;
using System.Collections;

public class ButtonTheme : MonoBehaviour {

	public string theme = string.Empty;
	public string functionName = "GoTheme";

	public bool firstSelect = false;
	public GameObject controllerTheme;
	public Transform shine;
	
	void Start ()
	{
		if(firstSelect)
		{
			shine.position = transform.position;
			controllerTheme.SendMessage(functionName, theme);
		}
	}
	
	public void OnMouseUpAsButton ()
	{
		if(shine != null)
			shine.position = transform.position;
		
		controllerTheme.SendMessage(functionName, theme);
	}
}
