using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System;
using UnityEngine.UI;

public class EditorContextMenu : MonoBehaviour {
	const float INNER_BUFFER_DISTANCE = 0.5f;
	const float OUTER_BUFFER_DISTANCE = 2f;

	static EditorContextMenu active_context_menu;

	ShipTile selected_tile;

	/* Public interfaces */

	/* Default interfaces */

	protected List<EditorContextMenuAction> actions;

	/* Private methods */

	void Start () {
		if (active_context_menu) {
			GameObject.Destroy(active_context_menu);
			active_context_menu = this;
		}
		int num_actions = actions.Count;
		for (int i = 0; i < num_actions; ++i) {
			GameObject ecma_gameobj = Instantiate(ParticleDict.get().editor_context_menu_action) as GameObject;
			ParticleSystem system = ecma_gameobj.GetComponent<ParticleSystem>();
			system.transform.parent = transform;
			system.transform.localPosition = Vector3.zero;
			actions[i].SetUpParticleSystem(i, num_actions, system);
		}
	}

	void Update () {
		// PrintMouseInfo();
	}

	void OnDestroy() {
		active_context_menu = null;
	}

	/* Helper methods */

	void PrintMouseInfo() {
		Vector2 mouse_displacement = (Mouse.main.getWorldMousePosition() - transform.position).vector2();
		print("Mouse Pos: " + mouse_displacement
				+ "; Mouse Dist: " + mouse_displacement.magnitude
				+ "; Mouse Angle: " + mouse_displacement.ClockAngle()
				+ "; Zone: " + GetRegion());
	}

	int GetRegion() {
		Vector2 mouse_displacement = (Mouse.main.getWorldMousePosition() - transform.position).vector2();
		if (INNER_BUFFER_DISTANCE > mouse_displacement.magnitude
				|| OUTER_BUFFER_DISTANCE < mouse_displacement.magnitude) {
			// If mouse is too close or too far, return "invalid" result.
			// To disable this, set Inner to 0 and Outer to float max
			return -1;
		}
		float region_size = 360f / actions.Count;
		float mouse_angle = mouse_displacement.ClockAngle();
		float adjusted_mouse_angle = mouse_angle + (region_size / 2f);
		if (adjusted_mouse_angle > 360f) {
			return 0;
		} else {
			return Mathf.FloorToInt(adjusted_mouse_angle / region_size);
		}
	}
}