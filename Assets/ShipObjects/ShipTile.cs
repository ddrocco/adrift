using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.ComponentModel;
using System;
using System.Security.Cryptography.X509Certificates;

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

	public void OnMouseEnter() {
		Debug.Log("Mouse Enter at (" + coordinates.x + ", " + coordinates.y + ")");
	}

	public void OnMouseExit() {
		Debug.Log("Mouse Exit at (" + coordinates.x + ", " + coordinates.y + ")");
	}

	public void OnMouseDown() {
		/* Called when clicked by the player's mouse. */
		Debug.Log("Mouse Down at (" + coordinates.x + ", " + coordinates.y + ")");
		PlayerGUI.main.ShipTileClicked(this);
	}

	public virtual EditorContextMenu AttachEditorContextMenu() {
		/* Attaches a context menu to this ShipTile.  Overridden by each child.

		This virtual implementation should never surface; it should always be overridden. */
		throw new NotImplementedException("No override exists for this ShipTile type for AttachEditorContextMenu!");
	}
}
