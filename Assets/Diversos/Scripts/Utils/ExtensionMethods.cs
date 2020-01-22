using UnityEngine;
using System.Collections;

public static class ExtensionMethods {
	public static bool IsRight (this Transform t)
	{
		if(t.localScale.x > 0)
			return true;
		else
			return false;
	}

	public static void Turn (this Transform t, bool isRight)
	{
		if((isRight && t.localScale.x < 0f) || (!isRight && t.localScale.x > 0f))
			t.localScale = new Vector3(t.localScale.x * -1f, t.localScale.y, t.localScale.z);
	}
}
