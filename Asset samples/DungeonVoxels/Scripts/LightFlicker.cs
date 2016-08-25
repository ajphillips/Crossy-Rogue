using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {
     public float minIntensity = 0.25f;
     public float maxIntensity = 0.5f;
	public float lightPosition = .003f;
     float random;
     void Start(){
        random = Random.Range(0.0f, 65535.0f);

     }
 
     void Update(){
		Vector3 jiggle = new Vector3 (Random.Range(-lightPosition,lightPosition),Random.Range(-lightPosition,lightPosition),Random.Range(-lightPosition,lightPosition));
         float noise = Mathf.PerlinNoise(random, Time.time);
         GetComponent<Light>().intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
		GetComponent<Light> ().transform.position += jiggle;
     }
}
