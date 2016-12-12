using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour {

	public static int size = 10 ;
	public static int[,] board ;
	
	public GameObject solidBlock ;
	public GameObject normalBlock ;
	public GameObject floorBlock ;
	public Slider mapSizeSlider;
	public Camera cam;

	private double sbProportion = 0.15;

	private HashSet<Vector3> startingPoints;

	// Use this for initialization
	void OnEnable() {
		size = (int) mapSizeSlider.value;
		board = new int[size,size];
		GameObject.Find ("Main Camera").GetComponent<camAdjustment> ().setCamera ();
		startingPoints = new HashSet<Vector3> {
		new Vector3(1,0.5f,1), new Vector3(1,0.5f,size-2), new Vector3(size-2,0.5f,size-2), new Vector3(size-2,0.5f,1), 
		new Vector3(2,0.5f,1), new Vector3(2,0.5f, size-2), new Vector3(size-3,0.5f,size-2), new Vector3(size-3,0.5f,1), 
		new Vector3(1,0.5f,2), new Vector3(1,0.5f,size-3), new Vector3(size-2,0.5f,size-3), new Vector3(size-2,0.5f,2), 
		
		} ;
		generateSolidBlocks();
		generateFloor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void generateFloor() {
		for (int i=1;i<size-1;i++) {
			for (int j=1;j<size-1;j++) {
				createBlock(new Vector3(i,-0.5f,j),floorBlock);
				if (board[i,j] == 0 && !startingPoints.Contains(new Vector3(i,0.5f,j))) {
					createBlock(new Vector3(i,0.5f,j), normalBlock);
				}
			}
		}
	}

	void generateSolidBlocks() {
		for (int i=0; i<size-1 ; i++) {
			createBlock(new Vector3(0,0.5f,i),solidBlock);
			createBlock(new Vector3(size-1,0.5f,size-1-i),solidBlock);
			createBlock(new Vector3(i,0.5f,size-1),solidBlock);
			createBlock(new Vector3(size-1-i,0.5f,0),solidBlock);
		}
		int counter = 0;
		int timeout = 0;
		while (timeout++ < 100 && counter < (int)(size-2)*(size-2)*sbProportion) {
			int x = Random.Range(1,size-2);
			int y = Random.Range(1,size-2);
			if (goodNeighbors(x,y)) {
				createBlock(new Vector3(x,0.5f,y),solidBlock);
				counter++;
			}
		}

	}

	private bool goodNeighbors(int x, int y) {
		int counter = 0;
		if (board[x,y] != 0) return false;
		int i = -1;
		while (counter < 2 && i<2) {
			int j = -1;
			while (counter <2 && j<2) {
				if (board[x+i,y+j] != 0) counter++;
				j++;
			}
			i++;
		}
		if (counter > 1) return false;
		return true;
	}

	void createBlock(Vector3 position, GameObject prefab) {
		(Instantiate(prefab, position, prefab.transform.rotation) as GameObject).transform.parent = transform;
		if (prefab == solidBlock) {
			board[(int)position.x,(int)position.z]=2;
		} else if (prefab == normalBlock) {
			board[(int)position.x,(int)position.z]=1;
		}
	}
		
}
