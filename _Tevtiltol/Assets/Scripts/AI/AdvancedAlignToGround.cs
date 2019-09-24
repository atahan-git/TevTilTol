using UnityEngine;
using System.Collections;

public class AdvancedAlignToGround : MonoBehaviour {

	/*--------------------------------------------
	 * 
	 * 	This Script will make the object align
	 * 	to the ground's normal. make sure to 
	 * 	set LayerMask correctly to make it work
	 * 	LayerMask should olny include ground
	 * 
	 *--------------------------------------------
	 */



	public float alignTime = 5f;

	public float groundOffset = 0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		RaycastHit hit = new RaycastHit ();
		Ray myRay = new Ray ();
		int layerMask = 2048;

		myRay = new Ray (transform.parent.position + transform.parent.up * 5f, new Vector3(0,-1,0));
		if (Physics.Raycast (myRay, out hit, 100, layerMask)) {
			Debug.DrawLine (myRay.origin, hit.point, Color.blue);
			//Vector3 normal = hit.normal;
		}

		Quaternion rot = Quaternion.FromToRotation (transform.parent.up, hit.normal) * transform.parent.rotation;

		layerMask = 6144;

		myRay = new Ray (transform.parent.position + transform.parent.up * 1f, new Vector3(0,-1,0));
		if (Physics.Raycast (myRay, out hit, 100, layerMask)) {
			Debug.DrawLine (myRay.origin, hit.point, Color.red);
			//Vector3 normal = hit.normal;
		}

		transform.rotation = Quaternion.Slerp (transform.rotation, rot, alignTime * Time.deltaTime);
		transform.position = Vector3.Lerp (transform.position, hit.point + new Vector3(0,groundOffset,0), alignTime * Time.deltaTime);
	}
}
