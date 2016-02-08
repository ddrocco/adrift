using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleDict : MonoBehaviour {
	public GameObject motor_runoff;
	static ParticleDict main;
	
	void Start() {
		main = this;
	}
	
	public static ParticleDict get() {
		return main;
	}
}
