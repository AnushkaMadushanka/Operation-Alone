using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RandomWalkingScript : MonoBehaviour {

	private NavMeshAgent agent;
	private Animator anim;
	private MazeLoader ml;
	private Vector3 position;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		ml = FindObjectOfType<MazeLoader> ();
		anim.SetBool ("Run", Random.Range(0,2) == 1 ? true : false);
		anim.SetBool("Attack", false);
		position = GameObject.Find ("Floor " + Random.Range (0, ml.mazeColumns) + "," + Random.Range (0, ml.mazeRows)).transform.position;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (agent.remainingDistance > 0 && agent.remainingDistance < 2f) {
			position = GameObject.Find ("Floor " + Random.Range (0, ml.mazeColumns) + "," + Random.Range (0, ml.mazeRows)).transform.position;
		}
		agent.SetDestination (position);
	}
}
