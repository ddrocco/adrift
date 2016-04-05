using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public class EditorContextMenuAction {
	const float TOTAL_EMISSION_RATE = 400f;

	protected Color color;

	/* Public interfaces */
	/* Default override interfaces */

	public void ConfigureRegion(int index, int total, GameObject obj) {
		float arc = 360f / total;
		float euler_z = 90f - (arc / 2f) + (index * arc);
        obj.transform.eulerAngles = new Vector3(0, 0, euler_z);
        Renderer rend = obj.GetComponentInChildren<Renderer>();
        Vector3 randColor = UnityEngine.Random.insideUnitSphere;
        rend.material.color = new Color(randColor.x, randColor.y, randColor.z);
        
	}

	public virtual void PerformAction() {
		/* Default action.  Should be overwritten by children. */
		throw new NotImplementedException();
	}

	/* Virtual inherited methods */

	/* Private methods */
}
