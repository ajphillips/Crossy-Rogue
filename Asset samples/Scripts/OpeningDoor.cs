using UnityEngine;
using System.Collections;

public class OpeningDoor : Thing {


    private bool guiShow = false;
    private bool isOpen = false;
    private bool isLocked = true;
	private PlayerController controller;

	private void Start() {
		GameObject obj = GameObject.Find ("PlayerRoot");
		controller = obj.GetComponent<PlayerController> ();
	}

    public bool open()
    {
        return isOpen;
    }

    protected override void Interact()
    {
		if (controller == null) {
			Debug.Log ("No controller found");
		}
		if (!isOpen && controller.ConsumeKey())
        {
            isOpen = true;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            gameObject.GetComponent<Animation>().Play("DoorOpen");

            isLocked = false;
            /*if(gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                gameObject.GetComponent<MeshCollider>().enabled = true;
            }*/

            return;
        }
        //Currently not working. 
       /*else if (isOpen)
        {
            isOpen = false;
            gameObject.GetComponent<Animation>().Play("DoorClose");
            return;
        }*/
    }
}
