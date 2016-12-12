using UnityEngine;
using System.Collections;

public class camAdjustment : MonoBehaviour {
	
	float xInc = .5f;
	float yInc = (16.67f - 14.85f) / 10f;
	float zInc = (0 + 1) / 10f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setCamera() {
		int incValue = (MapGeneration.size - 10);
		float newX = transform.position.x + incValue * xInc;
		float newY = transform.position.y + incValue * yInc;
		float newZ = transform.position.z + incValue * zInc;
		Vector3 newPos = new Vector3 (newX, newY,newZ);
		transform.position = newPos;
	}
}
