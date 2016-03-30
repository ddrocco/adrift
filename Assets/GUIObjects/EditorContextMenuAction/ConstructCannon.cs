using UnityEngine;
using System.Collections;

public class ConstructCannon : EditorContextMenuAction {
	/* Public interfaces */

	/* Default override interfaces */

	public ConstructCannon() {
		color = Color.red;
	}

	public override void PerformAction() {
		/* Default action.  Should be overwritten by children. */
		system.startColor = Color.magenta;
	}

	/* Virtual inherited methods */

	/* Private methods */
}
