using UnityEngine;
using System.Collections;

public class CannonPivot : MonoBehaviour {
    Mouse mouse;
    public Crosshair crosshairPrefab;
    public GameObject round;
    GameObject currentRound;
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

    bool onFixed = false;
    Vector3 onFixedPosition;

    void FixedUpdate()
    {
        if(!onFixed)
        {
            return;
        }
        onFixed = false;
        Vector3 position = onFixedPosition;
        position.z = transform.parent.position.z;
        currentRound = GameObject.Instantiate(round, transform.parent.position, Quaternion.identity) as GameObject;
        Rigidbody body = currentRound.GetComponent<Rigidbody>();
        Physics.IgnoreCollision(body.GetComponent<Collider>(), transform.GetComponent<Collider>());
        Physics.IgnoreCollision(body.GetComponent<Collider>(), transform.parent.GetComponent<Collider>());

        Vector3 direction = (position - transform.position).normalized;
        direction.z = -1;
        float distance = (position - transform.position).magnitude / 2;
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude);
        body.velocity = direction * velocity;
        Debug.Log(body.velocity);
    }

    void Fire(Vector3 position)
    {
        onFixedPosition = position;
        onFixed = true;
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
