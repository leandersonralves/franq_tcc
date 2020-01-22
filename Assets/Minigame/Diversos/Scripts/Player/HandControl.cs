using UnityEngine;
using System.Collections;

public class HandControl : MonoBehaviour
{

	public GameObject hand;

	public GameObject p1;
	public GameObject p2;
	public GameObject p3;

	private int atual;
	private float maxScale;

	private bool move;

	// Use this for initialization
	void Start ()
	{
		maxScale = 1.5F;
		atual = 1;
		move = true;
	}

	// Update is called once per frame
	void Update ()
	{
		Command();
		Control();

		NextLevel();
	}

	private void NextLevel(){
		if(Input.GetKeyUp(KeyCode.A)){
			Application.LoadLevel(2);
		}
	}

	private void Command(){

		if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.RightArrow)){
			atual--;
			move = true;
			
			if(atual < 1) atual = 3;
		}
		if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow)){
			atual++;
			move = true;
			
			if(atual > 3) atual = 1;
		}

	}

	private void Control(){
		switch(atual){
			case 1:
				if(move){
					iTween.MoveTo(hand, iTween.Hash(
						"time",2,
						"x",p1.transform.position.x,
						"y",p1.transform.position.y,
						"easetype",iTween.EaseType.easeInOutCubic
					));
					p1.transform.FindChild("particle").gameObject.SetActive(true);
					p2.transform.FindChild("particle").gameObject.SetActive(false);
					p3.transform.FindChild("particle").gameObject.SetActive(false);

					p1.transform.localScale = new Vector3(maxScale,maxScale,maxScale);
					p2.transform.localScale = new Vector3(1,1,1);
					p3.transform.localScale = new Vector3(1,1,1);
				}
			break;
			
			case 2:
				if(move){
					iTween.MoveTo(hand, iTween.Hash(
						"time",2,
						"x",p2.transform.position.x,
						"y",p2.transform.position.y,
						"easetype",iTween.EaseType.easeInOutCubic
						));
					p2.transform.FindChild("particle").gameObject.SetActive(true);
					p1.transform.FindChild("particle").gameObject.SetActive(false);
					p3.transform.FindChild("particle").gameObject.SetActive(false);

					p2.transform.localScale = new Vector3(maxScale,maxScale,maxScale);
					p1.transform.localScale = new Vector3(1,1,1);
					p3.transform.localScale = new Vector3(1,1,1);
				}
			break;
			
			case 3:
				if(move){
					iTween.MoveTo(hand, iTween.Hash(
						"time",2,
						"x",p3.transform.position.x,
						"y",p3.transform.position.y,
						"easetype",iTween.EaseType.easeInOutCubic
						));
					p3.transform.FindChild("particle").gameObject.SetActive(true);
					p2.transform.FindChild("particle").gameObject.SetActive(false);
					p1.transform.FindChild("particle").gameObject.SetActive(false);

					p3.transform.localScale = new Vector3(maxScale,maxScale,maxScale);
					p2.transform.localScale = new Vector3(1,1,1);
					p1.transform.localScale = new Vector3(1,1,1);
				}
			break;
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(25, 300, 300, 20), "Mova com as setas");
		GUI.Label(new Rect(25, 400, 300, 20), "Aperte A para proxima tela");
	}

}

