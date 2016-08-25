using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {
    
    public Button startText;
    public Button exitText;
    public Button bossRunText;
    public PlayerController player;

    private int SKey;

	// Use this for initialization
	void Start ()
    {
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        bossRunText = bossRunText.GetComponent<Button>();
        PlayerPrefs.SetInt("Gold", 0);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        SKey = player.GetSKeys();
        if (SKey > 0)
        {
            bossRunText.gameObject.SetActive(true);
        }
        player.lockControls = true;
        exitText.gameObject.SetActive(false);
    }
    
    public void ExitPress()
    {
        Application.Quit();
    }

    public void StartBoss()
    {
        SceneManager.LoadScene("Boss Run");
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Forest Level");
    }

}
