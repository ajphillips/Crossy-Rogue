using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour {
	public void Quit()
	{
        SceneManager.LoadScene("Menu Scene");
	}

}
