using UnityEngine;
using System.Collections;

public class Thing : MonoBehaviour {

	protected float burnTime = 5.0f;
	protected bool burning = false;

	public ParticleSystem fire;

	protected virtual void Burn() {
		if (fire != null) {
			Debug.Log ("Burnination!");
			burning = true;
			fire.Play ();
			StartCoroutine (DieInXSeconds ());
		}
	}
	private void Kill() {
		Destroy (gameObject);
	}
	protected virtual void Interact() {
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Interact")) {
			
			Interact ();
		}

	}

	void OnCollisionEnter (Collision other) {
		if (other.collider.CompareTag ("Torch")) {
			Burn ();
		}
	
	}
	IEnumerator DieInXSeconds() {
		yield return new WaitForSeconds (burnTime);
		Debug.Log ("object should be Destroyed now");
		Destroy (gameObject);

	}

}
