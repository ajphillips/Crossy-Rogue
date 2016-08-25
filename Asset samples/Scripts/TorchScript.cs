using UnityEngine;
using System.Collections;

public class TorchScript : MonoBehaviour {

	public GameObject LightComponents;
	public float snuffSeconds = 15.0f;
	public float burnSeconds = 90.0f;
	public bool isActive = true;

	private bool lit;
//	private bool thrown;
	private float burnDownSeconds;

	// Use this for initialization
	void Start () {
		lit = true;
//		thrown = false;
		burnDownSeconds = snuffSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		if (lit) {
			LightComponents.SetActive (true);
//			if (thrown) {
				burnDownSeconds -= Time.deltaTime;
//			}
			burnSeconds -= Time.deltaTime;
			if (burnSeconds <= 0 || burnDownSeconds <= 0) {
				lit = false;
			}
		}
			LightComponents.SetActive (lit);
		if (burnSeconds <= 0) {
			isActive = false;
//			if (thrown) {
				Destroy (gameObject);
//			}
		}
	}
	public void LightTorch () {
		lit = true;
		LightComponents.SetActive (lit);
	}

	public void ThrowTorch () {
//		thrown = true;
	}
	public float getSnuffSeconds() {
		return snuffSeconds;
	}

	public void setSnuffSeconds(float snuffSeconds) {
		this.burnDownSeconds = snuffSeconds;
	}
	public float getBurnSeconds() {
		return burnSeconds;
	}
	public void setBurnSeconds(float timeRemaining) {
		burnSeconds = timeRemaining;
	}

	public void PickupTorch () {
//		thrown = false;
		burnDownSeconds = snuffSeconds;
	}

}
