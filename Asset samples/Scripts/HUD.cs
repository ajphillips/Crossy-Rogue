using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    public Sprite[] hearts;
    public Image heartUI;
    public Text coins;
    public Text torches;
    public Text keys;

    private PlayerController player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Debug.Log(player.GetHealth());
    }

    void Update()
    {
        if (player != null)
        {
            heartUI.sprite = hearts[player.GetHealth()];
            coins.text = player.GetCoins().ToString();
            torches.text = player.GetTorches().ToString();
            keys.text = player.GetKeys().ToString();
        }
    }
}
