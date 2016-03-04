using UnityEngine;
using System.Collections;
using UnityEditor;

public class OpenShipTile : ShipTile {

	public OpenShipTile(int x, int y) {
		coordinates.x = x;
		coordinates.y = y;
		coordinates.attached = true;
		SetPositionByCoordinates();
	}

	// Use this for initialization
	protected new void Start () {
		base.Start();
		color = Color.gray;
	}
	
	// Update is called once per frame
	protected new void Update () {
		base.Update();
	}
}
