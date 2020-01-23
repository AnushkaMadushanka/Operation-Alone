using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	private Transform player;
	public float smooth = 5f;
	public float height = 5f;

	private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3 ();
		pos.x = player.position.x;
		pos.z = player.position.z - 7f;
		pos.y = player.position.y + height;
		transform.position = Vector3.SmoothDamp (transform.position, pos,ref velocity, smooth);
	}
}
