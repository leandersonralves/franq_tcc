using UnityEngine;
using System.Collections;

public class Button {
	public static KeyCode Up = KeyCode.None;
	public static KeyCode Down = KeyCode.None;
	public static KeyCode Left = KeyCode.None;
	public static KeyCode Right = KeyCode.None;
	public static KeyCode Jump = KeyCode.None;
	public static KeyCode Float = KeyCode.None;
	public static KeyCode Glide = KeyCode.None;
	public static KeyCode Liquify = KeyCode.None;
	public static KeyCode Dash = KeyCode.None;
	public static KeyCode Gas = KeyCode.None;
	public static KeyCode Defense = KeyCode.None;

	public static void SetStandards () {
		Button.SetArrowKey(KeyCode.LeftArrow);
		Button.SetDash(KeyCode.F);
		Button.SetDefense(KeyCode.A);
		Button.SetJump(KeyCode.Space);
		Button.SetFloat(KeyCode.Space);
		Button.SetGas(KeyCode.D);
		Button.SetGlide(KeyCode.S);
		Button.SetLiquify(KeyCode.DownArrow);
	}

	public static bool Horizontal {
		get {
			if(Input.GetKey(Left) || Input.GetKey(Right))
				return true;
			else
				return false;
		}
	}

	public static bool Vertical {
		get {
			if(Input.GetKey(Up) || Input.GetKey(Down))
				return true;
			else
				return false;
		}
	}

	public static bool SetArrowKey (KeyCode button) {
		if(button == KeyCode.UpArrow || 
		   button == KeyCode.DownArrow || 
		   button == KeyCode.LeftArrow || 
		   button == KeyCode.RightArrow
		   )
		{
			Up = KeyCode.UpArrow;
			Down = KeyCode.DownArrow;
			Left = KeyCode.LeftArrow;
			Right = KeyCode.RightArrow;
			
			return true;
		}
		else if(button == KeyCode.W || 
		        button == KeyCode.S || 
		        button == KeyCode.A || 
		        button == KeyCode.D
		        )
		{
			Up = KeyCode.W;
			Down = KeyCode.S;
			Left = KeyCode.A;
			Right = KeyCode.D;
			
			return true;
		}
		else
			return false;
	}

	public static bool SetJump (KeyCode button) {
		if(button != Down &&
		   button != Left &&
		   button != Right)
		{
			Jump = button;
			return true;
		}
		else
			return false;
	}

	public static bool SetLiquify (KeyCode button) {
		if((Left == KeyCode.LeftArrow && button == KeyCode.DownArrow) ||
		   (Left == KeyCode.A && button == KeyCode.S))
		{
			Liquify = button;
			return true;
		}
		else
			return false;
	}

	public static bool SetGas (KeyCode button) {
		if(button != Up && 
		   button != Down &&
		   button != Left &&
		   button != Right && 
		   button != Jump && 
		   button != Dash && 
		   button != Liquify)
		{
			Gas = button;
			return true;
		}
		else
			return false;
	}
	
	public static bool SetDash (KeyCode button) {
		if(button != Up && 
		   button != Down &&
		   button != Left &&
		   button != Right && 
		   button != Jump &&
		   button != Liquify &&
		   button != Gas)
		{
			Dash = button;
			return true;
		}
		else
			return false;
	}

	public static bool SetFloat (KeyCode button)
	{
		if(button == Jump)
		{
			Float = Jump;
			return true;
		}
		else
			return false;
	}
	
	public static bool SetDefense (KeyCode button) {
		if(button != Up && 
		   button != Down &&
		   button != Left &&
		   button != Right && 
		   button != Jump &&
		   button != Liquify &&
		   button != Gas &&
		   button != Dash &&
		   button != Glide)
		{
			Defense = button;
			return true;
		}
		else
			return false;
	}

	public static bool SetGlide (KeyCode button) {
		if(button != Up && 
		   button != Down &&
		   button != Left &&
		   button != Right && 
		   button != Jump &&
		   button != Liquify &&
		   button != Gas &&
		   button != Dash)
		{
			Glide = button;
			return true;
		}
		else
			return false;
	}
}
