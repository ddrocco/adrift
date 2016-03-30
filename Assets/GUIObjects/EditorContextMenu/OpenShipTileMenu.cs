using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenShipTileMenu : EditorContextMenu {
	public OpenShipTileMenu() {
	/* Override of constructor for ABC EditorContextMenu. Populates 'actions'. */
		actions = new List<EditorContextMenuAction>();
		actions.Add(new ConstructMotor());
		actions.Add(new ConstructStockpile());
		actions.Add(new ConstructCannon());
	}
}
