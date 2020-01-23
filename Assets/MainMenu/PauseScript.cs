using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
	bool pause;
	public GameObject PauseCanvas;
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			pause = !pause;
			PauseActivate ();
		}
	}

	void PauseActivate(){
		Time.timeScale = pause ? 0f : 1f;
		PauseCanvas.SetActive (pause);
		Cursor.visible = pause;
	}

	public void Resume(){
		pause = false;
		PauseActivate ();
	}
}
