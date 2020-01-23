using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {
	private GameObject target;
	private NavMeshAgent agent;
	public GameObject healthCanvas;
	public float damage = 10f;
	private HealthScript hs;
	private Animator anim;
	private PlayerScript ps;
	private bool dead;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag ("Player");
		agent = GetComponent<NavMeshAgent> ();
		hs = target.GetComponent<HealthScript> ();
		anim = GetComponent<Animator> ();
		ps = target.GetComponent<PlayerScript> ();
		anim.SetBool ("Run", Random.Range(0,2) == 1 ? true : false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!dead) {
			agent.SetDestination (target.transform.position);
			if (agent.remainingDistance > 0 && agent.remainingDistance < 2f) {
				anim.SetBool ("Attack", true);
			} else {
				anim.SetBool("Attack", false);
			}
			healthCanvas.transform.eulerAngles = new Vector3 (0f, 0f, 0f);
		}

		if (ps.dead) {
			gameObject.AddComponent<RandomWalkingScript> ();
			GameObject.Destroy (healthCanvas);
			Destroy (this);
		}
	}

	public void Dead(){
		GameObject.Destroy (healthCanvas);
		Destroy (GetComponent<Collider>());
		if(!dead)
			anim.SetTrigger("Dead");
		dead = true;
	}

	public void DestroyBody(){
		GameObject.Destroy (gameObject);
	}


	public void AddDamage(){
		hs.AddDamage (damage);
	}
}
