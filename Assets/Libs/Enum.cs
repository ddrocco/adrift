﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Direction {
	north,
	east,
	south,
	west
};
	
public static class DirectionExtensions {
	public static Vector2 vector2(this Direction direction) {
		switch(direction) {
			case Direction.north: return Vector2.up;
			case Direction.east: return Vector2.right;
			case Direction.south: return -Vector2.up;
			case Direction.west: return -Vector2.right;
			default: return Vector2.zero;
		}
	}
	
	public static Quaternion quaternion (this Direction direction) {
		switch(direction) {
			case Direction.north: return Quaternion.Euler(new Vector3(90f, 0, 0));
			case Direction.east: return Quaternion.Euler(new Vector3(0, 270f, 90f));
			case Direction.south: return Quaternion.Euler(new Vector3(270f, 0, 0));
			case Direction.west: return Quaternion.Euler(new Vector3(0, 90f, 90f));
			default: return Quaternion.identity;
		}
	}
}
