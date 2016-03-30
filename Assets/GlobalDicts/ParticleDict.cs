using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleDict : MonoBehaviour {
	public GameObject motor_runoff;
	public GameObject potential_ship_tile;
	public GameObject editor_context_menu_action;

	static ParticleDict _main = null;
	public static ParticleDict main {
		get {
			if (!_main) {
				_main = FindObjectOfType<ParticleDict>();
			}
			return _main;
		}
	}
	
	public static ParticleDict get() {
		return main;
	}
}
