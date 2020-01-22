using UnityEngine;
using System.Collections;

public class StartControl : MonoBehaviour
{

	public GameObject nave;
	public GameObject nave_c;
	public GameObject cam;
	public float startPush;

	// Use this for initialization
	void Start ()
	{
		iTween.MoveTo(nave, iTween.Hash(
			"x",0,
			"time",5,
			"easetype",iTween.EaseType.linear,
			"oncomplete","ActiveCam",
			"oncompletetarget",gameObject
		));
	}

	public void ActiveCam()
	{
		nave_c.GetComponent<NaveControl> ().isActive = true;
		cam.GetComponent<MoveCameraMiniGame>().isActive = true;
		nave.rigidbody2D.AddForce(nave.transform.right * startPush);
		GameObject.Destroy(gameObject);
	}

}

