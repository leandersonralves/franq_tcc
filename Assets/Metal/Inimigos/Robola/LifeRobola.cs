using UnityEngine;
using System.Collections;

public class LifeRobola : Life {
	protected override void Die ()
	{
		GameObject.Destroy(gameObject);
	}
}
