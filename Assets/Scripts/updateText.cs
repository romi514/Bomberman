using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class updateText : MonoBehaviour {

	private Text textComponent;

	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetSliderValue(float sliderValue) {
		textComponent.text = "Map Size: " + ((int) sliderValue).ToString() +" Blocks";
	}
}
