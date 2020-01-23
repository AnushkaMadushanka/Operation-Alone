using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour {
	private GameObject player;

	public Image fullHealthPic;
	public Image curHealthPic;
	private float fullHealth;
	private float currhealth;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		fullHealth = FindObjectOfType<MazeLoader>().spawnCount;
		SetHealthImages ();
	}
	
	// Update is called once per frame
	void Update () {
		var distance = Vector3.Distance (transform.position, player.transform.position);

		if(distance < 3.5f && currhealth == fullHealth){
			PlayerPrefs.SetInt ("CurrentLevel", PlayerPrefs.GetInt ("CurrentLevel") + 1);
			SceneManager.LoadScene ("Loading Scene");
			Destroy (this);
		}
		SetHealthImages ();
	}

	public void AddToCompletion(){
		currhealth++;
		if (currhealth > fullHealth)
			currhealth = fullHealth;
	}

	void SetHealthImages(){
		if (curHealthPic != null && fullHealthPic != null) 
			curHealthPic.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, currhealth / fullHealth * fullHealthPic.rectTransform.sizeDelta.x);
	}
}
