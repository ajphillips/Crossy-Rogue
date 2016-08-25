using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FinalChestBrainScript : Thing {

    
    public EndGameScript endingStart;

	private bool isOpened;
	public ParticleSystem goldFountain;
    
    // Use this for initialization
    void Start () {
		isOpened = false;
        endingStart = GameObject.FindGameObjectWithTag("Ending").GetComponent<EndGameScript>();
    }

	public bool open() {

		return isOpened;
	}

	protected override void Interact() {
		if (!isOpened) {
			isOpened = true;
			goldFountain.Play ();
            gameObject.GetComponent<Animation>().Play("ChestOpen");
            StartCoroutine(endSequence());
        }
    }
    IEnumerator endSequence()
    {
        yield return new WaitForSeconds(6);
        endingStart.isAnimating = true;
    }
}
