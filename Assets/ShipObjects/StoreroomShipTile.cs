using UnityEngine;
using System.Collections;

public class StoreroomShipTile : ShipTile {
/* A constructed ShipTile which can store fuel, ammunition, and food.

Not yet implemented (functionally an OpenTile). */

	protected new void Start () {
		base.Start();
		color = Color.green;
	}

	protected new void Update () {
		base.Update();
	}
}
