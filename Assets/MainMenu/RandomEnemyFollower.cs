using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyFollower : MonoBehaviour {
	private Transform player;
	public float smooth = 5f;
	public float height = 5f;
	private Vector3 pos;

	private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
		Time.timeScale = 1f;
		StartCoroutine (TakeAnotherFollower ());
	}

	// Update is called once per frame
	void Update () {
		pos = new Vector3 ();
		pos.x = player.position.x;
		pos.z = player.position.z - 7f;
		pos.y = player.position.y + height;
		transform.position = Vector3.SmoothDamp (transform.position, pos,ref velocity, smooth);
	}

	IEnumerator TakeAnotherFollower(){
		var enemies = FindObjectsOfType<RandomWalkingScript> ();
		player = enemies [Random.Range (0, enemies.Length)].transform;
		yield return new WaitForSeconds (10f);
		StartCoroutine (TakeAnotherFollower ());

	}
}
