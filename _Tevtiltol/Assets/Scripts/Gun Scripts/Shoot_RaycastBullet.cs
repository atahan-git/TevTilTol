using UnityEngine;
using System.Collections;
//using EZObjectPools;
//using EZEffects;

public class Shoot_RaycastBullet : MonoBehaviour {

	[HideInInspector]
	public GunSharedValues val;


	public GameObject bulletHoleDecal;
	LineRenderer myLine;
	//public EffectMuzzleFlash MuzzleEffect;
	public GameObject muzzleEffect;
	public AudioSource Audio;

	[HideInInspector]
	public float z = 10f;
	// Use this for initialization
	void Start () {

		if (val == null)
			val = GetComponent<GunSharedValues> ();
		if (val == null)
			val = GetComponentInParent<GunSharedValues> ();
		if (val == null)
			val = GetComponentInChildren<GunSharedValues> ();

		//MuzzleEffect.SetupPool();
		myLine = GetComponent<LineRenderer> ();

	
	}
	
	// Update is called once per frame
	void Shoot(){

		//MuzzleEffect.SetupPool ();
		//  The Ray-hits will be in a circular area
		float randomRadius = Random.Range (0f, (float)val.accuracy);

		float randomAngle = Random.Range (0f, 2f * Mathf.PI);

		//Calculating the raycast direction
		Vector3 direction = new Vector3 (
			randomRadius * Mathf.Cos (randomAngle),
			randomRadius * Mathf.Sin (randomAngle),
			z
		);

		//Make the direction match the transform
		//It is like converting the Vector3.forward to transform.forward
		direction = val.myBulletSource.TransformDirection (direction.normalized);

		//Raycast and debug
		Ray r = new Ray (val.myBulletSource.position, direction);
		RaycastHit hit;        

		int layerMask = 1023;
        //print(layerMask);

		if(val.isPlayer)
			layerMask = 511;
		
		if (Physics.Raycast (r, out hit, Mathf.Infinity, layerMask)) {

			//deal damage
			//if (hit.collider.gameObject.tag == "Enemy") {

			Hp hp = hit.collider.gameObject.GetComponent<Hp> ();
			if (hp == null)
				hp = hit.collider.gameObject.GetComponentInParent<Hp> ();
			if (hp == null)
				hp = hit.collider.gameObject.GetComponentInChildren<Hp> ();
			if (hp != null)
				hp.Damage (val.damage);

			Health health = hit.collider.gameObject.GetComponent<Health> ();
			if (health == null)
				health = hit.collider.gameObject.GetComponentInParent<Health> ();
			if (health == null)
				health = hit.collider.gameObject.GetComponentInChildren<Health> ();
			if (health != null)
				health.Damage (val.damage, transform);

			//}

			/*
			if (your.bacı == false) {
				set.anan = true
			}
			*/

			//decals
			if (!health && hit.collider.gameObject.tag != "GunDrop") {
				GameObject myDecal = (GameObject)Instantiate (bulletHoleDecal, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal));
				myDecal.transform.parent = hit.collider.gameObject.transform;
			}
		} else {
			hit.point = (r.direction * 20) + r.origin;
		}

		//-------------------------
		//gfc
		Debug.DrawLine (val.barrelPoint.position, hit.point); 
		if (myLine) {
			myLine.enabled = true;
			myLine.SetPosition (0, val.barrelPoint.position);
			myLine.SetPosition (1, hit.point);
			Invoke ("RemoveLine", 0.1f);
		}
		//MuzzleEffect.ShowMuzzleEffect(val.barrelPoint.transform, true, Audio);
		Instantiate(muzzleEffect, val.barrelPoint.position, val.barrelPoint.rotation);
		//----------------------------------------
	}

	void RemoveLine(){
		if (myLine == null)
			return;
		myLine.enabled = false;
	}
}
