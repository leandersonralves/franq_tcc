using UnityEngine;
using System.Collections;

public class DetectMouseOver : MonoBehaviour
{

	public GameObject p1;
	public GameObject p2;
	public GameObject p3;

	private float maxScale = 1.5F;

	void Update()
	{

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
		
		if(hit != null && hit.collider != null){

			string tag = hit.collider.gameObject.tag;

			if(p1.tag == tag){
				p1.transform.FindChild("particle").gameObject.SetActive(true);				
				p1.transform.localScale = new Vector3(maxScale,maxScale,maxScale);
			}

			if(p2.tag == tag){
				p2.transform.FindChild("particle").gameObject.SetActive(true);				
				p2.transform.localScale = new Vector3(maxScale,maxScale,maxScale);
			}

			if(p3.tag == tag){
				p3.transform.FindChild("particle").gameObject.SetActive(true);				
				p3.transform.localScale = new Vector3(maxScale,maxScale,maxScale);
			}

		}else{
			p1.transform.FindChild("particle").gameObject.SetActive(false);				
			p1.transform.localScale = new Vector3(1,1,1);

			p2.transform.FindChild("particle").gameObject.SetActive(false);				
			p2.transform.localScale = new Vector3(1,1,1);

			p3.transform.FindChild("particle").gameObject.SetActive(false);				
			p3.transform.localScale = new Vector3(1,1,1);
		}

	}
	
}

