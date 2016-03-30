using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
    Mouse mouse;
	// Use this for initialization
	void Start () {
        mouse = Mouse.main;
        transform.position = mouse.getWorldMousePosition();
        mouse.OnMove += SetPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    void OnDestroy()
    {
        mouse.OnMove -= SetPosition;
    }
}
