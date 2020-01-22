using UnityEngine;
using System.Collections;

public class DefineButtons_TESTE : MonoBehaviour {

	bool isDefined = false;

	void Update () {
		if(isDefined || !Debug.isDebugBuild)
			return;

		isDefined = true;
		Button.SetArrowKey(KeyCode.LeftArrow);
		Button.SetDash(KeyCode.F);
		Button.SetGas(KeyCode.D);
		Button.SetJump(KeyCode.Space);
		Button.SetLiquify(KeyCode.DownArrow);
		Button.SetGlide(KeyCode.S);
	}
}
