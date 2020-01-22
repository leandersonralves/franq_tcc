using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	
	public bool isTutorial = true;

	public delegate void CheckButton(KeyCode button);
	public static CheckButton CheckPressed;
	
	float timePress = 0f;

	IEnumerator Start () {
		Singleton.audio.Play(AudioList.speech_ListenMe);
		Singleton.audio.PlayQueue(AudioList.tutorial_Walk);

		while(Singleton.audio.audioPlaying == AudioList.tutorial_Walk || 
		      Singleton.audio.audioPlaying == AudioList.speech_ListenMe)
			yield return null;
	}

	void OnGUI () {
		if(Input.anyKeyDown)
		{
			KeyCode buttonPressed = Event.current.keyCode;
			
			if(Time.timeSinceLevelLoad != timePress)
			{
				timePress = Time.timeSinceLevelLoad;

				if(CheckPressed != null)
					CheckPressed(buttonPressed);
			}
		}
	}
	
	public static void ArrowKey (KeyCode buttonPressed) {
		if(Button.SetArrowKey(buttonPressed))
			CheckPressed = null;
		else
			Singleton.audio.PlayQueue(AudioList.tutorial_Wrong, false);
	}

	public static void Glide (KeyCode buttonPressed) {
		if(Button.SetGlide(buttonPressed))
			CheckPressed = null;
		else
			Singleton.audio.PlayQueue(AudioList.tutorial_Wrong, false);
	}

	public static void JumpKey (KeyCode buttonPressed) {
		if(Button.SetJump(buttonPressed))
			CheckPressed = null;
		else
			Singleton.audio.PlayQueue(AudioList.tutorial_Wrong2, false);
	}

	public static void Float (KeyCode buttonPressed)
	{
		if(Button.SetFloat(buttonPressed))
			CheckPressed = null;
		else
			Singleton.audio.PlayQueue(AudioList.tutorial_Wrong2, false);
	}

	public static void DashKey (KeyCode buttonPressed) {
		if(Button.SetDash(buttonPressed))
			CheckPressed = null;
		else
			Singleton.audio.PlayQueue(AudioList.tutorial_Wrong, false);
	}

	public static void LiquifyKey (KeyCode buttonPressed) {
		if(Button.SetLiquify(buttonPressed))
			CheckPressed = null;
		else
			Singleton.audio.PlayQueue(AudioList.tutorial_Wrong2, false);
	}

	public static void GasKey (KeyCode buttonPressed) {
		if(Button.SetGas(buttonPressed))
			CheckPressed = null;
		else
			Singleton.audio.PlayQueue(AudioList.tutorial_Wrong, false);
	}

	public static void DefenseKey (KeyCode buttonPressed) {
		if(Button.SetDefense(buttonPressed))
			CheckPressed = null;
		else
			Singleton.audio.PlayQueue(AudioList.tutorial_Wrong, false);
	}

	public void FinishTutorial (KeyCode buttonPressed) {
		isTutorial = false;
	}
}
