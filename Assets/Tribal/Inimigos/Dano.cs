using UnityEngine;
using System.Collections;

public class Dano : MonoBehaviour
{
	private GameObject player;

	public string tagCollider = "Player";
	public float damage = 0f;

	public bool applyForce = false;
	public float force = 20f;

	public bool autoDestroy = false;

	void Start ()
	{
		if(player == null)
			player = GameObject.FindWithTag ("Player");
	}

	public void Ataque(float dmg){
		if(!Skills.inDefense)
			player.GetComponent<Life> ().Health -= dmg;
	}

	void OnTriggerEnter2D (Collider2D hit)
	{
		if(!this.enabled) return;

		if(hit.CompareTag(tagCollider) && hit.GetComponent<Life>() != null)
		{
			if(hit.GetComponent<Skills>() && Skills.inDefense)
				return;

			hit.GetComponent<Life>().Health -= damage;
			if(applyForce)
			{
				hit.rigidbody2D.velocity = Vector2.zero;
				hit.rigidbody2D.AddForce((hit.transform.position - transform.position).normalized * force);
			}
			
			if(autoDestroy)
				GameObject.Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D hit)
	{
		if(!this.enabled) return;

		if(hit.collider.CompareTag(tagCollider) && hit.collider.GetComponent<Life>() != null)
		{
			if(hit.collider.GetComponent<Skills>() && Skills.inDefense)
				return;

			hit.collider.GetComponent<Life>().Health -= damage;

			if(applyForce)
			{
				hit.collider.rigidbody2D.velocity = Vector2.zero;
				hit.collider.rigidbody2D.AddForce((hit.collider.transform.position - transform.position).normalized * force);
			}
			
			if(autoDestroy)
				GameObject.Destroy(gameObject);
		}
	}
}

