using UnityEngine;
using System.Collections;

public class SendMessageTrigger : MonoBehaviour {

	public string tagColliderContact = string.Empty;
	public string functionName = string.Empty;
	public GameObject objectReceiver;

	public Sprite sprite;
	private SpriteRenderer spriteRenderer;

	void Awake () {
		if(string.IsNullOrEmpty (tagColliderContact) || string.IsNullOrEmpty (functionName) || objectReceiver == null)
			GameObject.Destroy (this);

		if(sprite != null)
			spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnTriggerEnter2D (Collider2D hit) {
		if(hit.collider2D.CompareTag(tagColliderContact)) {
			if(sprite != null)
				spriteRenderer.sprite = sprite;

			objectReceiver.SendMessage(functionName);
			GameObject.Destroy(hit.gameObject);
		}
	}
}
