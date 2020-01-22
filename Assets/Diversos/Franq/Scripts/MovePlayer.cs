using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovePlayer : MonoBehaviour {

	static MovePlayer playerMove;

	public static bool canMove = true;

	public float forceWalk;
	public float maxWalkHor;

	public float forceJump;
	public float maxJumpVer;

	public float forceFallHor;
	public float maxFallHor;

	public float forceSwim;
	public float maxSwimVer;
	public float maxSwimHor;
	public float gravityInWater;

	public float normalGravity = 1f;

	public float forceFloatVer;
	public float forceFloatHor;
	public float amountEnergyFloat = 0.5f;
	public float maxFloatVer;
	public float maxFloatHor;

	public float gravityInGlide = 0.1f;
	public float amountEnergyGlide = 0.2f;

	private Transform m_transform;
	private Rigidbody2D m_rigidbody;
	private Animator m_animator;

	public LayerMask layerGround;
	public LayerMask layerDuct;

	public bool inGround = false;
	private bool inWater = false;
	public bool UseSkills {
		get {
			if(!inWater)
				return true;
			else
				return false;
		}
	}

	private bool inFall = false;

	private bool isJumping = false;
	private bool isFloating = false;

	private Cooldown coolDown;

	private bool canFloat = true;
	float timeToFloat = 0.25f;
	float timePressedJump = 0f;

	void Start ()
	{
		SceneManager.OnDie += Respawn;

		playerMove = this;

		coolDown = GetComponent<Cooldown>();

		m_animator = GetComponent<Animator>();
		m_transform = transform;
		m_rigidbody = rigidbody2D;
	}

	void Respawn ()
	{
		StartCoroutine(OutWater());
		m_rigidbody.velocity = Vector2.zero;
		m_animator.SetBool("InDie", false);
	}

	bool upJumpButton = false;

	void Update () {
//		bool cache_inGround = GroundBelow ();
//		if(inGround != cache_inGround)
//		{
//			if(cache_inGround)
//			{
//				isJumping = false;
//				Debug.Log("Triggou o InGround");
//				m_animator.SetTrigger("InGround");
//				Cooldown.canRecupair = true;
//			}
//			else if (!inWater)
//				Cooldown.canRecupair = false;
//		}
//		inGround = cache_inGround;
		if(inGround != GroundBelow())
		{
			inGround = GroundBelow();
			if(!inWater)
				Cooldown.canRecupair = inGround;
			if(inGround)
			{
				isJumping = false;
				m_animator.SetTrigger("InGround");
			}
			else
				m_animator.ResetTrigger("InGround");
		}

		if(!canMove)
		{ 
			m_animator.SetBool("Move", false);
			m_animator.SetBool("Float", false);
			m_animator.SetBool("Glide", false);
			return;
		}

		if(!inWater)
		{
			if(inFall &&
			   !inGround && 
			   Input.GetKey(Button.Glide) && 
			   coolDown.Expend(amountEnergyGlide)) {
				m_rigidbody.gravityScale = gravityInGlide;
				m_animator.SetBool("Glide", true);
			}
			else if (m_rigidbody.gravityScale != normalGravity){
				m_rigidbody.gravityScale = normalGravity;
				m_animator.SetBool("Glide", false);
			}
			else
			{
//				if(Input.GetKeyDown(Button.Float) && !inGround){
//					isFloating = true;
//					m_animator.SetBool("Float", isFloating);
//				}
//				else if (inGround || (isFloating && !Input.GetKey(Button.Float))){
//					isFloating = false;
//					m_animator.SetBool("Float", isFloating);
//				}

				if(Input.GetKey(Button.Float) && !inGround && !upJumpButton){
					timePressedJump += Time.deltaTime;
					if(timePressedJump > timeToFloat)
					{
						isFloating = true;
						m_animator.SetBool("Float", isFloating);
					}
				}
				else if (inGround || (isFloating && !Input.GetKey(Button.Float))){
					timePressedJump = 0f;
					isFloating = false;
					m_animator.SetBool("Float", isFloating);
				}

				if(Input.GetKeyDown(Button.Jump) && inGround && !isJumping && !m_animator.GetBool("Liquefy")){
					isJumping = true;
					upJumpButton = false;
					StartCoroutine(Jump());
					m_animator.SetTrigger("Jump");
				}

				if(Input.GetKeyUp(Button.Jump))
					upJumpButton = true;

				if(!inFall && m_rigidbody.velocity.y < -1f && !inGround)
				{
					inFall = true;
					m_animator.SetBool("Fall", inFall);
				}
				else if (inFall)
				{
					inFall = false;
					m_animator.SetBool("Fall", inFall);
				}
			}
		}
	}

	void LockPlayer ()
	{
		MovePlayer.LockPlayer(true);
	}

	void UnlockPlayer ()
	{
		MovePlayer.LockPlayer(false);
	}

	public static void LockPlayer (bool state){
		canMove = !state;
		playerMove.m_animator.SetBool("Float", false);
		playerMove.m_animator.SetBool("Liquefy", false);
		playerMove.m_animator.SetBool("Move", false);
		playerMove.m_animator.SetBool("Fall", false);
	}

	static List<string> lockPlayer = new List<string>();
	public static void LockPlayer (bool state, string type){
		if(state)
			lockPlayer.Add(type);
		else if(!lockPlayer.Contains(type) && !state)
			return;

		LockPlayer(state);
	}

	void FixedUpdate () {
		if(!canMove) return;

		string currentAnimation = string.Empty;
		if(m_animator.GetCurrentAnimationClipState(0).Length > 0)
			currentAnimation = m_animator.GetCurrentAnimationClipState(0)[0].clip.name;

		bool canWalk = currentAnimation.Contains("defense") || currentAnimation.Contains("dash") || currentAnimation.Contains("solid") || currentAnimation.Equals("liquid") ? false : true;
		float h = Button.Horizontal && canWalk ? Input.GetAxis("Horizontal") : 0f;

		if(h != 0)
			m_animator.SetBool("Move", true);
		else
			m_animator.SetBool("Move", false);

		if(inWater){
			if(Input.GetKey(Button.Jump) && !isJumping && !WaterAbove){
				isJumping = true;
				upJumpButton = false;
				MoveVertical(1, forceJump, maxJumpVer);
			}
			
			if(Input.GetKeyUp(Button.Jump))
				upJumpButton = true;

			MoveHorizontal(h, forceSwim, maxSwimHor);

			float v = Button.Vertical ? Input.GetAxis("Vertical") : 0f;
			if(!WaterAbove && v >= 0f && !isJumping)
			{
				v = 0f;
				MoveVertical(v, 0, 0.05f);
			}
			else if (!isJumping)
			{
				MoveVertical(v, forceSwim, maxSwimVer);
			}
		}
		else
		{
			if(isFloating){
				MoveHorizontal(h, forceFloatHor, maxFloatHor);
				
				if(coolDown.Expend(amountEnergyFloat))
					MoveVertical(1, forceFloatVer, maxFloatVer);
			}
			else if(inGround)
				MoveHorizontal(h, forceWalk, maxWalkHor);
			else if(inFall || isJumping)
				MoveHorizontal(h, forceFallHor, maxFallHor);
		}
	}

	IEnumerator Jump () {
		m_animator.ResetTrigger("InGround");
		yield return new WaitForSeconds(0.15f);
		m_animator.ResetTrigger("Jump");
		MoveVertical(1, forceJump, maxJumpVer);
	}

	private void MoveHorizontal (float direction, float force, float maximum) {
		#region Rotaciona o personagem
		if (direction < 0)
			m_transform.Turn(false);
		else if(direction > 0)
			m_transform.Turn(true);
		#endregion

		#region Aplica a força na direçao da movimentaçao
		Vector2 vectorDirection = Vector2.right * direction * force;
		m_rigidbody.AddForce(vectorDirection);
		#endregion

		#region Limita a Velocidade do Rigidbody
		Vector2 currentVelocity = m_rigidbody.velocity;
		currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maximum, maximum);
		m_rigidbody.velocity = currentVelocity;
		#endregion
	}

	private void MoveVertical (float direction, float force, float maximum)
	{
		#region Aplica a força na direçao da movimentaçao
		Vector2 vectorDirection = Vector2.up * direction * force;
		m_rigidbody.AddForce(vectorDirection);
		#endregion
		
		#region Limita a Velocidade do Rigidbody
		Vector2 currentVelocity = m_rigidbody.velocity;
		if(currentVelocity.y > maximum)
			currentVelocity.y = maximum;

		if(inWater && currentVelocity.y < -maximum)
			currentVelocity.y = -maximum;

		m_rigidbody.velocity = currentVelocity;
		#endregion
	}
	public float height = .45f;
	public float lateral = .45f;
	bool GroundBelow ()
	{
		Vector3 pos = m_transform.position;
		pos.y += .2f;
		float x = pos.x;
		for(float i = -1f; i <= 1f; i++){
			pos.x = x + i * lateral;
//			Collider2D collider = Physics2D.OverlapCircle(pos, .8f, layerGround);
//			if(collider == null)
//				collider = Physics2D.OverlapCircle(pos, .8f, layerDuct);
//
//			if(collider != null)
//				return true;

			RaycastHit2D hit = Physics2D.Raycast(pos, -Vector2.up, height, layerGround);
			if(hit.collider == null)
				hit = Physics2D.Raycast(pos, -Vector2.up, height, layerDuct);

			if(hit.collider != null && hit.collider.enabled && !hit.collider.isTrigger)
				return true;
		}

		return false;
	}

	bool WaterAbove
	{
		get {
			Collider2D[] hit = Physics2D.OverlapPointAll(m_transform.position + Vector3.up * 0.3f);
			for(int i = 0; i < hit.Length; i++)
				if(hit[i].CompareTag("Water"))
					return true;

			return false;
		}
	}
	#region Eventos de Colisao
//	float timeOut = 0f;
//	float timeToOut = 0.2f;
//	private void OnCollisionEnter2D (Collision2D hit) {
//		if(!inGround && string.Equals(hit.gameObject.tag, "Ground")){
//			inGround = true;
//			Cooldown.canRecupair = true;
//			if((Time.timeSinceLevelLoad - timeOut) >= timeToOut)
//				m_animator.SetTrigger("InGround");
//
//			isJumping = false;
//		}
//	}

//	private void OnCollisionExit2D (Collision2D hit) {
//		if(inGround && string.Equals(hit.gameObject.tag, "Ground")){
//			inGround = false;
//			timeOut = Time.timeSinceLevelLoad;
//		}
//	}

	private void OnTriggerStay2D (Collider2D hit)
	{
		if(!inWater && string.Equals(hit.gameObject.tag, "Water")){
			if(inOutWater)
				StopCoroutine(OutWater());

			inWater = true;
			isJumping = false;
			m_animator.SetBool("Fall", false);
			m_animator.SetBool("Glide", false);
			Cooldown.canRecupair = true;
			m_animator.SetTrigger("Water");
			m_rigidbody.Sleep();
			m_rigidbody.gravityScale = gravityInWater;
			m_animator.SetBool("Swim", inWater);
		}
	}

	private void OnTriggerExit2D (Collider2D hit)
	{
		if(inWater && string.Equals(hit.gameObject.tag, "Water") && !IsInvoking("OutWater"))
			StartCoroutine(OutWater());
	}

	bool inOutWater = false;
	IEnumerator OutWater ()
	{
		inOutWater = true;
		yield return new WaitForSeconds(0.15f);

		inOutWater = false;
		inWater = false;
		Cooldown.canRecupair = false;
		m_rigidbody.gravityScale = normalGravity;
		m_animator.SetBool("Swim", inWater);
	}
	#endregion
}
