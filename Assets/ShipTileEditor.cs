using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShipTileEditor : MonoBehaviour {
	public static ShipTileEditor main;

	public PlayerShipConstruct player_ship;
	public bool edit_mode;

	CartesianMap<bool> _ConstructableTiles;
	CartesianMap<bool> ConstructableTiles {
		get {
			if (!_needs_refresh) {
				return _ConstructableTiles;
			} else {
				RefreshConstructableTiles();
				_needs_refresh = false;
				return _ConstructableTiles;
			}
		}
	}
	bool _needs_refresh = true;

	void RefreshConstructableTiles() {
		_ConstructableTiles = new CartesianMap<bool>();
		foreach (ShipTile tile in player_ship.tiles) {
			AddToConstructableTilesIfAvailable(tile.coordinates.x+1, tile.coordinates.y);
			AddToConstructableTilesIfAvailable(tile.coordinates.x-1, tile.coordinates.y);
			AddToConstructableTilesIfAvailable(tile.coordinates.x, tile.coordinates.y+1);
			AddToConstructableTilesIfAvailable(tile.coordinates.x, tile.coordinates.y-1);
		}
	}

	void AddToConstructableTilesIfAvailable(int x, int y) {
		if (player_ship.tilemap.Get(x, y) == default(bool)
				&& ConstructableTiles.Get(x, y) == default(bool)) {
			_ConstructableTiles.Insert(x, y, true);
		}
	}

	// Use this for initialization
	void Start () {
		main = this;
		edit_mode = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!edit_mode) {
			return;
		}
		foreach (ShipTile tile in player_ship.tilemap.GetAll()) {
			print(tile.coordinates.x + " " + tile.coordinates.y);
		}
	}
}
