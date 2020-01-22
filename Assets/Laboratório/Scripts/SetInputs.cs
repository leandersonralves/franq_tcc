using UnityEngine;
using System.Collections;

public enum Command {
	Arrow,
	Jump,
	Liquid,
	Gas,
	Dash,
	Defense,
	Glide,
	Float
}

public class SetInputs : MonoBehaviour, IFeedback {

	Feedback feedback = Feedback.NoDiegetic;
	public Feedback Feedback {
		get {
			return feedback;
		}
	}

	bool noDiegetic = true;
	public void Enable ()
	{
		noDiegetic = true;
	}
	public void Disable ()
	{
		noDiegetic = false;
	}


	public Command command;
	public string tagCollider;
	private bool initialPlay = false;
	private bool canDefine = false;

	public AudioSource source;

	public Sprite[] spritesTextInfos = new Sprite[]{};

	bool isSet = false;

	void Start ()
	{
		FeedbackManager.instance.Add((IFeedback)this);

		SceneManager.OnDie += Respawn;
	}

	void Respawn ()
	{
		if(Tutorial.CheckPressed != null)
			Tutorial.CheckPressed = null;

		if(isSet)
		{
			bool reset = false;

			switch(command){
			case Command.Arrow:
				if(Button.Left == KeyCode.None)
					reset = true;
				break;
				
			case Command.Dash:
				if(Button.Dash == KeyCode.None)
					reset = true;
				break;
				
			case Command.Defense:
				if(Button.Defense == KeyCode.None)
					reset = true;
				break;
				
			case Command.Gas:
				if(Button.Gas == KeyCode.None)
					reset = true;
				break;
				
			case Command.Jump:
				if(Button.Jump == KeyCode.None)
					reset = true;
				break;
				
			case Command.Liquid:
				if(Button.Liquify == KeyCode.None)
					reset = true;
				break;
				
			case Command.Glide:
				if(Button.Glide == KeyCode.None)
					reset = true;
				break;
				
			case Command.Float:
				if(Button.Float == KeyCode.None)
					reset = true;
				break;
			}

			if(reset)
			{
				isSet = false;
				canDefine = false;
			}
		}
	}

	void Update () {
		if(isSet)
			return;

		if(source == null)
		{
			canDefine = false;
			return;
		}

		if(!initialPlay && !source.isPlaying)
			return;

		initialPlay = true;

		if(!source.isPlaying)
			canDefine = true;
	}


	void OnTriggerStay2D (Collider2D hit) {
		if(!canDefine || !string.Equals(hit.tag, tagCollider) || isSet)
		   return;

		StartCoroutine(QueueInput());
		
		isSet = true;
	}

	IEnumerator QueueInput ()
	{
		while(Tutorial.CheckPressed != null)
			yield return null;

		switch(command){
		case Command.Arrow:
			Tutorial.CheckPressed = Tutorial.ArrowKey;
			break;
			
		case Command.Dash:
			Tutorial.CheckPressed = Tutorial.DashKey;
			break;
			
		case Command.Defense:
			Tutorial.CheckPressed = Tutorial.DefenseKey;
			break;
			
		case Command.Gas:
			Tutorial.CheckPressed = Tutorial.GasKey;
			break;
			
		case Command.Jump:
			Tutorial.CheckPressed = Tutorial.JumpKey;
			break;
			
		case Command.Liquid:
			Tutorial.CheckPressed = Tutorial.LiquifyKey;
			break;
			
		case Command.Glide:
			Tutorial.CheckPressed = Tutorial.Glide;
			break;
			
		case Command.Float:
			Tutorial.CheckPressed = Tutorial.Float;
			break;
		}

		if(noDiegetic)
			SetSprite();
	}

	void SetSprite () {
		switch(command){
		case Command.Jump:
			if(Button.Left == KeyCode.LeftArrow)
				HUDController.SetInfo(spritesTextInfos[0]);
			else if (Button.Left == KeyCode.A)
				HUDController.SetInfo(spritesTextInfos[1]);
			break;
			
		case Command.Liquid:
			if(Button.Left == KeyCode.LeftArrow)
				HUDController.SetInfo(spritesTextInfos[0]);
			else if (Button.Left == KeyCode.A)
				HUDController.SetInfo(spritesTextInfos[1]);
			break;

		case Command.Float:
			if(Button.Jump == KeyCode.UpArrow)
				HUDController.SetInfo(spritesTextInfos[0]);
			else if (Button.Left == KeyCode.D)
				HUDController.SetInfo(spritesTextInfos[1]);
			else if (Button.Left == KeyCode.Space)
				HUDController.SetInfo(spritesTextInfos[2]);
			break;

		default:
			HUDController.SetInfo(spritesTextInfos[0]);
			break;
		}
	}
}
