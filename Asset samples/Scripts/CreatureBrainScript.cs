using UnityEngine;
using System.Collections;

public class CreatureBrainScript : Thing {

	private static Vector3 NONE = new Vector3( -42, -42, -42 );
	/**
	 * If the creature's vision intersects with the light radius,
	 * the creature will wander towards and investigate that position
	 * If the vision hits the player, AND the player is illuminated, 
	 * the creature will pursue the player until the player is outside of 
	 * the chaseRange, or until the player loses illumination status.
	 * In both cases, the creature should continue to move towards the last
	 * location it saw the player.
	 */

	public float wanderRange = 5.0f;
	public float maxRange = 10.0f;
	public float wanderAngle = 0.5f;
	public float closeEnough = 0.5f;
	public float fieldOfVision = 110.0f;

	protected Transform player;
	private float fleeDistance = 2.0f;
	private Vector3 currentGoal;
	private SphereCollider vision;
	private Vector3 nextStep;
	private bool chasing;
	private NavMeshAgent agent;
	protected Emotion emotion; 
	protected PlayerController playerScript;

    //This is here so we can get the animation component for animation changing enemies
    protected Animation animChange;

	/*
	 * Emotion object determines creatures current status
	 * CALM: Wander randomly
	 * triggered when currentGoal = null
	 * CURIOUS: Wander towards currentGoal
	 * triggered when currentGoal != null && !chasing
	 * CHASING:	update, and move towards currentGoal
	 * triggered when currentGoal != null && chasing
	 * BURNING: Select a random location, move towards it
	 * triggered when hit by a torch
	*/

	protected enum Emotion {
		Calm,
		Curious,
		Chasing,
		Scared,
		Burning
	};


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerRoot").transform;
		currentGoal = NONE;
		nextStep = NONE;
		chasing = false;
		agent = GetComponent<NavMeshAgent> ();
		vision = GetComponent<SphereCollider> ();
		playerScript = player.GetComponent<PlayerController> ();
        animChange = GetComponent<Animation>();	
		emotion = Emotion.Calm;

	}
	
	// Update is called once per frame
	virtual protected void Update () {
		if (burning) {
			emotion = Emotion.Burning;
		} else 

		//decide what to do
		if (PlayerInLOS ()) {
			emotion = Emotion.Chasing;
			currentGoal = player.position;
		} else if (currentGoal != NONE) {
			if (emotion != Emotion.Chasing) {
				emotion = Emotion.Curious;
				nextStep = Investigate (currentGoal, wanderAngle);
			}
		} else {
			if (emotion != Emotion.Scared) {
				emotion = Emotion.Calm;
				currentGoal = RandomWalk (wanderRange); //this sets currentGoal
				}
		}

		//take action towards your goal
		switch (emotion) {
		case Emotion.Burning:
			agent.speed = 5.0f;
			agent.destination = RandomNavSphere (transform.position, fleeDistance);
			break;
		case Emotion.Chasing:
			agent.destination = currentGoal;
			break;
		case Emotion.Curious:
			agent.destination = nextStep;
			break;
		default: //emotion == calm
			nextStep = NONE;
			break;

		}
		//clear currentGoal if within closeEnough
		float distance = Vector3.Distance(transform.position,currentGoal);
		if (distance <= closeEnough) {
			currentGoal = NONE;
			DoAttack ();
			//TODO(DWenzel): this is also maybe where you want to trigger an attack if chasing??
		}
	}
	/*
	Define Behaviors here.
	*/
	protected virtual void DoAttack() {} //override me in each monster's brain.


	void OnTriggerStay(Collider other) {
		if (other.CompareTag ("light") && emotion != Emotion.Chasing) {
			currentGoal = other.transform.position;
		}
	}
	/*
	 * This will be overloaded by the specific creature script to define
	 * what exactly a particular creature desires. E.g. a goblin seeks out 
	 * illuminated players, spiders run from lights, etc.
	 * @return	Returns true if the player is in line of sight of the creature
	*/
	protected virtual bool PlayerInLOS () {
		return false;
	}

	/*
	* Assigns a random target within a distance set by the range parameter
	* This will cause the creature to become curious about that location, and it
	* **should** start wandering towards the target
	*
	* @param 	range	 a float giving the max distance of the Random Walk
	* @return	Returns a Vector3 of a random location in the navmesh within range
	*/
	Vector3 RandomWalk(float range){
		return RandomNavSphere (transform.position, range);
	}

	/*
	 *Sets nextStep to a random position within a spherical area between the creature
	 * and the position of the curiosity
	 * 
	 * @param	curiosity	location of the triggering target
	 * @param	size		Scale of random sphere, should be a float between 0.5f and 
	 * some small number, lets say; 0.05f 
	*/
	Vector3 Investigate(Vector3 curiosity, float size) {
		if (nextStep == NONE || Vector3.Distance(nextStep, transform.position)<1.0f) {
			Vector3 between = (curiosity - transform.position) * size;
			if (between.sqrMagnitude < 4.0f) //squares are cheaper
				return curiosity;
			float radius = Vector3.Magnitude (between);
		Vector3 randomSphere = RandomNavSphere (between, radius);
		return randomSphere;
		}
		return nextStep;
	}

	/*
	 * 					##**NOTE**##
	* This function is derived wholly from the following forum post:
	* http://forum.unity3d.com/threads/solved-random-wander-ai-using-navmesh.327950/
	* Returns a Vector3 of a random location within a sphere of radius distance,
	* centered on origin
	* 
	* @param	origin		Position of the center of the sphere to be used
	* @param	distance	Max distance the returned location can be from 
	* 						origin
	* @return	Vector3
	*/
	static Vector3 RandomNavSphere(Vector3 origin, float distance) {
		Vector3 randomDir = UnityEngine.Random.insideUnitSphere * distance;

		randomDir += origin;
		NavMeshHit navHit;

		NavMesh.SamplePosition (randomDir, out navHit, distance, -1);
		return navHit.position;
	}


}