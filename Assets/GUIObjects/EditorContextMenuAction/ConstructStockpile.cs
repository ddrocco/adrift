using UnityEngine;
using System.Collections;

public class ConstructStockpile : EditorContextMenuAction {
	/* Public interfaces */

	/* Default override interfaces */

	public ConstructStockpile() {
		color = Color.green;
	}

	public override void PerformAction() {
		/* Default action.  Should be overwritten by children. */
		system.startColor = Color.magenta;
	}

	/* Virtual inherited methods */

	/* Private methods */
}
