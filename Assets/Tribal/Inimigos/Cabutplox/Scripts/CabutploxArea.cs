using UnityEngine;
using System.Collections;

public class CabutploxArea : MonoBehaviour
{

	public float shotCooldown;
	private float cooldown;

	void Start(){
		cooldown = shotCooldown;
	}

	void OnTriggerStay2D( Collider2D obj ){

		if( obj.gameObject.tag == "Player" ){

			Debug.Log("player = " + cooldown);

			gameObject.GetComponentInParent<CabutploxBackControl>().SetShotX( obj.gameObject.transform.position.x );

			if( cooldown >= 0F ){
				cooldown -= Time.deltaTime;
			}else{
				gameObject.GetComponentInParent<CabutploxBackControl>().Atacar();
				cooldown = shotCooldown;
			}

		}

	}

	void OnTriggerExit2D( Collider2D obj ){
		if( obj.gameObject.tag == "Player" ) cooldown = shotCooldown;
	}

}

