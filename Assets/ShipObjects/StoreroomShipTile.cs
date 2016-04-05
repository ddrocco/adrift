using UnityEngine;
using System.Collections;
using System;

public class StoreroomShipTile : ShipTile {
/* A constructed ShipTile which can store fuel, ammunition, and food.

Not yet implemented (functionally an OpenTile). */

	protected new void Start () {
		base.Start();
		color = Color.green;
	}

	protected new void Update () {
		base.Update();
	}

	public override EditorContextMenu AttachEditorContextMenu() {
		/* Attaches a context menu to this ShipTile.  Overridden by each child.

		This virtual implementation should never surface; it should always be overridden. */
		throw new NotImplementedException("No override exists for StoreroomShipTile type for AttachEditorContextMenu!");
	}
}
