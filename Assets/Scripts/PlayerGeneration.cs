using UnityEngine;
using System.Collections;

public class PlayerGeneration : MonoBehaviour {

	public Player[] players;

	private Vector3[] startingPositions = {
		new Vector3(1,0.5f,1),
		new Vector3(MapGeneration.size-2,0.5f,MapGeneration.size-2)
	};

	// Use this for initialization
	void Start () {
		for (int i=1 ; i<players.Length+1 ; i++) {			
			Instantiate(players[i-1], startingPositions[i-1], this.transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {	
	}
}
