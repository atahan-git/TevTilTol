using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelAtStart : MonoBehaviour {

	public int levelid = 1;

	// Use this for initialization
	void Start () {
        //Invoke("LoadNow", 6.5f);
		LoadNow();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadNow()
    {
        SceneManager.LoadSceneAsync(levelid);
    }
}
