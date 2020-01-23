using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnPoint : MonoBehaviour {

	private GameObject player;
	public float spawnDistance = 10f;
	private EnemySpawnScript ess;
	private float currentTime;
	private int currentZombieCount;

	public Image fullHealthPic;
	public Image curHealthPic;
	public float fullHealth = 100f;
	private float currhealth;

	void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		ess = transform.parent.GetComponent<EnemySpawnScript> ();
	}

	void Update(){
		var distance = Vector3.Distance (transform.position, player.transform.position);
		if (distance < spawnDistance) {
			if (currentTime < 0) {
				Instantiate (ess.EnemyPrefabs[Random.Range(0,ess.EnemyPrefabs.Length)], transform.position, Quaternion.identity);
				currentZombieCount++;
				currentTime = ess.spawnTime;
			} else {
				currentTime -= Time.deltaTime;
			}
		}

		if(distance < 3.5f && Input.GetKey(KeyCode.Space)){
			currhealth += 0.3f;
			if (currhealth > 100) {
				currhealth = 100;
				GameObject.FindObjectOfType<FinishScript> ().AddToCompletion ();
				Destroy (this);
			}
		}
		if (curHealthPic != null && fullHealthPic != null) 
			curHealthPic.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, currhealth / fullHealth * fullHealthPic.rectTransform.sizeDelta.x);
	}

}
