using UnityEngine;
using System.Collections;

public class SkeletonKingAttack : MonoBehaviour {
    private Animator anim;
    private BossAnimationToggleScript bossHiveMind;
    private int firstStateHash = Animator.StringToHash("SkeletonKingRise");
    //private int secStateHash = Animator.StringToHash("SkeletonKingRise 0");

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        bossHiveMind = GameObject.FindGameObjectWithTag("Bosses").GetComponent<BossAnimationToggleScript>();
    }
    

   

        // This is called as an animation event inside the animation clip from the Animation component
        //      not to be confused with the Animator component which the rest of this script utilizes instead
        
    void finishAnim()
    {
        bossHiveMind.bossesAnimating = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (bossHiveMind.bossesAnimating == false)
        {
            if (other.CompareTag("Player"))
            {
                AnimateFirst();
                bossHiveMind.bossesAnimating = true;
            }
        }  
    }

    void OnTriggerStay(Collider other)
    {
        if (bossHiveMind.bossesAnimating == false)
        {
            if (other.CompareTag("Player"))
            {
                AnimateFirst();
                bossHiveMind.bossesAnimating = true;
            }
        }
    }

    void AnimateFirst()
    {
          anim.Play(firstStateHash);
    }
}
