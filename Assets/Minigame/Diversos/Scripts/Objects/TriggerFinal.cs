using UnityEngine;
using System.Collections;

public class TriggerFinal : MonoBehaviour
{

	public GameObject m_camera;
	public GameObject player;

	public string nextLevel;

	private bool trigger;
	private float timer = 6F;
	
	void OnTriggerEnter2D( Collider2D obj ) {
		if( obj.CompareTag("Player")){			
			m_camera.GetComponent<MoveCameraMiniGame>().isActive = false;
			player.GetComponentInChildren<NaveControl>().isActive = false;

			trigger = true;
		}
	}

	void Update(){
		if( trigger ){
			if( timer > 0 ){
				timer -= Time.deltaTime;
			}else{
				SceneManager.LoadLevel(nextLevel);
//				Application.LoadLevel(nextLevel);
			}
		}
	}

}

