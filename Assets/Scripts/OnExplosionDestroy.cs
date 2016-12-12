using UnityEngine;
using System.Collections;

public class OnExplosionDestroy : MonoBehaviour {

	private float blueOrbProportion = 0.33f; // sum of 3 should be 1 
	private float greenOrbProportion = 0.33f;
	private float redOrbProportion = 0.33f;
	private float orbProportion = 0.33f;

	public GameObject blueOrb;
	public GameObject greenOrb;
	public GameObject redOrb;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (MapGeneration.board[Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.z)] != 1) {
			Destroy(gameObject);
			if (Random.value < orbProportion) {
				float r = Random.value;
				if (r<blueOrbProportion) {
					Instantiate(blueOrb,gameObject.transform.position,gameObject.transform.rotation);
				} else if (1-r > greenOrbProportion) {
					Instantiate(greenOrb,gameObject.transform.position,gameObject.transform.rotation);
				} else {
					Instantiate(redOrb,gameObject.transform.position,gameObject.transform.rotation);
				}
			}
		}
	}
}
