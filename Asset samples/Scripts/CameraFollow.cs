using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	private Vector3 vel = Vector3.zero;
	private float dTime = 0.3f;
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		Quaternion target = Quaternion.LookRotation (player.transform.position - transform.position);

		transform.rotation = Quaternion.Slerp(transform.rotation, target, 1.0f * Time.deltaTime);

//		transform.LookAt (player.transform.position);
		transform.position = Vector3.SmoothDamp (transform.position, player.transform.position + offset,ref vel, dTime);
			//player.transform.position + offset;
	}

}
