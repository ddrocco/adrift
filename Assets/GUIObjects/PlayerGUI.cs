using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerGUI : MonoBehaviour {
	public static PlayerGUI main;
	public Image navpad;
	public Image navbead;
	public Text navcoords;
	public Text scrapmetal;
	public Text mode;

	// Use this for initialization
	void Start () {
		main = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetNavigationBead(float x, float y) {
		navbead.transform.localPosition = new Vector2(
				navpad.rectTransform.rect.width * x / 2f,
				navpad.rectTransform.rect.height * y / 2f);
		navcoords.text = "(" + x.ToString() + ", " + y.ToString() + ")";
	}
}
