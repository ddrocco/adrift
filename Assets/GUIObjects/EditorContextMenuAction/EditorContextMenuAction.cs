using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

public class EditorContextMenuAction {
	const float TOTAL_EMISSION_RATE = 400f;

	protected Color color;
	protected ParticleSystem system;

	/* Public interfaces */
	/* Default override interfaces */

	public void SetUpParticleSystem(int index, int total, ParticleSystem particle_system) {
		system = particle_system;
		system.startColor = color;
		system.SetEmissionRate(TOTAL_EMISSION_RATE / total);
		float arc = 360f / total;
		SerializedObject serialized_system = new SerializedObject(system);
		serialized_system.FindProperty("ShapeModule.arc").floatValue = arc;
		serialized_system.ApplyModifiedProperties();
		float euler_z = 90f - (arc / 2f) + (index * arc);
		system.transform.eulerAngles = new Vector3(0, 0, euler_z);
	}

	public virtual void PerformAction() {
		/* Default action.  Should be overwritten by children. */
		throw new NotImplementedException();
	}

	/* Virtual inherited methods */

	/* Private methods */
}
