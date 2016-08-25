using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SignBrainScript : Thing {

	private Text SignContent;
    public Canvas canvas;

    void Start()
    {
        canvas.enabled = false;
    }

	protected override void Interact ()
	{
        //TODO: Display contents of sign here.
        canvas.enabled = true;
		base.Interact ();
	}

    void OnTriggerEnter(Collider other)
    {
        canvas.enabled = true;
    }

	void OnTriggerExit() {
        //hide the tutorial text
        canvas.enabled = false;

    }

}
