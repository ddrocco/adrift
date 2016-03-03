using UnityEngine;
using System.Collections;
public class Mouse : MonoBehaviour {
    public delegate void MousePositionDelegate(Vector3 position);
    public event MousePositionDelegate OnMove;
    public event MousePositionDelegate OnLeftClickDown;
    public event MousePositionDelegate OnLeftClickUp;
    public event MousePositionDelegate OnRightClickDown;
    public event MousePositionDelegate OnRightClickUp;

    static public Mouse main
    {
        get
        {
            return GameObject.FindGameObjectWithTag("Mouse").GetComponentInChildren<Mouse>();
        }
    }

    Vector3 lastPosition;
    Camera cam;

    Vector3 getWorldMousePosition()
    {
        Vector3 screenpos = Input.mousePosition;
        screenpos.z = cam.nearClipPlane;
        Vector3 worldpos = cam.ScreenToWorldPoint(screenpos);
        worldpos.z = 0;
        return worldpos;
    }

    void Start()
    {
        cam = Camera.main;
        lastPosition = getWorldMousePosition();
    }
    void Update()
    {
        Vector3 current = getWorldMousePosition();

        if ((lastPosition - current).sqrMagnitude > .0001 && OnMove != null) OnMove(current);
        if (Input.GetMouseButtonDown(0) && OnLeftClickDown != null) OnLeftClickDown(current);
        if (Input.GetMouseButtonUp(0) && OnLeftClickUp != null) OnLeftClickUp(current);
        if (Input.GetMouseButtonDown(1) && OnRightClickDown != null) OnRightClickDown(current);
        if (Input.GetMouseButtonUp(1) && OnRightClickUp != null) OnRightClickUp(current);

        lastPosition = current;
    }

}
