using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour {

    public bool isAnimating;
    public Text goldText;
    public Text deathText;
    public Text scoreText;
    public Text sKeyText;
    private Animator endAnim;
    private int firstStateHash = Animator.StringToHash("EndingTextAnimation");
    public PlayerController playerLock;

    // Use this for initialization
    void Start () {
        isAnimating = false;
        endAnim = GetComponent<Animator>();
        playerLock = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (isAnimating)
        {
            startPlaying();
            playerLock.lockControls = true;
        }
	}

    void StopAnim()
    {
        if (playerLock.GetSKeys() <= 0)
        {
            endAnim.Stop();
        }
    }

    void StopAfterSKeys()
    {
        endAnim.Stop();
    }

    void UpdateGold()
    {
        goldText.text = "Gold: " + playerLock.GetCoins();
    }

    void UpdateDeaths()
    {
        deathText.text = "Deaths: " + playerLock.GetDeaths();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + (playerLock.GetCoins() - (playerLock.GetDeaths() * 20));
    }
    void startPlaying()
    {
        endAnim.Play(firstStateHash);
        StartCoroutine(goToMenuSoon());
    }

    IEnumerator goToMenuSoon()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Menu Scene");
    }
}
