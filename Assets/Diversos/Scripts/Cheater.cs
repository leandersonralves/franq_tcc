using UnityEngine;
using System.Collections;

public class Cheater : MonoBehaviour {

	bool isOn = false;

	public string fase_tutorial = "tutorial";
	public string fase_tribal = "tribal";
	public string fase_metal = "metal";
	public string fase_g0 = "gravzero";
	public string fase_minigame_1 = "minigame1";
	public string fase_minigame_2 = "minigame2";
	public string fase_minigame_3 = "minigame3";

	bool infiniteLife = false;
	bool infiniteCooldown = false; 

	LifePlayer lifePlayer;
	Cooldown cooldownPlayer;

	bool ready = false;

	IEnumerator Start ()
	{
		while(Singleton.player == null)
			yield return null;

		lifePlayer = LifePlayer.m_instance;
		cooldownPlayer = Cooldown.m_instance;

		ready = true;
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.F12))
		{
			isOn = !isOn;
			Screen.showCursor = isOn;
			Screen.lockCursor = !isOn;
		}
	}

	void OnGUI ()
	{
		if(!isOn)return;

		if(!ready)
		{
			GUI.Label(new Rect(50, 50, 200, 30), "Espere um instante o Jogo Carregar.");
			return;
		}

		if(GUI.Button(new Rect(50, 50, 200, 30), "Start Fase LABORATORIO"))
			Application.LoadLevel(fase_tutorial);
		if(GUI.Button(new Rect(50, 90, 200, 30), "Start Fase TRIBAL"))
			Application.LoadLevel(fase_tribal);
		if(GUI.Button(new Rect(50, 130, 200, 30), "Start Fase METAL"))
			Application.LoadLevel(fase_metal);
		if(GUI.Button(new Rect(50, 170, 200, 30), "Start Fase METAL"))
			Application.LoadLevel(fase_g0);
		if(GUI.Button(new Rect(50, 210, 200, 30), "Start Fase MINIGAME 1"))
			Application.LoadLevel(fase_minigame_1);
		if(GUI.Button(new Rect(50, 250, 200, 30), "Start Fase MINIGAME 2"))
			Application.LoadLevel(fase_minigame_2);
		if(GUI.Button(new Rect(50, 290, 200, 30), "Start Fase MINIGAME 3"))
			Application.LoadLevel(fase_minigame_3);

		if(!Application.loadedLevelName.Contains("minigame"))
		{
			infiniteLife = GUI.Toggle(new Rect(50, 330, 200, 15), infiniteLife, "Vida Infinita");
			if(infiniteLife != lifePlayer.infiniteLife)
				lifePlayer.infiniteLife = infiniteLife;

			infiniteCooldown = GUI.Toggle(new Rect(50, 370, 200, 15), infiniteCooldown, "Cooldown Infinita");
			if(infiniteCooldown != cooldownPlayer.infiniteCooldown)
				cooldownPlayer.infiniteCooldown = infiniteCooldown;
		}
		else
		{
			infiniteLife = GUI.Toggle(new Rect(50, 330, 200, 15), infiniteLife, "Vida Infinita");
			if(infiniteLife != NaveControl.infiniteLife)
				NaveControl.infiniteLife = infiniteLife;

			infiniteCooldown = GUI.Toggle(new Rect(50, 370, 200, 15), infiniteCooldown, "Combustivel Infinita");
			if(infiniteCooldown != NaveControl.infiniteFuel)
				NaveControl.infiniteFuel = infiniteCooldown;
		}

		if(GUI.Button(new Rect(50, 410, 200, 30), "Proximo Checkpoint"))
			CheckPoint.ChangeCheckpoint(true);

		if(GUI.Button(new Rect(50, 450, 200, 30), "Checkpoint Anterior"))
			CheckPoint.ChangeCheckpoint(false);

		if(GUI.Button(new Rect(50, 490, 200, 30), "Definir botoes Padroes"))
			Button.SetStandards();

		if(GUI.Button(new Rect(50, 530, 200, 30), "Lock/Unlock Player"))
		{
			if(MovePlayer.canMove)
				MovePlayer.LockPlayer(true);
			else
				MovePlayer.LockPlayer(false);
		}
	}
}
