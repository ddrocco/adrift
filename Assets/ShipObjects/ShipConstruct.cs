using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.WSA;

public class ShipConstruct : MonoBehaviour {
/* An object representing a single Ship.  This Ship is a collection of ShipTiles which work together. */
	public List<ShipTile> tiles;
	public List<MotorShipTile> motors;
	public CartesianMap<ShipTile> tilemap;

	public void InitializeConstruction() {
	/* Called by Start() to configure the ShipConstruct's child ShipTiles.

	TODO(Ian): Make an Editor button to call this and remove it from Start().
	*/
		tiles = new List<ShipTile>();
		foreach (MotorShipTile motor in GetComponentsInChildren<MotorShipTile>()) {
			motors.Add(motor);
		}
		foreach (ShipTile tile in GetComponentsInChildren<ShipTile>()) {
			tile.coordinates.x = Mathf.RoundToInt(tile.gameObject.transform.position.x);
			tile.coordinates.y = Mathf.RoundToInt(tile.gameObject.transform.position.y);
			tile.coordinates.attached = true;
			tiles.Add(tile);
			// TODO(Derek): Fix this.  It currently recalculates every time there's an update.
			tilemap.Insert(
					tile.coordinates.x,
					tile.coordinates.y,
					tile);
		}
	}

	public void AddToConstruction(ShipTile new_tile, ShipTile.Coordinates coordinates) {
	/* Called when a new ShipTile is constructed to register it with this ShipConstruction's child ShipTiles. */
		new_tile.transform.parent = ShipTileEditor.main.player_ship.transform;
		new_tile.coordinates = coordinates;
		new_tile.SetPositionByCoordinates();
		tiles.Add(new_tile);
		tilemap.Insert(
				new_tile.coordinates.x,
				new_tile.coordinates.y,
				new_tile);
	}
	
	Vector2 velocity = Vector2.zero;
	protected float Mass {
	/* Represents a Ship's Mass, influencing how fast motors can move the Ship and collision damage. */
		get {
			return 15f * tiles.Count;
		}
	}

	protected virtual void Start () {
		tilemap = new CartesianMap<ShipTile>();
		InitializeConstruction();
	}
	
	protected virtual void Update () {
	/* Though this is virtual, its children should call base.Update to ensure this still runs.

	Handles movement of the ShipConstruct.

	TODO(Derek/Ian): Find a clean way of making this happen during Update, but making movement happen during FixedUpdate.
	*/
		Move ();
		transform.Translate(velocity);
	}
	
	void FixedUpdate() {
	/* TODO(Derek): This is terrible, but it sorta gets the job done.  Find a better way to do it, ideally.

	Slows down the ShipConstruct. Ensures that a terminal velocity exists for each motor strength
	Ensures that ships won't continue to drift if the motors are off.
	*/
		velocity *= 0.95f;
	}
	
	protected virtual Vector2 GetThrustDirection() {
	/* A virtual method to determine direction of thrust (relative to ship's forward).

	Always falls within the Unit Square.

	Overwritten by PlayerShipConstruct to use player Input.
	Overwritten by FoeShipConstruct to use foe AI.
	*/
		return new Vector2(1f, 1f);
	}
	
	void Move() {
	/* Modify the Ship's Velocity according to the Thrust Direction, Thrust Force, and Mass.

	Thrust Force is the total vector of thrust from all motors.
	*/
		Vector2 thrust_direction = GetThrustDirection();
		Vector2 thrust = Vector2.zero;
		foreach (MotorShipTile motor in motors) {
			thrust += motor.GetThrust(thrust_direction);
		}
		velocity += thrust * Time.deltaTime / Mass;
	}
}
