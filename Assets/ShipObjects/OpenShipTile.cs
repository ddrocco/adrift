using UnityEngine;
using System.Collections;
using UnityEditor;

public class OpenShipTile : ShipTile {
/* A constructed ShipTile with nothing on it.  Can be outfitted as a functional tile.

TODO(Derek): Add the ability to outfit to a functional tile.
*/

	public OpenShipTile(int x, int y) {
		coordinates.x = x;
		coordinates.y = y;
		coordinates.attached = true;
		SetPositionByCoordinates();
	}

	// Use this for initialization
	protected new void Start () {
		base.Start();
		color = Color.gray;
	}
	
	// Update is called once per frame
	protected new void Update () {
		base.Update();
	}

	public override EditorContextMenu AttachEditorContextMenu() {
		/* Attaches a context menu to this OpenShipTile.  Overridde of ShipTile.AttachEditorContextMenu. */
		print("Nailed it");
		return gameObject.AddComponent<OpenShipTileMenu>();
	}
}
