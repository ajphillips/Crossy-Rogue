using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	public Transform PauseBackground;
	public Transform pauseMenu;
	public Transform videoMenu;
	public Transform soundMenu;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Pause ();
		}

	}
	public void Pause()
	{
		if (PauseBackground.gameObject.activeInHierarchy == false) {
			if (pauseMenu.gameObject.activeInHierarchy == false) 
			{
				pauseMenu.gameObject.SetActive (true);
				soundMenu.gameObject.SetActive (false);
				videoMenu.gameObject.SetActive (false);
			}
				PauseBackground.gameObject.SetActive (true);
				Time.timeScale = 0;
		} else 
		{
			PauseBackground.gameObject.SetActive (false);
			Time.timeScale = 1;
		}
	}
	public void Sound(bool Open)
	{
		if (Open)
		{
			soundMenu.gameObject.SetActive (true);
			pauseMenu.gameObject.SetActive (false);
		}
		if (!Open) 
		{
			soundMenu.gameObject.SetActive (false);
			pauseMenu.gameObject.SetActive (true);
		}
	}
	public void Video(bool Open)
	{
		if (Open)
		{
			videoMenu.gameObject.SetActive (true);
			pauseMenu.gameObject.SetActive (false);
		}
		if (!Open) 
		{
			videoMenu.gameObject.SetActive (false);
			pauseMenu.gameObject.SetActive (true);
		}
	}
}
