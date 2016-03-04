using UnityEngine;
using System.Collections;

public class ShipTile : MonoBehaviour {

	/* Public interfaces */

	public struct Coordinates {
		public int x;
		public int y;
		public bool attached;
	};
	public Coordinates coordinates;

	public void SetPositionByCoordinates() {
		Vector2 v2 = new Vector2(coordinates.x, coordinates.y);
		transform.localPosition = v2.vector3();
	}

	/* Default interfaces */

	public ShipTile() {
		/* Default constructor for ABC ShipTile. Will only be called as a filler in datastructures. */
		coordinates.attached = false;
	}

	/* Virtual methods */

	protected Color color;

	protected virtual void Start () {
		color = Color.black;
	}

	protected virtual void Update () {
		gameObject.GetComponent<Renderer>().material.color = color;
		if (GetComponents<ShipTile>().Length > 1) {
			throw new UnityException("Too many object controllers for object!");
		}
	}
}
