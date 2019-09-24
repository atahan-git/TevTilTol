using UnityEngine;
using System.Collections;

public class KapiScript : MonoBehaviour {

	public Transform rotateObject;

	Quaternion goToRotation;
	public float speed = 5f;

	public float openDegree = 110f;
	float closeDegree = 0f;

	bool currentState = false;

	float threshold = 0.1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void OnMouseDown (){

		if (currentState) {
			goToRotation.eulerAngles = new Vector3 (0, closeDegree, 0);
			currentState = false;
			StartCoroutine (RotatePls());
		} else {
			goToRotation.eulerAngles = new Vector3 (0, openDegree, 0);
			currentState = true;
			StartCoroutine (RotatePls());
		}

	}

	IEnumerator RotatePls () {

		while (Quaternion.Angle (rotateObject.localRotation, goToRotation) < threshold) {
			
			rotateObject.localRotation = Quaternion.RotateTowards (rotateObject.localRotation, goToRotation, speed * Time.deltaTime);

			yield return 0;
		}
		yield return 0;
	}
		
}
