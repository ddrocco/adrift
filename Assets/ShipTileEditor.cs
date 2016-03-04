using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.WSA;

public class ShipTileEditor : MonoBehaviour {
	public static ShipTileEditor main;

	PlayerShipConstruct _player_ship;
	public PlayerShipConstruct player_ship {
		get {
			if (!_player_ship) {
				_player_ship = FindObjectOfType<PlayerShipConstruct>();
			}
			return _player_ship;
		}
	}

	public struct TileCoordinates {
		public int x;
		public int y;
		public ParticleSystem renderer;
	}

	List<TileCoordinates> _ConstructableTiles;
	List<TileCoordinates> ConstructableTiles {
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
		foreach (TileCoordinates tile_coordinate in _ConstructableTiles) {
			GameObject.Destroy(tile_coordinate.renderer);
		}
		_ConstructableTiles = new List<TileCoordinates>();
		foreach (ShipTile tile in player_ship.tilemap.GetAllCached()) {
			AddToConstructableTilesIfAvailable(tile.coordinates.x+1, tile.coordinates.y);
			AddToConstructableTilesIfAvailable(tile.coordinates.x-1, tile.coordinates.y);
			AddToConstructableTilesIfAvailable(tile.coordinates.x, tile.coordinates.y+1);
			AddToConstructableTilesIfAvailable(tile.coordinates.x, tile.coordinates.y-1);
		}
	}

	void AddToConstructableTilesIfAvailable(int x, int y) {
		ShipTile old_tile = player_ship.tilemap.Get(x, y);
		if (!old_tile) {
			if (!_ConstructableTiles.Exists(tile=> tile.x == x && tile.y ==y)) {
				GameObject renderer = Instantiate(ParticleDict.get().potential_ship_tile) as GameObject;
				renderer.transform.parent = player_ship.transform;
				renderer.transform.localPosition = new Vector3(x, y, 0);
				TileCoordinates tile_coordinates = new TileCoordinates {
					x = x,
					y = y,
					renderer = renderer.GetComponent<ParticleSystem>()
				};
				_ConstructableTiles.Add(tile_coordinates);
			}
		}
	}

	// Use this for initialization
	void Start () {
		main = this;
		_ConstructableTiles = new List<TileCoordinates>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!player_ship.in_placement_mode) {
			return;
		}
	}
}
