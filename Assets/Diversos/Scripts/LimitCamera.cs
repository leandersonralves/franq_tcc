using UnityEngine;
using System.Collections;

public enum Direction {
	Horizontal,
	Vertical
}

public enum TypeLimit {
	Maximum,
	Minimun,
	Lock
}

public class LimitCamera : MonoBehaviour {
	public Direction direction = Direction.Horizontal;
	public TypeLimit type = TypeLimit.Lock;
}
