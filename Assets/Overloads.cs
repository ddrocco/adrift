using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Vector3Overloads {
	public static Vector2 vector2(this Vector3 v3) {
		return new Vector2(v3.x, v3.y);
	}
}

public static class Vector2Overloads {
	public static Vector3 vector3(this Vector2 v2) {
		return new Vector3(v2.x, v2.y, 0);
	}
}