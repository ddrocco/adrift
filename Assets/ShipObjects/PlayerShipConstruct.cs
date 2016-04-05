using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class PlayerShipConstruct : ShipConstruct {
/* A ShipConstruct which is controlled by the Player.

The camera follows this Ship.

Integrated with various UI objects.
*/
	PlayerGUI _playergui;
	PlayerGUI playergui {
		get {
			if (!_playergui) {
				_playergui = PlayerGUI.main;
			}
			return _playergui;
		}
	}

	ShipTileEditor _ship_tile_editor;
	ShipTileEditor ship_tile_editor {
		get {
			if (!_ship_tile_editor) {
				_ship_tile_editor = FindObjectOfType<ShipTileEditor>();
			}
			return _ship_tile_editor;
		}
	}

	// Remove public nature of scrapmetal and pick a new default.
	public int scrapmetal = 500;
	// TODO(Derek): Remove the 'in placement mode' bool and switch over to an enum StateMachine.  (PlayerGUI?).
	public bool in_placement_mode = false;

	protected new void Start () {
		base.Start();
		playergui.scrapmetal.text = scrapmetal.ToString();
		SetPlacementModeText();
	}
	
	protected new void Update () {
	/* In addition to ShipConstruct Update(), listens for mode swaps.

	Swaps mode between Normal (firing) and Placement on P-keypress.
	*/
		base.Update();
		if (Input.GetKeyDown(KeyCode.P)) {
			if (PlayerGUI.main.guistate == PlayerGUI.GUIState.editing) {
				// Since editing is in progress, the menu can't be dropped.
				// Consider maybe causing this to drop all editing contexts?  IDK.
				return;
			}
			in_placement_mode = !in_placement_mode;
			SetPlacementModeText();
			ShipTileEditor.main.SetConstructableTileVisibility(in_placement_mode);
			PlayerGUI.main.ToggleEditState();
		}

	}

	void SetPlacementModeText() {
	/* TODO(Derek): Remove this method and UI element once a better system is in place.

	Updates the UI text to state whether the Player is in Normal (firing) mode or Placement mode.
	*/
		if (in_placement_mode) {
			playergui.mode.text = "placement";
		} else {
			playergui.mode.text = "normal";
		}
	}
	
	protected override Vector2 GetThrustDirection() {
	/* Overrides ShipConstruct's ThrustDirection.

	Uses the controller to fetch MotorThrust.  Adjusts the UI NavigationBead accordingly.

	TODO(Derek): Set this up to use the Controller -or- Keyboard.
	*/
		float x = 0;
		if (Input.GetKey(KeyCode.D)) {
			x += 1f;
		}
		if (Input.GetKey (KeyCode.A)) {
			x -= 1f;
		}
		float y = 0;
		if (Input.GetKey(KeyCode.W)) {
			y += 1f;
		}
		if (Input.GetKey (KeyCode.S)) {
			y -= 1f;
		}
		playergui.SetNavigationBead(x, y);
		return new Vector2(x, y);
	}
}
