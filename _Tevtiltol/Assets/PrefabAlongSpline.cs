using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//[ExecuteInEditMode]
public class PrefabAlongSpline : MonoBehaviour {


	public GameObject myPrefab;
	public float prefabSize = 1f;
	public Quaternion prefabRotation = Quaternion.identity;
	//public Vector3 prefabOffset = Vector3.zero;

	GameObject[] myObjects = new GameObject[0];
	int size = 0;

	public bool editModeRender = false;

	//-------------------------------------------------------- INSTANTIATE STUFF

	void Start () {
		/*foreach (Transform chld in transform) {
			DestroyImmediate (chld.gameObject);
		}
		foreach (Transform chld in transform) {
			DestroyImmediate (chld.gameObject);
		}*/
		DestroyOldObjects ();
		OnValidate ();
	}

	public void Update (){
		OnValidate ();
	}

	int n = 0;
	public void OnValidate () {
		if (myPrefab == null)
			return;

		if (Application.isEditor && !editModeRender && !Application.isPlaying)
			return;

		//print ("on validate");
		n = 0;
		DestroyOldObjects ();
		Vector3 p0 = points[0];
		for (int i = 1; i < points.Length; i += 1) {
			//print (i + " " + i);
			Vector3 p1 = points[i];
			CheckSize (p0, p1);
			p0 = p1;
		}


		//ResizeObjects ();
	}

	int oldSize;
	float distance;
	float oldDistance;
	void CheckSize (Vector3 pointA, Vector3 pointB) {
		//print ("check size");
		distance = Vector3.Distance (pointA, pointB);

		size = (int)(distance/prefabSize) + 1;

		//print (distance + " " + size);

		//if (size != oldSize) {
			DrawPrefabs (pointA, pointB, size);
		//} else if (distance != oldDistance) {
			//DrawPrefabs (pointA, pointB, size);
			//ResizeObjects (pointA, pointB);
		//}

		oldSize = size;
		oldDistance = distance;
	}

	void DrawPrefabs (Vector3 pointA, Vector3 pointB, int mySize) {
		
		myObjects = new GameObject[mySize];
		for (int i = 0; i < mySize; i++) {
			//print (mySize + " " + i);
			myObjects [i] = (GameObject)Instantiate (myPrefab, Vector3.zero, Quaternion.identity);
			myObjects [i].name = myObjects [i].name + " " + n.ToString();
			n++;
			myObjects [i].transform.parent = transform;
		}

		ResizeObjects (pointA, pointB);
	}


	void ResizeObjects (Vector3 pointA, Vector3 pointB) {
		Quaternion objRot = Quaternion.LookRotation (pointA - pointB);
		for (int i = 0; i < size; i++) {
			myObjects [i].transform.position = Vector3.Lerp (pointA, pointB, (1f / ((float)size)) * ((float)i + 1));
			myObjects [i].transform.position += transform.position;
			//print ((1f / ((float)size + 1f)) * ((float)i + 1));
			myObjects [i].transform.rotation = objRot;
			//myObjects [i].transform.position += myObjects [i].transform.InverseTransformDirection (prefabOffset);
			//myObjects [i].transform.localRotation *= prefabRotation;
			float s = distance / (prefabSize * (float)size);
			myObjects [i].transform.localScale = new Vector3 (1f,1f,s);
		}
	}


	public void DestroyOldObjects () {

		var children = new List<GameObject>();
		foreach (Transform child in transform) children.Add(child.gameObject);

		if (Application.isEditor)
			children.ForEach(child => DestroyImmediate(child));
		else
			children.ForEach(child => Destroy(child));

		/*foreach (Transform chld in parentObj) {
			DestroyImmediate (chld.gameObject);
		}
		if (myObjects.GetLength(0) > 0) {
			foreach (GameObject obj in myObjects) {
				if (obj != null)
					DestroyImmediate (obj.gameObject);
			}
		}*/
		/*
		foreach (Transform chld in transform) {
			if (Application.isEditor)
				DestroyImmediate(chld.gameObject);
			else
				Destroy(chld.gameObject);

			try{
				DestroyImmediate (chld.gameObject);
			}catch{
			}
			try{
				Destroy (chld.gameObject);
			}catch{
			}
		}
		*/
		/*foreach (Transform chld in transform) {
			try{
			Destroy (chld.gameObject);
			}catch{
			}
		}*/
	}











	//-------------------------------------------------------- LINE STUFF

	public Vector3[] points = new Vector3[2];

	public Vector3 GetPoint (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 2;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 1;
		}
		return transform.TransformPoint(Line.GetPoint(
			points[i], points[i + 1], t));
	}

	public Vector3 GetVelocity (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 2;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 1;
		}
		return transform.TransformPoint(Line.GetFirstDerivative(
			points[i], points[i + 1],  t)) - transform.position;
	}

	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}

	public int CurveCount {
		get {
			return (points.Length - 1) / 1;
		}
	}

	public void Reset () {
		points = new Vector3[] {
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f)
		};
	}

	public void AddCurve () {
		Vector3 point = points[points.Length - 1];
		Array.Resize(ref points, points.Length + 1);
		point.x += 1f;
		points[points.Length - 1] = point;
	}
}

public static class Line {

	public static Vector3 GetPoint (Vector3 p0, Vector3 p1, float t) {
		t = Mathf.Clamp01(t);

		return Vector3.Lerp (p0, p1, t);
		/*float oneMinusT = 1f - t;
		return
			oneMinusT * oneMinusT * oneMinusT * p0 +
			3f * oneMinusT * oneMinusT * t * p1 +
			3f * oneMinusT * t * t * p2 +
			t * t * t * p3;*/
	}

	public static Vector3 GetFirstDerivative (Vector3 p0, Vector3 p1, float t) {
		t = Mathf.Clamp01(t);
		float oneMinusT = 1f - t;
		return
			Vector3.Normalize(p1 - p0);
	}
		
}
