using UnityEngine;
using System.Collections;

public class SpiderBrainScript : CreatureBrainScript {



	protected override void Update () {
		//do stuff
		if (playerScript.isIlluminated() && (player.position - transform.position).magnitude > maxRange) {
			
		}
		base.Update ();
	}




	
	protected override bool PlayerInLOS () {

		//	**This is default behavior for Spiders**

		//if distance > range bail early
		Vector3 targetVector = player.position - transform.position;
		if (targetVector.magnitude > maxRange) {
			emotion = Emotion.Calm;
			return false;
		}
		//if Player.isIlluminated, bail early
		if (playerScript.isIlluminated ()) {
			emotion = Emotion.Scared;
			return false;
		}
		//Check to see if the player is within the creature's Field of Vision
		float angle = Vector3.Angle(targetVector,transform.forward);
		if (angle < fieldOfVision * 0.5f) {
			RaycastHit hit;
			if (Physics.Raycast(transform.position,targetVector.normalized,out hit)) {

				if (hit.transform == player) {
					return true;
				} else
					return false;
			}	
		}

		//Otherwise, shoot ray, if you hit the player, return true 


		return false;
	}

}
