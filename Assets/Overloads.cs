using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Vector3Overloads {
	public static Vector2 vector2(this Vector3 v3) {
		return new Vector2(v3.x, v3.y);
	}
}
