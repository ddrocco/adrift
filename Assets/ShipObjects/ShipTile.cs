using UnityEngine;
using System.Collections;

public class ShipTile : MonoBehaviour {
	protected Color color;
	
	// Use this for initialization
	protected void Start () {
		color = Color.black;
	}
	
	// Update is called once per frame
	protected void Update () {
		gameObject.GetComponent<Renderer>().material.color = color;
		if (GetComponents<ShipTile>().Length > 1) {
			throw new UnityException("Too many object controllers for object!");
		}
	}
}
