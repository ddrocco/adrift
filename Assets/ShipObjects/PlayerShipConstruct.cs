using UnityEngine;
using System.Collections;

public class PlayerShipConstruct : ShipConstruct {
	PlayerGUI _playergui;
	PlayerGUI playergui {
		get {
			if (!_playergui) {
				_playergui = PlayerGUI.main;
			}
			return _playergui;
		}
	}
	// Remove public nature of scrapmetal and pick a new default.
	public int scrapmetal = 500;
	bool in_placement_mode = false;

	void Start () {
		base.Start();
		playergui.scrapmetal.text = scrapmetal.ToString();
		SetPlacementModeText();
	}
	
	void Update () {
		base.Update();
		if (Input.GetKeyDown(KeyCode.P)) {
			in_placement_mode = !in_placement_mode;
			SetPlacementModeText();
		}
	}

	void SetPlacementModeText() {
		if (in_placement_mode) {
			playergui.mode.text = "placement";
		} else {
			playergui.mode.text = "normal";
		}
	}
	
	protected override Vector2 GetThrustDirection() {
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
