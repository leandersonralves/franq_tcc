using UnityEngine;
using System.Collections;

public enum TypeValue
{
	None,
	Float,
	String
}

public class ButtonSendMessage : MonoBehaviour {
	
	public string functionName;
	public TypeValue typeValue = TypeValue.String;

	public float floatToSend;
	public string stringToSend;

	public GameObject receiver;

	public Transform shine;

	void OnMouseUpAsButton ()
	{
		switch (typeValue)
		{
		case TypeValue.Float:
			receiver.SendMessage(functionName, floatToSend);
			break;
		case TypeValue.String:
			receiver.SendMessage(functionName, stringToSend);
			break;
		case TypeValue.None:
			receiver.SendMessage(functionName);
			break;
		}
	}
	
	public void OnMouseOver ()
	{
		if(shine != null)
			shine.position = transform.position;
	}
}
