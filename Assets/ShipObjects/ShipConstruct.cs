using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipConstruct : MonoBehaviour {
	public List<ShipTile> tiles;
	public List<MotorShipTile> motors;
	
	Vector2 velocity = Vector2.zero;
	protected float Mass {
		get {
			return 15f * tiles.Count;
		}
	}

	public void Start () {
		foreach (MotorShipTile motor in GetComponentsInChildren<MotorShipTile>()) {
			motors.Add(motor);
		}
		foreach (ShipTile tile in GetComponentsInChildren<ShipTile>()) {
			tiles.Add(tile);
		}
	}
	
	public void Update () {
		Move ();
		transform.Translate(velocity);
	}
	
	void FixedUpdate() {
		velocity *= 0.95f;
	}
	
	protected virtual Vector2 GetThrustDirection() {
		return new Vector2(1f, 1f);
	}
	
	void Move() {
		// The desired movement direction relative to the ship's forward.
		// -1f <= thrust_direction.x, thrust_direction.y >= 1f
		Vector2 thrust_direction = GetThrustDirection();
		// thrust is the total vector of thrust returned by motors.
		Vector2 thrust = Vector2.zero;
		foreach (MotorShipTile motor in motors) {
			thrust += motor.GetThrust(thrust_direction);
		}
		velocity += thrust * Time.deltaTime / Mass;
	}
}
