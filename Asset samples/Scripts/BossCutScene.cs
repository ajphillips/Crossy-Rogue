using UnityEngine;
using System.Collections;

public class BossCutScene : MonoBehaviour {

	Collider SceneStart;
	GameObject BlackKnight;
	GameObject player;
	void Start () {
		BlackKnight = GameObject.Find ("BlackKnightPrefab");
		player = GameObject.Find ("PlayerRoot");
	}

	void CutScene() {
//		takeControl ();
//		moveCamera (BlackKnight);
//		BlackKnight.RunToDeath (deathtrigger);
//		BlackKnight.DeathAnimation ();
//		moveCamera (player);
//		releaseControl ();
	}

}
