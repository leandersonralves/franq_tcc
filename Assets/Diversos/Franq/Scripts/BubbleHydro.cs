using UnityEngine;
using System.Collections;

public class BubbleHydro : MonoBehaviour {

	public float constantVelocityHorizontal = 0.3f;
	public float constantVelocityUp = 0.3f;

	public float velocityHorizontal = 0.3f;
	public float velocityVertical = 0.3f;

	public float maxVelocity = 3f;

	public float timeToDestroy = 3f;
	float currentTime = 0f;

	public static bool canThrow = true;
	static GameObject bubble;
	Vector3 directionMove;

	void FixedUpdate () {
		bubble.transform.position += Vector3.up * constantVelocityUp * Time.deltaTime;
		bubble.transform.position += directionMove * constantVelocityHorizontal * Time.deltaTime;

		if(Input.GetKey(Button.Down) || Input.GetKey(Button.Up)){
			float v = Input.GetAxis("Vertical");
			bubble.rigidbody2D.AddForce(Vector3.up * v * velocityVertical * Time.deltaTime, ForceMode2D.Force);
//			bubble.transform.position += Vector3.up * v * velocityVertical;
		}

		if(Input.GetKey(Button.Left) || Input.GetKey(Button.Right)){
			float h = Input.GetAxis("Horizontal");
			bubble.rigidbody2D.AddForce(Vector3.right * h * velocityHorizontal * Time.deltaTime, ForceMode2D.Force);
//			bubble.transform.position += Vector3.right * h * velocityHorizontal;
		}

		if(bubble.rigidbody2D.velocity.magnitude > maxVelocity)
		{
			Vector3 currentVelocity = bubble.rigidbody2D.velocity;
			bubble.rigidbody2D.velocity = Vector3.ClampMagnitude(currentVelocity, maxVelocity);
		}

		currentTime += Time.deltaTime;
		if(currentTime >= timeToDestroy)
			bubble.SetActive(false);
	}

	public AudioClip sampleExplode;
	void OnDisable () {
		MovePlayer.LockPlayer(false, "Bubble");
		currentTime = 0f;

		MoveCamera.GoToPlayer();
		canThrow = true;

		AudioSource.PlayClipAtPoint(sampleExplode, transform.position);
	}

	public static void Throw (Vector3 direction, Vector3 position) {
		if(bubble == null)
			bubble = GameObject.Instantiate(Resources.Load("Prefabs/Gas")) as GameObject;
		else
			bubble.SetActive(true);

		bubble.transform.position = position;
		bubble.GetComponent<BubbleHydro>().directionMove = direction;

		MoveCamera.Follow = bubble.transform;

		canThrow = false;
	}

	public static void Deactive() {
		if(bubble != null && bubble.activeSelf)
			bubble.SetActive(false);
	}
}