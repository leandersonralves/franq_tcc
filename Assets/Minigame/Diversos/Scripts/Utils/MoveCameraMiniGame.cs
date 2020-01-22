using UnityEngine;
using System.Collections;

public class MoveCameraMiniGame : MonoBehaviour
{

	public Transform target;
	public bool isActive;

	public bool guiON;

	public float camSize;

	void Update (){

		if(isActive){
			if (target) transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
		}

		gameObject.camera.orthographicSize = camSize;
		
	}

	void OnGUI() {
		if(guiON){
			GUI.Label(new Rect(25, 310, 300, 20), "Tamanho da Tela (" + camSize + ")");
			camSize = GUI.HorizontalSlider(new Rect(25, 330, 100, 30), camSize, 1F, 10F);
		}
	}

}

