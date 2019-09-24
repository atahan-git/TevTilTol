﻿using UnityEngine;
using System.Collections;

public class ObjectivesBase : MonoBehaviour {

	public Objective[] objectives;

	public GameObject GUIPrefab; //these must include a ObjectiveGUI script
	public RectTransform parent; //make sure this guy has layout group

	[System.Serializable]
	public class MyEventType : UnityEngine.Events.UnityEvent {}
	public MyEventType callWhenAllIsDone;

	//reference
	/*public enum conditionType{
		ifTrue,
		ifFalse,
		ifAll,
		ifAny,
		ifLessThan,
		ifMoreThan,
		ifEqual
	}*/

	void Start (){

		/*foreach (Objective myObjective in objectives) {
			GameObject myGUI = (GameObject)Instantiate (GUIPrefab, transform.position, transform.rotation);
			myGUI.GetComponent<RectTransform>().SetParent(parent);
			myGUI.transform.localPosition = Vector3.zero;
			myGUI.transform.localRotation = Quaternion.identity;
			myGUI.transform.localScale = new Vector3 (1, 1, 1);
			myGUI.GetComponent<ObjectiveGUI> ().myObjective = myObjective;
		}*/

	}

	public void InitiateObjective (int id) {

		GameObject myGUI;
		
		if (objectives [id].GUI != null) {
			myGUI = (GameObject)Instantiate (objectives [id].GUI, transform.position, transform.rotation);
		} else {
			myGUI = (GameObject)Instantiate (GUIPrefab, transform.position, transform.rotation);
		}
		myGUI.GetComponent<RectTransform>().SetParent(parent);
		myGUI.transform.localPosition = Vector3.zero;
		myGUI.transform.localRotation = Quaternion.identity;
		myGUI.transform.localScale = new Vector3 (1, 1, 1);
		myGUI.GetComponent<ObjectiveGUI> ().myObjective = objectives [id];
		objectives [id].isEnabled = true;
		objectives [id].checkerI.isActive = true;
	}

	void Update (){

		//check if we have achieved the objective or not
		//if yes then call events
		foreach (Objective myObjective in objectives) {
			
			float value = myObjective.checkerI.curValue;
			float requiredValue = myObjective.requiredValue;

			switch (myObjective.type) {

			case Objective.conditionType.ifTrue:
				if (value == 1) {
					if (myObjective.onlyTriggerOnce) {
						if (!myObjective.isTriggeredOnce) {
							myObjective.isTriggeredOnce = true;
							myObjective.checkerI.isActive = false;
							CallEvent (myObjective);
						}
					} else {
						CallEvent (myObjective);
					}
					myObjective.isDone = true;
				} else
					myObjective.isDone = false;
				break;

			case Objective.conditionType.ifFalse:
				if (value == 0){
					if (myObjective.onlyTriggerOnce) {
						if (!myObjective.isTriggeredOnce) {
							myObjective.isTriggeredOnce = true;
							myObjective.checkerI.isActive = false;
							CallEvent (myObjective);
						}
					} else {
						CallEvent (myObjective);
					}
					myObjective.isDone = true;
				} else
					myObjective.isDone = false;
				break;

			case Objective.conditionType.ifAll:
				if (value == requiredValue) {
					if (myObjective.onlyTriggerOnce) {
						if (!myObjective.isTriggeredOnce) {
							myObjective.isTriggeredOnce = true;
							myObjective.checkerI.isActive = false;
							CallEvent (myObjective);
						}
					} else {
						CallEvent (myObjective);
					}
					myObjective.isDone = true;
				} else
					myObjective.isDone = false;
				break;

			case Objective.conditionType.ifAny:
				if (value > 0) {
					if (myObjective.onlyTriggerOnce) {
						if (!myObjective.isTriggeredOnce) {
							myObjective.isTriggeredOnce = true;
							myObjective.checkerI.isActive = false;
							CallEvent (myObjective);
						}
					} else {
						CallEvent (myObjective);
					}
					myObjective.isDone = true;
				} else
					myObjective.isDone = false;
				break;

			case Objective.conditionType.ifLessThan:
				if (value < requiredValue){
					if (myObjective.onlyTriggerOnce) {
						if (!myObjective.isTriggeredOnce) {
							myObjective.isTriggeredOnce = true;
							myObjective.checkerI.isActive = false;
							CallEvent (myObjective);
						}
					} else {
						CallEvent (myObjective);
					}
					myObjective.isDone = true;
				} else
					myObjective.isDone = false;
				break;

			case Objective.conditionType.ifMoreThan:
				if (value > requiredValue) {
					if (myObjective.onlyTriggerOnce) {
						if (!myObjective.isTriggeredOnce) {
							myObjective.isTriggeredOnce = true;
							myObjective.checkerI.isActive = false;
							CallEvent (myObjective);
						}
					} else {
						CallEvent (myObjective);
					}
					myObjective.isDone = true;
				} else
					myObjective.isDone = false;
				break;

			case Objective.conditionType.ifEqual:
				if (value == requiredValue) {
					if (myObjective.onlyTriggerOnce) {
						if (!myObjective.isTriggeredOnce) {
							myObjective.isTriggeredOnce = true;
							myObjective.checkerI.isActive = false;
							CallEvent (myObjective);
						}
					} else {
						CallEvent (myObjective);
					}
					myObjective.isDone = true;
				} else
					myObjective.isDone = false;
				break;

			}
		}

		if (IsAllDone ())
			callWhenAllIsDone.Invoke ();

	}

	void CallEvent (Objective myObjective){
		myObjective.callWhenDone.Invoke ();
	}

	bool IsAllDone (){
		foreach (Objective myObjective in objectives) {
			if (!myObjective.isDone)
				return false;
		}
		return true;
	}
}