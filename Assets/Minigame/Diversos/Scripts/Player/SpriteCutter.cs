using UnityEngine;
using System.Collections;

public class SpriteCutter : MonoBehaviour
{

//	private Texture2D spriteTexture;
//	private Rect spriteTextureRect;
//
//	private float spriteMaxWidth;
//	private float spriteMaxHeight;
//	private float spriteX;
//	private float spriteY;

	public float fuelLevel;
	Material materialFuel;

	// Use this for initialization
	void Start ()
	{
		materialFuel = renderer.material;
//		spriteTexture = gameObject.GetComponent<SpriteRenderer>().sprite.texture;
//		spriteTextureRect = gameObject.GetComponent<SpriteRenderer>().sprite.textureRect;
//
//		spriteMaxWidth = spriteTextureRect.width;
//		spriteMaxHeight = spriteTextureRect.height;
//		spriteX = spriteTextureRect.x;
//		spriteY = spriteTextureRect.y;
	}

	// Update is called once per frame
	void Update ()
	{
		Color c = materialFuel.color;
		c.a = fuelLevel / 100;
		c.a = Mathf.Clamp(c.a, 0f,0.989f);
		materialFuel.color = c;
//		float actualHeight = (spriteMaxHeight * fuelLevel) / 100F;
//		Rect actualRect = new Rect(spriteX,spriteY,spriteMaxWidth,actualHeight);
//
//		Sprite actualSprite = Sprite.Create(spriteTexture,actualRect, new Vector2(0,1), 300F);
//		gameObject.GetComponent<SpriteRenderer>().sprite = actualSprite;
	}

}

