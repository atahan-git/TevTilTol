using UnityEngine;
using System.Collections;

public class SpeechEventsController : MonoBehaviour {

	DialogTreeMaster dialog;

	V2_EnemySpawner spawner;

	int triggeredWave = 2;
	int lastDialog = 1;

	// Use this for initialization
	void Start () {
		dialog = GetComponent<DialogTreeMaster> ();
		dialog.StartDialog (0);

		spawner = GameObject.FindObjectOfType<V2_EnemySpawner> ();

	}

	bool isAllDone = false;

	public void DoneAllMissions (){
		isAllDone = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawner.curWave == triggeredWave) {
			if (triggeredWave < 5) {
				dialog.StartDialog (lastDialog);
			}

			if (spawner.curWave >= 10 && isAllDone) {
				dialog.StartDialog (10);
			}

			triggeredWave++;
			lastDialog++;
		}
	}
}
