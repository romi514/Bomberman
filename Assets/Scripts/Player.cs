using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

    public Bomb bomb;

    private Rigidbody rigidBody;
    private Transform myTransform;

    public int playerNumber;
    public bool dead = false;
    private float timeToExplode = 3f;

    // Stats
    private float moveSpeed = 6f;
    private float maxSpeed = 15f;
    public Slider speedSlider;

    private int explosionRadius = 1;
    private int maxExplosionRadius = MapGeneration.size;
    public Slider powerSlider;

    private int totalBombs = 1;
    private int numberOfBombs;
    private int maxBombs = 10;
    public Slider bombSlider;

	public deadText deadt;
	public ShowPanels UI;

	// Use this for initialization
	void Start () {
        gameObject.tag = "Player";
        setPlayerPosition();
        setSliders();
        //speedSlider.maxValue = maxSpeed - moveSpeed;
		rigidBody = GetComponent<Rigidbody>();
		myTransform = transform;
        numberOfBombs = totalBombs;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement();
	}

    void setPlayerPosition() {
        if (playerNumber == 1) {
            transform.position = new Vector3(1,0.5f,1);
        }
        if (playerNumber == 2) {
            transform.position = new Vector3(MapGeneration.size-2,0.5f,MapGeneration.size-2);
        }
    }

	void UpdateMovement() {
        if (playerNumber == 2) {
    		if (Input.GetKey(KeyCode.UpArrow)) { 
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
                myTransform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.LeftArrow)) { 
                rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
                myTransform.rotation = Quaternion.Euler(0, 270, 0);
            }

            if (Input.GetKey(KeyCode.DownArrow)) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
                myTransform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow)) {
                rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
                myTransform.rotation = Quaternion.Euler(0, 90, 0);
            }

            if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && (numberOfBombs > 0)) {
                setBomb();
            }
        } else if (playerNumber == 1) {
            if (Input.GetKey(KeyCode.Z)) { 
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
                myTransform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Input.GetKey(KeyCode.Q)) { 
                rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
                myTransform.rotation = Quaternion.Euler(0, 270, 0);
            }

            if (Input.GetKey(KeyCode.S)) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
                myTransform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (Input.GetKey(KeyCode.D)) {
                rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
                myTransform.rotation = Quaternion.Euler(0, 90, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && (numberOfBombs > 0)) {
                setBomb();
            }
        }
	}

    void setBomb() {
		if (MapGeneration.board [Mathf.RoundToInt (myTransform.position.x), Mathf.RoundToInt (myTransform.position.z)] == 0) {
			
			Instantiate(bomb, new Vector3 (Mathf.RoundToInt (myTransform.position.x), 
				0.5f, Mathf.RoundToInt (myTransform.position.z)),
				bomb.transform.rotation);
			bomb.explosionRadius = explosionRadius;
			bomb.timeToExplode = timeToExplode;
			numberOfBombs--;
			StartCoroutine (WaitToExplode ());
		}
    }

    private IEnumerator WaitToExplode() {
        yield return new WaitForSeconds(timeToExplode);
        numberOfBombs++;
    }

    public void addBomb() {
        numberOfBombs++;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Explosion")) {
            Debug.Log("Player" + playerNumber + " hit by explosion!");
            dead = true;
			UI.ShowDeadPanel ();
			deadt.playerDead (playerNumber);
			gameObject.SetActive (false);
        }
        if (other.CompareTag("Orb")) {
            if (other.name == "BlueOrb(Clone)") {
                if (totalBombs < maxBombs){
                    numberOfBombs++;
                    totalBombs++;
                    bombSlider.value++;
                }
            } else if (other.name == "GreenOrb(Clone)") {
                if (moveSpeed < maxSpeed) {
                    moveSpeed++;
                    speedSlider.value ++;
                }
            } else if (other.name == "RedOrb(Clone)") {
                if (explosionRadius < maxExplosionRadius) {
                    explosionRadius++;
                    powerSlider.value++;
                }
            }
            Destroy(other.gameObject);         
        }
    }

    public void setSliders() {
        speedSlider.minValue = moveSpeed;
        speedSlider.maxValue = maxSpeed;
        bombSlider.minValue = totalBombs;
        bombSlider.maxValue = maxBombs;
        powerSlider.minValue = explosionRadius;
        powerSlider.maxValue = maxExplosionRadius;
    }
}
