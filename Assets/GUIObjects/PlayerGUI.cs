using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEditorInternal;

public class PlayerGUI : MonoBehaviour {
	public static PlayerGUI main;
	public Image navpad;
	public Image navbead;
	public Text navcoords;
	public Text scrapmetal;
	public Text mode;

	public enum GUIState {
		playing,  // For regular use.  Minimal GUI menus should appear.
		constructing,  // The 'main' construction menu.  Allows construction of new tiles.
		editing  // Selecting a modification of a current tile.
	};
	public GUIState guistate;

	/* When guistate is 'editing', this will be set to the tile in question. */
	public EditorContextMenu menu;

	// Use this for initialization
	void Start () {
		main = this;
		guistate = GUIState.playing;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetNavigationBead(float x, float y) {
		navbead.transform.localPosition = new Vector2(
				navpad.rectTransform.rect.width * x / 2f,
				navpad.rectTransform.rect.height * y / 2f);
		navcoords.text = "(" + x.ToString() + ", " + y.ToString() + ")";
	}

	public void ToggleEditState() {
		/* Called when a ShipTile is clicked by the player.

		If the state is 'playing', set state to 'constructing' and show construction GUI elements.
		If the state is 'constructing', set state to 'playing' and hide construction GUI elements.
		If the state is 'editing', something went wrong; that shouldn't ever happen.
		*/
		switch (guistate) {
		case GUIState.constructing:
			guistate = GUIState.playing;
			break;
		case GUIState.playing:
			guistate = GUIState.constructing;
			break;
		case GUIState.editing:
			throw new Exception("GUIState toggled during editing mode!  This shouldn't happen.");
		}
	}

	public void ShipTileClicked(ShipTile shiptile) {
		/* Called when a ShipTile is clicked by the player.

		If the state is 'playing', ignore this.
		If the state is 'constructing', set state to 'editing' and create an editor context.
		If the state is 'editing', something went wrong; that shouldn't ever happen.
		*/
		if (guistate == GUIState.playing) {
			return;
		} else if (guistate == GUIState.constructing) {
			guistate = GUIState.editing;
			menu = shiptile.AttachEditorContextMenu();
			ShipTileEditor.main.SetConstructableTileVisibility(false);
		} else if (guistate == GUIState.editing) {
			throw new Exception("ShipTile clicked during 'editing' phase; this shouldn't happen.");
		}
	}

	public void ShipTileDestroyed() {
		/* Called when a ShipTile is destroyed for any reason.

		If the state is not 'editing', something went wrong.  Fix that error.
		If the state is 'editing', sets the state to constructing and cleans up editing GUI.
		*/
		if (guistate != GUIState.editing) {
			throw new Exception("ShipTile clicked during 'editing' phase; this shouldn't happen.");
		} else {
			guistate = GUIState.constructing;
			menu = null;
			ShipTileEditor.main.SetConstructableTileVisibility(true);
		}
	}
}
