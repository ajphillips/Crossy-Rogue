using UnityEngine;
using System.Collections;

public class GobSkeleBrainScript : CreatureBrainScript
{

    /*
	 * There are clues to what you want here:
	 * https://unity3d.com/learn/tutorials/projects/stealth/enemy-sight
	 * 
	*/

    protected override void Burn()
    {
        if (fire != null)
        {
            Debug.Log("Bye bye Goblin! HELLO Skeleton!");
            burning = true;
            fire.Play();
            Debug.Log("Waiting for change in seconds function after fire has started");
            StartCoroutine(ChangeInXSeconds());
        }
    }
    protected override bool PlayerInLOS()
    {

        //	**This is default behavior for goblins**
        //if !Player.illuminated, bail early
        if (!playerScript.isIlluminated())
        {
            return false;
        }
        //if distance > range bail early
        Vector3 targetVector = player.position - transform.position;
        if (targetVector.magnitude > maxRange)
        {
            return false;
        }
        //Check to see if the player is within the creature's Field of Vision
        float angle = Vector3.Angle(targetVector, transform.forward);
        if (angle < fieldOfVision * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetVector.normalized, out hit))
            {

                if (hit.transform == player)
                {
                    return true;
                }
                else
                    return false;
            }
        }


        //Otherwise, shoot ray, if you hit the player, return true 


        return false;
    }

    IEnumerator ChangeInXSeconds()
    {
        yield return new WaitForSeconds(1.0f);
        animChange.Stop();
        animChange.Play("SkeleMove");
        fire.Stop();
        Debug.Log("Skeleton time!");
    }
}
