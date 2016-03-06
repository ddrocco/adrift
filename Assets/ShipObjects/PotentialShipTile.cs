using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class PotentialShipTile : MonoBehaviour {
/* A ShipTile-adjacent location upon which a ShipTile can be constructed.

Handles Instantiation of new ShipTiles.

Since this isn't -yet- a ShipTile, it doesn't inherit from ShipTile.
*/
	public static float BASE_LIFETIME = 0.1f;
	public static Color BASE_COLOR = Color.white;
	public static Color MOUSEOVER_COLOR = Color.white;
	public static float BASE_SIZE = 0.1f;
	public static float MOUSEOVER_SIZE = 0.25f;

	// The index of this entry in ShipTileEditor.ConstructableTiles.
	public int editor_index;

	bool valid = true;

	ParticleSystem _particles;
	public ParticleSystem particles {
		get {
			if (!_particles) {
				_particles = GetComponent<ParticleSystem>();
			}
			return _particles;
		}
	}

	public void OnMouseEnter() {
	/* Sets the PotentialShipTile's appearance to "selected" during a mouseover. */
		particles.startColor = MOUSEOVER_COLOR;
		particles.startSize = MOUSEOVER_SIZE;
	}

	public void OnMouseExit() {
	/* Returns the PotentialShipTile's appearance to normal after a mouseover. */
		particles.startColor = BASE_COLOR;
		particles.startSize = BASE_SIZE;
	}

	public void OnMouseDown() {
	/* "Build" this PotentialShipTile.

	Destroys the PotentialShipTile and replaces it with an OpenShipTile.
	TODO(Derek): Instead issue a build order, so that construction is not instantaneous.

	Adds the OpenShipTile to the PlayerShipConstruct's tileset and refreshes the Editor's tile availability.
	*/
		if (!valid || !ShipTileEditor.main.player_ship.in_placement_mode) {
			return;
		}
		particles.startLifetime = 0f;
		valid = false;
		GameObject open_ship_tile_object = Instantiate(MainComponentDict.get().open_ship_tile) as GameObject;
		OpenShipTile open_ship_tile = open_ship_tile_object.GetComponent<OpenShipTile>();
		ShipTile.Coordinates coordinates = ShipTileEditor.main.GetCoordinatesByIndex(editor_index);
		ShipTileEditor.main.player_ship.AddToConstruction(open_ship_tile, coordinates);
		ShipTileEditor.main.NeedsRefresh();
		// This should set a timed construction job, but for now construction is insantaneous.
		ShipTileEditor.main.Refresh();
	}
}
