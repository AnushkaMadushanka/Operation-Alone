using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

	public Image fullHealthPic;
	public Image curHealthPic;
	public float fullHealth = 100f;
	public ObjectTypes type;
	private float currhealth;
	// Use this for initialization
	void Start () {
		currhealth = fullHealth;
		AddDamage (0);
	}
	
	// Update is called once per frame
	void Update () {
		if (type == ObjectTypes.Player) {
			if (currhealth < fullHealth) {
				currhealth += 0.3f * Time.deltaTime;
			} else {
				currhealth = fullHealth;
			}
		}
		if (curHealthPic != null && fullHealthPic != null) 
			curHealthPic.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, currhealth / fullHealth * fullHealthPic.rectTransform.sizeDelta.x);
	}

	public void AddDamage(float damage){		
		currhealth -= damage;
		if (currhealth <= 0) {
			currhealth = 0;
			switch (type)
			{
			case ObjectTypes.Player:
				GetComponent<PlayerScript> ().Dead();
				break;
			case ObjectTypes.Enemy:
				GetComponent<EnemyScript> ().Dead();
				break;
			default:
				break;
			}
		}
	}
}

public enum ObjectTypes{
	Player,
	Enemy
}
