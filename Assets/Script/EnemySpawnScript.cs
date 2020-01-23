using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

	public GameObject[] EnemyPrefabs;
	public float spawnTime;

	private GameObject player;
	private float currentTime;
	private MazeLoader ml;

	void Start(){
		ml = FindObjectOfType<MazeLoader> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
		if (currentTime < 0) {
			var distance = 0f;
			Vector3 position = Vector3.zero;
			while (distance < 18f) {
				position = GameObject.Find ("Floor " + Random.Range (0, ml.mazeColumns) + "," + Random.Range (0, ml.mazeRows)).transform.position;
				distance = Vector3.Distance (position, player.transform.position);
			}
			Instantiate (EnemyPrefabs[Random.Range(0,EnemyPrefabs.Length)],  position, Quaternion.identity);
			currentTime = spawnTime;
		} else {
			currentTime -= Time.deltaTime;
		}
	}
}
