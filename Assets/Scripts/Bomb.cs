using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

public GameObject explosion; 
public LayerMask levelMask;

public int explosionRadius;
private bool exploded = false;
public float timeToExplode;

	// Use this for initialization
	void Start () {
		MapGeneration.board [Mathf.RoundToInt (transform.position.x), Mathf.RoundToInt (transform.position.z)] = 3;
		Invoke("Explode",timeToExplode);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Explode() {
		Instantiate(explosion, transform.position, Quaternion.identity);

		StartCoroutine(CreateExplosions(Vector3.forward));
		StartCoroutine(CreateExplosions(Vector3.right));
		StartCoroutine(CreateExplosions(Vector3.back));
		StartCoroutine(CreateExplosions(Vector3.left));

		GetComponent<MeshRenderer>().enabled = false;
		exploded = true;
		MapGeneration.board [Mathf.RoundToInt (transform.position.x), Mathf.RoundToInt (transform.position.z)] = 0;
		Destroy(gameObject, .3f);
	}

	private IEnumerator CreateExplosions(Vector3 direction) {
 		for (int i = 1; i < explosionRadius+1; i++) { 
  			RaycastHit hit; 
  			Physics.Raycast(transform.position, direction, out hit, i, levelMask); 
 
 			Vector3 rayPosition = transform.position + (i * direction);
			if (!hit.collider) { 
   				Instantiate(explosion, rayPosition,
      			explosion.transform.rotation);
  			} else {
  				if (MapGeneration.board[Mathf.RoundToInt(rayPosition.x),Mathf.RoundToInt(rayPosition.z)] == 1) {
  					Instantiate(explosion, rayPosition,
      				explosion.transform.rotation);
      				MapGeneration.board[Mathf.RoundToInt(rayPosition.x),Mathf.RoundToInt(rayPosition.z)] = 0;
  				} 
    			break; 
    		}
 			
 			yield return new WaitForSeconds(.05f); 
		}
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("collider triggered with" + other.ToString());
		if (!exploded && other.CompareTag("Explosion")) {
  			CancelInvoke("Explode");
  			Explode();
		}
	}
	
	void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) { 
            GetComponent<Collider>().isTrigger = false; 
        }
    }
}
