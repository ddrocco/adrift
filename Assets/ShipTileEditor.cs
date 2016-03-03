using UnityEngine;
using System.Collections;

public class ShipTileEditor : MonoBehaviour {
	public PlayerShipConstruct player_ship;
	public bool edit_mode;

	// Use this for initialization
	void Start () {
		edit_mode = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!edit_mode) {
			return;
		}
		foreach (ShipTile tile in player_ship.tiles) {

		}
	}
}
