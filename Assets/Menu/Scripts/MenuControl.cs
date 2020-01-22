using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {

	public ButtonAction[] buttons = new ButtonAction[0]{};
	public int buttonSelect = 0;

//	public float speedFade = 1f;

//	void Awake ()
//	{
//		guiTexture.pixelInset = new Rect(0f,0f,Screen.width,Screen.height);
//	}

//	IEnumerator Start ()
//	{
//		buttons[buttonSelect].Over();
//
//		Color c = guiTexture.color;
//		c.a = 0f;
//		float lerp = 0f;
//
//		while(guiTexture.color.a > 0.01f)
//		{
//			lerp += Time.deltaTime * speedFade;
//			guiTexture.color = Color.Lerp(Color.black, c, lerp); 
//
//			yield return null;
//		}
//
//		GameObject.Destroy(guiTexture);
//		Screen.showCursor = true;
//		Screen.lockCursor = false;
//	}

	void Update ()
	{
		int temp_select = buttonSelect;
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
			buttonSelect++;
		else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
			buttonSelect--;

		if(buttonSelect != temp_select)
		{
			buttonSelect = Mathf.Clamp(buttonSelect, 0, buttons.Length-1);
			buttons[buttonSelect].Over();
		}

		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
			buttons[buttonSelect].Pressed();
	}
}
