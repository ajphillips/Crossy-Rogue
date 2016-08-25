using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {


	public float controlLockTime;
	public float pushBack;
	public float speed;
	public Collider strikeZone;
	public GameObject lightSource;
	public float throwLightForce;
	public Transform LightLauncher;
	public GameObject PlayersTorch;
	public int numTorches = 3;
	public float turboX;
	public float invulnTimer;
	public int levelNum;
	public ParticleSystem bloodSplatter;
    private Animator playerDeath;
    private int deathStateHash = Animator.StringToHash("PlayerRootVanish");
    private int normalStateHash = Animator.StringToHash("PlayerRootNormal");
    public string currentScene;
    public GameObject respawnPoint;
	public AudioSource coinSound;
	public AudioSource damageSound;
	public AudioSource keySound;
	public AudioSource doorSound;



    private int goldCounter;
    private int deathCounter;
	public bool lockControls;
	private int hitPoints;
	private int illumination;
	private Rigidbody playerRb;
	private float snuffSeconds;
	private bool hasTorch;
	private float turboSpeed;
	private bool invulnerable;
	private bool dead;
	private static int keys;
    private bool inBossRoom;
    public static int skelekey;     //this needs to be public so a script in the menu scene can see it

	// Use this for initialization
	void Start () {
		playerRb = GetComponent<Rigidbody> ();
//        currentScene = GameObject.FindGameObjectWithTag("Goal").GetComponent<GoalBoundScript>().currentLevel;
		hasTorch = true;
		illumination = 0;
		turboSpeed = speed * turboX;
		hitPoints = 3;
		lockControls = false;
		invulnerable = false;
		goldCounter = PlayerPrefs.GetInt("Gold", goldCounter);
		deathCounter = PlayerPrefs.GetInt ("Deaths", deathCounter);
        numTorches = 3;
        inBossRoom = false;
        playerDeath = GetComponent<Animator>();
        if (currentScene == "Menu Scene")
        {
            goldCounter = 0;
            PlayerPrefs.SetInt("Deaths", 0);
            keys = 0;
        }
	}
	
	void FixedUpdate () {
		
//		debugText.text = "Illumination: " + illumination;
			float vertical = -Input.GetAxis ("Vertical");
			float horizontal = -Input.GetAxis ("Horizontal");
			PlayersTorch.SetActive (hasTorch);
			strikeZone.enabled = false;
			Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);
			movement.x *= speed;
			movement.z *= speed;
			movement.y = playerRb.velocity.y;

			if (!lockControls) {
				playerRb.velocity = (movement);
			}
			//This checks if the player is moving, if he is then it plays the footstep sound
		if(playerRb.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false && GetComponent<AudioSource>().volume > 0){
				GetComponent<AudioSource> ().volume = Random.Range (0.5f, 0.8f);
				GetComponent<AudioSource> ().pitch = Random.Range (0.8f, 1.1f);
				GetComponent<AudioSource> ().Play ();
			}
			Mathf.Clamp (playerRb.velocity.y, 0.0f, 10.0f);

			if (horizontal != 0 || vertical != 0) {
				playerRb.transform.rotation = Quaternion.LookRotation (movement);
			}

			if (Input.GetKey (KeyCode.LeftShift)) {
				speed = turboSpeed;
			} else
				speed = turboSpeed / turboX;
	
		strikeZone.enabled = (Input.GetKey (KeyCode.E));


			if (Input.GetKeyDown (KeyCode.Q) && !hasTorch) {
				if (numTorches >= 1) {
					hasTorch = true;
				}
			}
			//TODO(DWenzel): probably define a clear keymap at some point (Soon!)
			if (Input.GetKeyDown (KeyCode.Space) && hasTorch) {
				ThrowLight ();
			}
	}

	public bool ConsumeKey() {
		if (keys > 0) {
			keys--;
			doorSound.Play ();
			return true;
		} return false;
	}

	void ThrowLight() {
		Debug.Log ("Throwing light");
		GameObject temp = (GameObject)Instantiate (lightSource, LightLauncher.position, LightLauncher.rotation);

		if (temp == null) {
			Debug.Log ("Failed to instantiate light source, This should not be");
			return;
		}
		illumination--;
		numTorches--;
		hasTorch = false;
		Rigidbody tRigid = temp.GetComponent<Rigidbody> ();
		Vector3 toss = playerRb.transform.forward;
		toss.y += .3f;
		tRigid.AddForce (toss * throwLightForce);
	}

	void PullOutTorch (){
		if (!hasTorch && numTorches > 0) {
			hasTorch = true;
		}
	}

    public int GetSKeys()
    {
        return skelekey;
    }

    public int GetHealth()
    {
        return hitPoints;
    }

    public int GetDeaths()
    {
		return PlayerPrefs.GetInt ("Deaths");
    }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt("Gold");
    }

    public int GetTorches()
    {
        return numTorches;
    }

    public int GetKeys()
    {
        return keys;
    }

	void OnTriggerExit(Collider other) {
		if (other.CompareTag ("light")) {
			illumination--;
		}
	}

	void OnParticleCollision(GameObject other) {
        PlayerPrefs.SetInt("Gold", ++goldCounter);
        Debug.Log ("collisions: " + goldCounter);
		coinSound.Play ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Torch")) {
			Debug.Log ("Picking up Light");
			illumination--;
			hasTorch = true;
			numTorches++;
			other.GetComponent<TorchBurnDownScript>().BurnDownTimer = 0.01f;
		}
		if (other.CompareTag ("light")) {
			illumination++;
		}
		if (other.CompareTag ("Key")) {
			keys++;
			keySound.Play ();
			Destroy (other.gameObject);
		}
        if (other.CompareTag("SKey"))
        {
            skelekey++;
            keySound.Play();
            Destroy (other.gameObject);
        }

		if (other.CompareTag ("Pitfall")) {
            //	debugText.text = "You're Dead!";
            KillPlayer();
		}

        if (other.CompareTag("BossRoom"))
        {
            Debug.Log("In boss room");
            inBossRoom = true;
        }
    }
	void OnCollisionEnter(Collision other) {
		if (other.collider.CompareTag ("Enemy") && !invulnerable) {
//			debugText.text = "hit";
			TakeDamage (other.collider.transform.position);
//			other.gameObject.GetComponent<CreatureScript> ().DoPushback (transform.position);
		}
    }

	void TakeDamage(Vector3 enemyLocation) {
		hitPoints--;
		//Debug.Log ("HP: " + hitPoints);
		damageSound.Play ();
		Instantiate (bloodSplatter, transform.position, transform.rotation);
//		debugText.text = "HP remaining: " + hitPoints;
		if (hitPoints <= 0) {
			KillPlayer ();
		} 
			DamageKnockback (enemyLocation);
	}

	public bool isIlluminated () {
		return illumination > 0;
	}

	void RestoreHealth() {
		hitPoints++;
	}

	void KillPlayer() {
		PlayerPrefs.SetInt("Gold", goldCounter);
		PlayerPrefs.SetInt("Deaths", ++deathCounter);
        Debug.Log("Deaths: " + GetDeaths());
        if (inBossRoom == true)
        {
            playerDeath.Play(deathStateHash);
            StartCoroutine(respawnTimer());
        }
        else
        {
            SceneManager.LoadScene(currentScene);
        }
    }
	/*
	Take control away from the player, push player back, return control to player
	*/
	void DamageKnockback(Vector3 enemyLocation) {
		Vector3 pushMe = playerRb.transform.position - enemyLocation;
		pushMe.y = 0.0f;
		StartCoroutine (InvulnStart ());
		StartCoroutine (freezeControl ());
		playerRb.AddForce (pushMe * pushBack);
	}

	IEnumerator InvulnStart() {
		invulnerable = true;
		yield return new WaitForSeconds (invulnTimer);
		invulnerable = false;
	}

	IEnumerator freezeControl () {
		lockControls = true;
		yield return new WaitForSeconds(controlLockTime);
		lockControls = false;
	}

    IEnumerator respawnTimer()
    {
        lockControls = true;
        yield return new WaitForSeconds(3);
        lockControls = false;
        Respawn();
        playerDeath.Play(normalStateHash);
    }

    void Respawn()
    {
          transform.position = respawnPoint.transform.position;
          hitPoints = 3;
          numTorches = 3;
    }
}
