using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GoalBoundScript : MonoBehaviour {

	public string nextLevel;
    public string currentLevel;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    void OnTriggerEnter(Collider other)
    {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(nextLevel);
            }
    }
}
