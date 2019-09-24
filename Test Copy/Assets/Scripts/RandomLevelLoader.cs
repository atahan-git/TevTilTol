using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RandomLevelLoader : MonoBehaviour {

	public static RandomLevelLoader s;

	public int levelId = 2;
	public int menuId = 3;

	public GameObject myPlayer;

	void Awake () {
		s = this;
	}

	void Start (){
		Invoke ("betterStart", 0.2f);
	}

	void betterStart () {
		if (SceneManager.sceneCount > 1)
			return;
		//SceneManager.LoadSceneAsync(levelId, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync(menuId, LoadSceneMode.Additive);
	}

	// Use this for initialization
	public void LoadScene () {
		SceneManager.LoadSceneAsync(levelId, LoadSceneMode.Additive);
	}

	public void EngageExploration () {

		myPlayer.SetActive (true);

	}

	public void BackToMenu () {
		SceneManager.LoadScene(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
