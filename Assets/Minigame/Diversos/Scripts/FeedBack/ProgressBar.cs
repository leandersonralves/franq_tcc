using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour, IFeedback
{
	const Feedback feedback = Feedback.NoDiegetic;
	public Feedback Feedback {
		get {
			return feedback;
		}
	}

	private bool guiON;

	#region combustivel
	private float fuel;
	#endregion
	
	#region life
	private int life;
	#endregion

	#region progress
	public Sprite barramolde;
	public Sprite barracor;
	public Sprite barrafuel;
	public Sprite barrafundo;
	public Sprite simbolo;

	public Transform start;
	public Transform end;
	private float totalDistance;
	private GameObject player;
	#endregion

	// Use this for initialization
	void Start(){
		Singleton.feedback.Add(this as IFeedback);
		player = Singleton.player;

		totalDistance = Vector3.Distance(start.position,end.position);
	}

	void Update(){
		life = GetComponent<NaveControl>().Life;
		fuel = GetComponent<NaveControl>().Fuel;
	}

	void OnGUI(){
		if(guiON){
			//progress bar
			float width = barramolde.rect.width * 0.7F;
			float height = barramolde.rect.height * 0.5F;

			float widthbarra = barramolde.rect.width * 0.69F;

			float actualDistance = Vector3.Distance(player.transform.position,end.position);
			float actualWidth = 100F -((actualDistance * 100F)/totalDistance);

			GUI.DrawTexture(new Rect( (Screen.width - width)/2, (Screen.height - 80), width, height), barrafundo.texture, ScaleMode.StretchToFill, true, 0F);
			GUI.DrawTexture(new Rect( (Screen.width - widthbarra)/2, (Screen.height - 80), widthbarra * (actualWidth / 100F), height), barracor.texture, ScaleMode.StretchToFill, true, 0F);
			GUI.DrawTexture(new Rect( (Screen.width - width)/2, (Screen.height - 80), width, height), barramolde.texture, ScaleMode.StretchToFill, true, 0F);

			float simboloX = ((Screen.width - widthbarra)/2) + (widthbarra * (actualWidth / 100F)) - (simbolo.rect.width / 2);
			float simboloY = (Screen.height - 82);

			GUI.DrawTexture(new Rect( simboloX, simboloY, simbolo.rect.width, simbolo.rect.height), simbolo.texture, ScaleMode.StretchToFill, true, 0F);

			//fuel bar
			float widthfuel = barramolde.rect.width * 0.35F;
			float heightfuel = barramolde.rect.height * 0.25F;

			float widthbarrafuel = barramolde.rect.width * 0.345F;

			GUI.DrawTexture(new Rect( ((Screen.width - width)/2), 60, widthfuel, heightfuel), barrafundo.texture, ScaleMode.StretchToFill, true, 0F);
			GUI.DrawTexture(new Rect( ((Screen.width - width)/2), 60, widthbarrafuel * (fuel / 100F), heightfuel), barrafuel.texture, ScaleMode.StretchToFill, true, 0F);
			GUI.DrawTexture(new Rect( ((Screen.width - width)/2), 60, widthfuel, heightfuel), barramolde.texture, ScaleMode.StretchToFill, true, 0F);

			//lifes
			if(life >= 1) GUI.DrawTexture(new Rect( (Screen.width - width)/2, 20, simbolo.rect.width * 0.75F, simbolo.rect.height * 0.75F), simbolo.texture, ScaleMode.StretchToFill, true, 0F);
			if(life >= 2) GUI.DrawTexture(new Rect( ((Screen.width - width)/2 + simbolo.rect.width), 20, simbolo.rect.width * 0.75F, simbolo.rect.height * 0.75F), simbolo.texture, ScaleMode.StretchToFill, true, 0F);
			if(life >= 3) GUI.DrawTexture(new Rect( ((Screen.width - width)/2 + simbolo.rect.width * 2), 20, simbolo.rect.width * 0.75F, simbolo.rect.height * 0.75F), simbolo.texture, ScaleMode.StretchToFill, true, 0F);
		}
	}

	public void Enable(){
		guiON = true;
	}

	public void Disable(){
		guiON = false;
	}

}

