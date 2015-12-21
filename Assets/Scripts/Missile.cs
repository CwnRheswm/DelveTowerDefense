using UnityEngine;
using System.Collections.Generic;

public class Missile : MonoBehaviour {

	public DamageTypes damageType;
	public GameObject target;
	public int maxTargets;
	public float damage;
	public float castTime = 0f; 
	public float cooldown = 1f;
	public int team;
	public targetAIs targetAI;
	
	public float range;
	public float accuracy;
	public float speed; //movement speed of the projectile;

	public bool hasSplash = false;
	public GameObject[] sTargets;
	public float sDamage;
	public DamageTypes sDamageType;
	public float sRange;
	public float sAccuracy;
	public float sSpeed;
	public int maxSplashTargets;

	public GameObject persistentEffect;

	//private GameHandler handler;
	private CharacterController controller;
	private Graveyard graveyard;
	private string tombName;
	private Body body;
	public Tower tower;
	//public Abilities abilityBook;
	public CommonClasses common;
	public bool triggerGlobal = true;

	// Use this for initialization
	void OnEnable() {
	    //handler = GameHandler.handler;
	    Debug.Log(target);
	    controller = GetComponent<CharacterController>();
	    body = target.GetComponent<Body>();
	    common = CommonClasses.common;
	    if(targetAI == targetAIs.Self){
	    	target = tower.gameObject;
	    }
	    tombName = gameObject.name;
	    tombName = tombName.Remove(tombName.Length - 7);
	    Debug.Log("Starting "+tombName);
	}
	void Impact(GameObject iTarget, float iDamage, DamageTypes iDamageType, float iAccuracy){
		Body body = iTarget.GetComponent<Body>();
		body.ApplyDamage(iDamage, iDamageType, iAccuracy);
		if(persistentEffect != null){
			Transform dot = transform.GetChild(0);
			//var dot = (GameObject) Instantiate (persistentEffect, transform.position, transform.rotation);
			dot.parent = this.transform;
			dot.gameObject.SetActive(true);
		}
		Trash();
	}
	void SplashDamage(){
		sTargets = new GameObject[maxSplashTargets];
		sTargets = common.FindTargets(gameObject, sTargets, transform.position, sRange, team, targetAIs.All);
		foreach (GameObject enemy in sTargets){
			Impact(enemy, sDamage, sDamageType, sAccuracy);
			if(persistentEffect != null){
				var splash = (GameObject) Instantiate (persistentEffect, transform.position, transform.rotation);
				splash.transform.parent = this.transform;
			}
		}
	}
	void OnTriggerEnter(Collider c){
		//Debug.Log(gameObject+" collides with "+c);
		if (c.gameObject == target){
			//run Death animation
			//Destroy (gameObject); //for now, send to graveyard and disable
			if (hasSplash){
				SplashDamage();
			}
			Impact(target, damage, damageType, accuracy);
		}
	}
	void Trash(){
		
		transform.parent = Graveyard.graveyard.transform;
		transform.localPosition = new Vector3(0,0,0);

		target = null;

		//string tombName = gameObject.name;
		//tombName = tombName.Remove(tombName.Length - 7);//removes the ".prefab"
		Debug.Log(tombName);
		if(!Graveyard.graveyard.tombstones.ContainsKey(tombName)){
			Graveyard.graveyard.tombstones.Add(tombName, new List<GameObject>());
		}

		Graveyard.graveyard.tombstones[tombName].Add(gameObject);
		gameObject.SetActive(false);
	}

	public void Recycle(Vector3 casterPoint){
		transform.parent = LayerProjectileScript.projLayer.transform;
		transform.position = casterPoint;
		//Get target

		//activate projectile
		//gameObject.SetActive(true);
		//de-count from Graveyard ledger
		Graveyard.graveyard.tombstones[tombName].RemoveAt(0);
	}

	void GameUpdate(){
		//on collision with target
		if(body.curHealth > 0){
			Vector3 dir = (target.transform.position - transform.position).normalized;
			dir *= speed * GameHandler.handler.GetDelta();
			transform.position += dir;
		} else {
			Destroy(gameObject);
		}
		/*
		if (collide w/ target){
			Impact(target, damage, damageType, accuracy);
			if (splashDamage) {
				SplashDamage();
			}
		}
		*/
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(!GameHandler.handler.IsPaused()){
			GameUpdate();
		}
	}
}
