using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrightnessAdjust : MonoBehaviour {

	public Slider slider;

	void Update()
	{
		AdjustBrightness ();
	}

	public void AdjustBrightness()
	{
		RenderSettings.ambientIntensity = slider.value;
	}
}
