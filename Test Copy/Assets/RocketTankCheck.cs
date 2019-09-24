using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class RocketTankCheck : MonoBehaviour, IValue {

	public static bool leActive = false;

	public static UnityEvent glowCallON;
	public static UnityEvent glowCallOFF;

	public float curValue {
		get{
			return leValue;
		}
		set{
			if(isActive)
				leValue = value;
		}
	}

	public float leValue;

	public bool isActive {
		get {
			return _isActive;
		}
		set {
			_isActive = value;
			leActive = _isActive;
			if (_isActive)
				glowCallON.Invoke ();
			else
				glowCallOFF.Invoke ();
		}
	}
	bool _isActive = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		//curValue = leValue;

	}
}
