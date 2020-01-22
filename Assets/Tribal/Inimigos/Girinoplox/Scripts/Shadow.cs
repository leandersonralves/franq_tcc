using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour
{

	private float alpha;
	public float increase;

	// Use this for initialization
	void Start ()
	{
		alpha = 0F;
		gameObject.GetComponent<SpriteRenderer>().color = new Color(0F,0F,0F,alpha);
	}

	// Update is called once per frame
	void Update ()
	{
		alpha += increase;
		gameObject.GetComponent<SpriteRenderer>().color = new Color(0F,0F,0F,alpha);
	}
}

