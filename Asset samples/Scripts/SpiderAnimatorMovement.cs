using UnityEngine;
using System.Collections;

public class SpiderAnimatorMovement : MonoBehaviour {

    int inc;
    public int ZRotation;
    public int reverseZRotation;

	void Start () {
        inc = 0;
	}
	
	void Update () {
        if (inc < 25 )
        {
            transform.Rotate(new Vector3(0, 0, ZRotation));
            inc++;
        }
        else if (inc >= 25 && inc < 50)
        {
            transform.Rotate(new Vector3(0, 0, reverseZRotation));
            inc++;
        }
        else
        {
            inc = 0;
        }
    }
}
