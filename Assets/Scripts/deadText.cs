using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class deadText : MonoBehaviour {
	private Text textScript;
	private int winner = 1;
	private float alphaValue = 0;
	private float redValue = 0.5f;
	private bool increasingRed = true;

	// Use this for initialization
	void Start () {
		textScript = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.active) {
			if (alphaValue < 1) {
				alphaValue += .02f;
			} 
			if (increasingRed) {
				redValue += .03f;
			} else {
				redValue -= .03f;
			}
			if (redValue >= .7f) {
				increasingRed = false;
			} else if (redValue <= .3f) {
				increasingRed = true;
			}
			textScript.color = new Color (redValue, 0f, 0f, alphaValue);
			textScript.text = "Player " + winner + " wins";
		}
	}

	public void playerDead(int i) {
		winner = 3 - i;
	}
}
