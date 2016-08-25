using UnityEngine;
using System.Collections;

public class DestroyPlatforms : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			GameObject[] platforms = GameObject.FindGameObjectsWithTag ("Deathtrap");
			foreach (GameObject platform in platforms) {
				GameObject.Destroy (platform);
			}
		}
	}
}
