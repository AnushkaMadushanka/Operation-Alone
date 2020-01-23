using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public float movementSpeed = 5f;
	public float rotationSpeed = 5f;
	public GameObject playerObj;
	public Animator anim;
	public float animTransitionSpeed = 5f;
	private CharacterController cc;

	public GameObject marker;

	public GameObject bulletSpawnPoint;
	public float shotDamage = 10f;
	public float waitTime;
	public float reloadTime;
	public int bullets;
	private float currTime;
	private int currBullets;
	public LineRenderer trail;

	public GameObject minimap;

	public AudioSource footStepSound;
	public AudioClip shootingSound;

	public Text bulletText;

	public bool dead;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		currBullets = bullets;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead && Time.timeScale > 0f) {
			Plane playerPlane = new Plane (Vector3.up, transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float hitDist = 0.0f;

			if (playerPlane.Raycast (ray, out hitDist)) {
				Vector3 targetPoint = ray.GetPoint (hitDist);
				marker.transform.position = targetPoint;
				Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
				targetRotation.x = 0;
				targetRotation.z = 0;
				playerObj.transform.rotation = Quaternion.Slerp (playerObj.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			}

			var verticalInput = Input.GetAxis ("Vertical");
			var horizontalInput = Input.GetAxis ("Horizontal");
			cc.SimpleMove(new Vector3 (horizontalInput, 0f, verticalInput).normalized * movementSpeed);

			//Debug.Log (playerObj.transform.rotation.eulerAngles.y);
			var angleDeg = playerObj.transform.rotation.eulerAngles.y;
			if (verticalInput > 0 && horizontalInput < 0)
				angleDeg += 45;
			else if (verticalInput == 0 && horizontalInput < 0)
				angleDeg += 90;
			else if (verticalInput < 0 && horizontalInput < 0)
				angleDeg += 135;
			else if (verticalInput < 0 && horizontalInput == 0)
				angleDeg += 180;
			else if (verticalInput < 0 && horizontalInput > 0)
				angleDeg += 215;
			else if (verticalInput == 0 && horizontalInput > 0)
				angleDeg += 270;
			else if (verticalInput > 0 && horizontalInput > 0)
				angleDeg += 315;

			var angle =  angleDeg * Mathf.Deg2Rad;
			if (cc.velocity.magnitude > 0.2f) {
				anim.SetBool ("Walking", true);
				footStepSound.mute = false;
				anim.SetFloat ("Vertical", Mathf.Lerp(anim.GetFloat("Vertical"), Mathf.Cos (angle), animTransitionSpeed * Time.deltaTime) );
				anim.SetFloat ("Horizontal", Mathf.Lerp(anim.GetFloat("Horizontal"), -Mathf.Sin (angle), animTransitionSpeed * Time.deltaTime) );
			} else {
				footStepSound.mute = true;
				anim.SetBool ("Walking", false);
			}

			if (currTime <= 0 && currBullets > 0) {
				bulletText.text = "Bullets: " + currBullets + "/" + bullets;
				if (Input.GetMouseButton (0))
					StartCoroutine ("Shoot");
			} else if (currBullets == 0) {
				Reload ();
			} else {
				currTime -= Time.deltaTime;
			}

			if (Input.GetKey (KeyCode.M))
				minimap.SetActive (true);
			else
				minimap.SetActive (false);

			if (Input.GetKey (KeyCode.R))
				Reload ();
			
		}
	}

	IEnumerator Shoot (){
		AudioSource.PlayClipAtPoint (shootingSound, bulletSpawnPoint.transform.position);
		RaycastHit hit;
		Ray ray = new Ray(bulletSpawnPoint.transform.position,bulletSpawnPoint.transform.forward);
		trail.enabled = true;
		trail.SetPosition (0, bulletSpawnPoint.transform.position);
		if (Physics.Raycast (ray, out hit, 100f)) {
			Transform objectHit = hit.transform;
			var healthScript = hit.transform.GetComponent<HealthScript> ();
			if (healthScript != null)
				healthScript.AddDamage (shotDamage);
			trail.SetPosition (1, hit.point);
		} else {
			trail.SetPosition (1, bulletSpawnPoint.transform.forward * 100);
		}
		currTime = waitTime;
		currBullets--;
		bulletText.text = "Bullets: " + currBullets + "/" + bullets;
		yield return new WaitForSeconds (0.1f);
		trail.enabled = false;
	}

	public void Dead(){
		if(!dead)
			anim.SetTrigger("Dead");
		dead = true;
		foreach (var script in FindObjectsOfType<EnemySpawnPoint>()) {
			Destroy (script);
		}
		Destroy (FindObjectOfType<EnemySpawnScript> ());
	}

	void Reload(){
		currBullets = bullets;
		currTime = reloadTime;
		bulletText.text = "Reloading...";
	}
}
