using UnityEngine;
using System.Collections;
using System;

public class TurretShipTile : ShipTile {
	/* TODO(Derek): Remove this and move some vestiges over to Ian's Cannon. */

	// Use this for initialization
	protected new void Start () {
		base.Start();
		color = Color.red;
	}
	
	// Update is called once per frame
	protected new void Update () {
		base.Update();
	}

	public override EditorContextMenu AttachEditorContextMenu() {
		/* Attaches a context menu to this ShipTile.  Overridden by each child.

		This virtual implementation should never surface; it should always be overridden. */
		throw new NotImplementedException("No override exists for TurretShipTile type for AttachEditorContextMenu!");
	}
}
