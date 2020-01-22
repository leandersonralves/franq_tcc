using UnityEngine;
using System.Collections;

public class InCamera : MonoBehaviour {

	public bool inView;

	void OnBecameVisible () {
		if(inView)
			return;

		inView = true;
	}

	void OnBecameInvisible () {
		if(!inView)
			return;
		
		inView = false;
	}
}
