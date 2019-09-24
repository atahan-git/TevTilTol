using UnityEngine;
using System.Collections;

public class GunPicker : MonoBehaviour {

	GameObject myCam;

	public float pickGunDistance = 4f;

	Animator anim;
	//GunController gcont;

	// Use this for initialization
	void Start () {
		if (myCam == null)
			myCam = Camera.main.gameObject;

		anim = GetComponent<Animator> ();
		//gcont = GetComponent<GunController> ();
	}

    // Update is called once per frame
    void Update()
    {
        //shoot a ray when key e pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(myCam.transform.position, myCam.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, pickGunDistance, 256))
            {
                GunBuilder myGunBuilder = hit.collider.gameObject.GetComponentInParent<GunBuilder>(); //get the gun builder

                if (!myGunBuilder)
                    return;

				if (PlayerPrefs.GetInt (4.ToString (), 0) == 0) {
					PlayerPrefs.SetInt (4.ToString (), 1);
					//HintScript.isPickedGun = true;
				}
                /*float distance = Vector3.Distance(myCam.transform.position, myGunBuilder.transform.position);
                //if it exists and close enough
                if (distance < pickGunDistance)
                {*/
                    GunBuilder myRealGunBuilder = GunController.myGunCont.gameObject.GetComponent<GunBuilder>(); //get our gun builder


                    GunBuilder.Gun temp = myRealGunBuilder.myGun;
                    myRealGunBuilder.myGun = myGunBuilder.myGun;
                    myGunBuilder.myGun = temp;


                    myRealGunBuilder.BuildGun();
                    myGunBuilder.BuildGun();
                    myGunBuilder.PickEffect();
                    anim.SetTrigger("isPicked");

                    //delete pickup
                    //Destroy(myGunBuilder.gameObject);
                //}
            }
        }

        Ray r2 = new Ray(myCam.transform.position, myCam.transform.forward);
        RaycastHit hit2;
        if (Physics.Raycast(r2, out hit2, pickGunDistance, 256))
        {
            GunBuilder myGunBuilder = hit2.collider.gameObject.GetComponentInParent<GunBuilder>(); //get the gun builder

            Debug.DrawLine(myCam.transform.position, hit2.point);
            //print(hit2.collider.gameObject);

            if (!myGunBuilder)
                return;

            /*AppearOnCanPickUp pick = myGunBuilder.GetComponent<AppearOnCanPickUp>();

            if (!pick)
                return;

            pick.CancelInvoke();
            pick.HighLight();*/
            

        }
    }
}
