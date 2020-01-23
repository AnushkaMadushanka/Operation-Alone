using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThankYouScript : MonoBehaviour {

	// Use this for initialization
	public void BackToMainMenu () {
		PlayerPrefs.SetInt ("CurrentLevel", 1);
		SceneManager.LoadScene ("Main Menu");
	}

}
