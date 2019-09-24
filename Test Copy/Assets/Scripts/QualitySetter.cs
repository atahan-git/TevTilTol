using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QualitySetter : MonoBehaviour {

	public Slider mySlider;

	// Use this for initialization
	void Awake () {
		//print ("this called");
		mySlider.value = PlayerPrefs.GetInt ("Quality", 5);
		QualitySettings.SetQualityLevel((int)mySlider.value, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ValueChanged () {


		QualitySettings.SetQualityLevel((int)mySlider.value, true);
		PlayerPrefs.SetInt ("Quality", (int)mySlider.value);
	}
}
