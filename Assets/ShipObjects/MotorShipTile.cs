﻿using UnityEngine;
using System.Collections;

public class MotorShipTile : ShipTile {
	public Direction direction = Direction.north;
	float forestrength = 1f;
	float aftstrength = 0.25f;
	
	// Use this for initialization
	protected new void Start () {
		base.Start();
		color = Color.blue;
		motorRunoff = null;
	}
	
	// Update is called once per frame
	protected new void Update () {
		base.Update();
	}
	
	ParticleSystem _motorRunoff;
	ParticleSystem motorRunoff {
		get {
			if (!_motorRunoff) {
				GameObject motorRunoffObj = Instantiate(
					ParticleDict.get().motor_runoff,
					gameObject.transform.position.vector2() - (0.5f * direction.vector2()),
					direction.quaternion()
					) as GameObject;
				motorRunoffObj.transform.parent = transform;
				_motorRunoff = motorRunoffObj.GetComponent<ParticleSystem>();
			}
			return _motorRunoff;
		}
		set {
			_motorRunoff = value;
		}
	}
	
	public Vector2 GetThrust(Vector2 thrust_direction) {
		float thrust_magnitude = Vector2.Dot(direction.vector2(), thrust_direction);
		if (thrust_magnitude > 0) {
			// Motor runs forward; use forestrength.
			motorRunoff.startSpeed = thrust_magnitude * forestrength;
			motorRunoff.startLifetime = thrust_magnitude * forestrength;
			return forestrength * thrust_magnitude * direction.vector2();
		} else {
			// Motor runs backward; use aftstrength.
			motorRunoff.startSpeed = -thrust_magnitude * aftstrength;
			motorRunoff.startLifetime = thrust_magnitude * aftstrength;
			return aftstrength * thrust_magnitude * direction.vector2();
		}
	}
}
