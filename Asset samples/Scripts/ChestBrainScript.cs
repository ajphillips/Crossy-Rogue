using UnityEngine;
using System.Collections;

public class ChestBrainScript : Thing {


	private bool isOpened;
	public ParticleSystem goldFountain;
	// Use this for initialization
	void Start () {
		isOpened = false;
	}

	public bool open() {

		return isOpened;
	}

	protected override void Interact() {
		if (!isOpened) {
			isOpened = true;
			goldFountain.Play ();
			GetComponent<AudioSource> ().Play ();
            gameObject.GetComponent<Animation>().Play("ChestOpen");


        }
    }

}
