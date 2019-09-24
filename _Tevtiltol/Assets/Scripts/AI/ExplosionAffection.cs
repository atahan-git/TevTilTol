using UnityEngine;
using System.Collections;

[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof(Rigidbody))]
public class ExplosionAffection : MonoBehaviour {

	NavMeshAgent nav;
	Rigidbody rigid;

	bool isGrounded = true;

	// Use this for initialization
	void Start () {

		nav = GetComponent<NavMeshAgent> ();
		rigid = GetComponent<Rigidbody> ();
		//GetEffected ();
		//nav.updatePosition = false;
	}
	
	// Update is called once per frame
	void Update () {
		//print (nav.isOnNavMesh);
		if (isGrounded)
			nav.updatePosition = true;
	}

	public void OnChildTriggerStay (){

		isGrounded = true;

	}

	public void GetEffected (){
		//print ("got affeted");
		nav.updatePosition = false;
		rigid.isKinematic = false;
		isGrounded = false;

	}
}
