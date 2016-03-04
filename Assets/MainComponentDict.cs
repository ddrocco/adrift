using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainComponentDict : MonoBehaviour {
	static MainComponentDict main;
	
	void Start() {
		main = this;
	}
	
	public static MainComponentDict get() {
		return main;
	}
}
