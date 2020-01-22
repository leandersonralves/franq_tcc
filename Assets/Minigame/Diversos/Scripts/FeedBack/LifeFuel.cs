using UnityEngine;
using System.Collections;

public class LifeFuel : MonoBehaviour, IFeedback
{
	const Feedback feedback = Feedback.Diegetic;
	public Feedback Feedback {
		get {
			return feedback;
		}
	}

	private bool guiON = true;

	#region combustivel
	private float fuel;
	public GameObject fuelBar;
//	public GameObject fuelBarOff;
	#endregion
	
	#region life
	private int life;
	public GameObject nave_s1;
	public GameObject nave_s2;
	public GameObject nave_s3;
	#endregion

	void Start(){
		Singleton.feedback.Add(this as IFeedback);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(guiON){
			life = GetComponent<NaveControl>().Life;
			fuel = GetComponent<NaveControl>().Fuel;

			fuelBar.GetComponent<SpriteCutter>().fuelLevel = fuel;

			ChangeSprite();
		}
	}

	void ChangeSprite(){
		if(life == 3){
			nave_s1.SetActive(true);
			nave_s2.SetActive(false);
			nave_s3.SetActive(false);
		}else if(life == 2){
			nave_s1.SetActive(false);
			nave_s2.SetActive(true);
			nave_s3.SetActive(false);
		}else if(life == 1){
			nave_s1.SetActive(false);
			nave_s2.SetActive(false);
			nave_s3.SetActive(true);
		}
	}
	
	public void Enable(){
		ChangeSprite();
		fuelBar.SetActive(true);
//		fuelBarOff.SetActive(true);

		guiON = true;
	}
	
	public void Disable(){
		nave_s1.SetActive(true);
		nave_s2.SetActive(false);
		nave_s3.SetActive(false);

		fuelBar.SetActive(false);
//		fuelBarOff.SetActive(false);

		guiON = false;
	}
}

