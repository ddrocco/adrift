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

	void Start () {
		base.Start();
	}
	
	void Update () {
		base.Update();
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
