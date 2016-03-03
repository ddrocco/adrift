using UnityEngine;
using System.Collections;

public class CannonPivot : MonoBehaviour {
    Mouse mouse;
	// Use this for initialization
	void Start () {
        mouse = Mouse.main;
        mouse.OnMove += onMouseMove;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LookAt(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.z = 0;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, direction, Color.green, 1);
        Debug.DrawRay(transform.position, forward, Color.yellow, 1);
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        Debug.Log(direction);
        Debug.Log(forward);
    }

    void onMouseMove(Vector3 position)
    {
        LookAt(position);
    }
}
