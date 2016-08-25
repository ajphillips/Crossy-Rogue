using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	protected Transform player;
	protected PlayerController playerScript;
	public AudioSource backGroundMusic;
	public AudioSource alternateMusic;

	// Use this for initialization
	void Start () {
		backGroundMusic.Play ();
		player = GameObject.Find ("PlayerRoot").transform;
		playerScript = player.GetComponent<PlayerController> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!playerScript.isIlluminated () && backGroundMusic.isPlaying ) {
			backGroundMusic.Stop ();
			alternateMusic.Play ();

		}
		if (playerScript.isIlluminated () && alternateMusic.isPlaying) {
			alternateMusic.Stop ();
			backGroundMusic.Play ();
		}
		
	}

		

}
