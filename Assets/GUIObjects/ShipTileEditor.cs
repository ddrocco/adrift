using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.WSA;

public class ShipTileEditor : MonoBehaviour {
	/* An object for handling Ship edits (tile construction and modification). */

	/* Since there's only ever one, this object has a static accessor. */
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
	/* A struct denoting a potential construction site.

	TODO(Derek): Think of a better name.
	*/
		public int x;
		public int y;
		public PotentialShipTile potential_tile;
	}

	public void SetConstructableTileVisibility(bool show_tiles) {
	/* Toggles rendering and usability of PotentialShipTiles.

	if show_tiles is true, sets all PotentialShipTiles to visible and usable.
	if show_tiles is false, sets all PotentialShipTiles to invisible and unusable.
	*/
		if (show_tiles) {
			foreach (TileCoordinates tile in ConstructableTiles) {
				tile.potential_tile.particles.startLifetime = PotentialShipTile.BASE_LIFETIME;
			}
		} else {
			foreach (TileCoordinates tile in ConstructableTiles) {
				tile.potential_tile.particles.startLifetime = 0f;
			}
		}
	}

	public ShipTile.Coordinates GetCoordinatesByIndex(int index) {
	/* Returns the coordinates of a PotentialShipTile given its List-index.

	TODO(Derek): Consider just moving the coordinates to that object.
	*/
		return new ShipTile.Coordinates {
			x = ConstructableTiles[index].x,
			y = ConstructableTiles[index].y,
			attached = true
		};
	}

	bool _needs_refresh = true;

	public void NeedsRefresh() {
	/* Called when the ConstructableTiles set has changed.  Denotes that the set must be recalculated. */
		_needs_refresh = true;
	}
	public void Refresh() {
	/* Actively refreshes the ConstructableTiles set.

	TODO(Derek): Since this is an expensive operation, we should find a way to only modify as necessary.
		This might still be necessary when chunks of the ship are blown off.
	*/
		RefreshConstructableTiles();
		_needs_refresh = false;
	}

	List<TileCoordinates> _ConstructableTiles;
	List<TileCoordinates> ConstructableTiles {
		get {
			if (_needs_refresh) {
				RefreshConstructableTiles();
				_needs_refresh = false;
			}
			return _ConstructableTiles;
		}
	}

	void RefreshConstructableTiles() {
	/* Lazily called after the ConstructableTiles set has changed.  Recalculates the set.

	Since this is an expensive operation, it should be called sparingly.

	TODO(Derek): Move some of the internals to standalone methods which can be called for smaller changes.
	*/
		foreach (TileCoordinates tile_coordinate in _ConstructableTiles) {
			GameObject.Destroy(tile_coordinate.potential_tile.gameObject);
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
	/* Attempts to add a new PotentialShipTile at (x, y).

	Only adds a new PST if no PST or ShipTile currently exists there.
	*/
		ShipTile old_tile = player_ship.tilemap.Get(x, y);
		if (!old_tile) {
			if (!_ConstructableTiles.Exists(tile=> tile.x == x && tile.y ==y)) {
				GameObject potential_tile_obj = Instantiate(ParticleDict.get().potential_ship_tile) as GameObject;
				PotentialShipTile potential_tile = potential_tile_obj.GetComponent<PotentialShipTile>();
				potential_tile.editor_index = _ConstructableTiles.Count;
				potential_tile.transform.parent = player_ship.transform;
				potential_tile.transform.localPosition = new Vector3(x, y, 0);
				TileCoordinates tile_coordinates = new TileCoordinates {
					x = x,
					y = y,
					potential_tile = potential_tile
				};
				_ConstructableTiles.Add(tile_coordinates);
			}
		}
	}

	void Start () {
		main = this;
		_ConstructableTiles = new List<TileCoordinates>();
	}

	void Update () {
		if (!player_ship.in_placement_mode) {
			return;
		}
	}

	void OnDestroy() {
		PlayerGUI.main.ShipTileDestroyed();
	}
}
