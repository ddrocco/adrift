using UnityEngine;
using System.Collections;

public class CannonPivot : MonoBehaviour {
    Mouse mouse;
    public Crosshair crosshairPrefab;
    Crosshair currentCrosshair;
	// Use this for initialization
	void Start () {
        mouse = Mouse.main;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LookAt(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.z = 0;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.back);
    }

    void OnMouseDown()
    {
        //This function is called by Unity when a collider is clicked
        mouse.OnMove += LookAt;
        mouse.OnLeftClickUp += FireHandler;
        currentCrosshair = GameObject.Instantiate(crosshairPrefab.gameObject).GetComponent<Crosshair>();
    }

    void Fire(Vector3 position)
    {
        Debug.Log("Boom!");
    }

    void FireHandler(Vector3 position)
    {
        Fire(position);
        mouse.OnMove -= LookAt;
        mouse.OnLeftClickUp -= FireHandler;
        if(currentCrosshair != null)
        {
            GameObject.Destroy(currentCrosshair.gameObject);
        }
    }

    void OnDestroy()
    {
        mouse.OnMove -= LookAt;
        mouse.OnLeftClickUp -= FireHandler;
    }
}
