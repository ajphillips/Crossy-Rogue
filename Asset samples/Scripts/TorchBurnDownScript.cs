using UnityEngine;
using System.Collections;

public class TorchBurnDownScript : MonoBehaviour {

	public float BurnDownTimer = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		BurnDownTimer -= Time.deltaTime;
	
		if (BurnDownTimer <= 0) {
			Destroy (gameObject);
		}

	}
}
