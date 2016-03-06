using UnityEngine;
using System.Collections;

public class TurretShipTile : ShipTile {
	/* TODO(Derek): Remove this and move some vestiges over to Ian's Cannon. */

	// Use this for initialization
	protected new void Start () {
		base.Start();
		color = Color.red;
	}
	
	// Update is called once per frame
	protected new void Update () {
		base.Update();
	}
}
