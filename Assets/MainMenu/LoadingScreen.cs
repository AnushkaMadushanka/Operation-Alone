using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	public Slider loadingSlider;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
		loadingSlider.value = 0;
		StartCoroutine (LoadSceneAsync ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator LoadSceneAsync(){
		yield return new WaitForSeconds (1f);
		var currLevel = PlayerPrefs.GetInt ("CurrentLevel");
		var asyncLoad = SceneManager.LoadSceneAsync ("Level "+currLevel);
		while (!asyncLoad.isDone) {
			loadingSlider.value = asyncLoad.progress;
			Debug.Log (asyncLoad.progress);
			yield return null;
		}
	}
}
