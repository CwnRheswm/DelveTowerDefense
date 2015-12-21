using UnityEngine;
using System.Collections;

public class Abilities : MonoBehaviour {
	//includes Attacks, Defenses, Supports & Mining
	//they are differentiated by projectile and "additional effect"
	//defense projectiles with have a script that adds a shield or something

	public AbilityBook[] abilityBook;
	public float globalCooldownTimer = 0.5f;
	private float globalCooldown = 0.5f;
	private float castTime;
	private AbilityBook casting;
	private int team;
	public targetAIs targetAI = targetAIs.Closest;

	private Body body;
	//private GameHandler handler;
	//private CommonClasses common;
	private Missile missile;
	private LayerProjectileScript projLayer;

	// Use this for initialization
	void Start () {
		body = transform.GetComponent<Body>();
		//handler = GameHandler.handler;
		//common = CommonClasses.common;
		team = body.team;
		foreach(AbilityBook ability in abilityBook){
			ability.projectile.GetComponent<Missile>().tower = this.gameObject.GetComponent<Tower>();
		}
		Debug.Log("Starting Abilites "+gameObject.name);
		globalCooldownTimer = 0.5f;
	}
	AbilityBook Cast(AbilityBook ability){
		/*Instantiates the passed ability projectile
		pass it the target, rutrns the cooldown time*/
		foreach(GameObject enemy in ability.target){
			GameObject shot = Graveyard.graveyard.CheckTombstone(ability.projectile.name);
			//Debug.Log(shot+" : "+ability.projectile.name);
			if (shot != null){
				shot.GetComponent<Missile>().Recycle(transform.position);
			} else {
				shot = (GameObject)Instantiate (ability.projectile, transform.position, ability.projectile.transform.rotation);
			}
			shot.transform.parent = LayerProjectileScript.projLayer.transform;
			Missile shotMissile = shot.GetComponent<Missile>();
			shotMissile.target = enemy;
			Debug.Log("Missile target: "+shotMissile.target);
			//add ability modifiers
			shotMissile.damage += ability.damagePlus;
			//Debug.Log(enemy+" : "+shotMissile.target);
			shot.SetActive(true);
		}
		ability.target = new GameObject[ability.maxTargets];
		OnCooldown(globalCooldown);
		//Debug.Log(ability.projectile);
		ability.cooldown = ability.projectile.GetComponent<Missile>().cooldown + ability.cooldownPlus;
		return null;

	}
	void GameUpdate(){
		if(globalCooldownTimer > 0){
				globalCooldownTimer -= GameHandler.handler.GetDelta();
		} 
		if (globalCooldownTimer < 0){
			globalCooldownTimer = 0;
		}
		foreach(AbilityBook ability in abilityBook){
			if(ability.cooldown > 0){
				ability.cooldown -= GameHandler.handler.GetDelta();
			}
			if(ability.cooldown < 0){
				ability.cooldown = 0;
			}
		}
		if (casting == null){
			//check for ready abilities, find targets, cast()
			if (globalCooldownTimer <= 0){
				//Debug.Log(gameObject.name+": "+globalCooldownTimer);
				foreach(AbilityBook ability in abilityBook){
					if (ability.cooldown <= 0 && ability.isActive){
						ability.target = CommonClasses.common.FindTargets(gameObject, ability.target, transform.position, ability.range + ability.rangePlus, team, ability.projectile.GetComponent<Missile>().targetAI);
						if(ability.target[0] != null){
							casting = ability;
							if(ability.castTime > 0){
								castTime = ability.castTime + ability.castTimePlus;
							}
						}
					}
				}
			}
		} else {
			if (castTime > 0){
				castTime -= GameHandler.handler.GetDelta();
			} else {
				casting = Cast(casting);
			}
		}
	}
	void OnValidate(){
		if(abilityBook.Length > 0){
			foreach(AbilityBook ability in abilityBook){
				Missile proj = ability.projectile.transform.GetComponent<Missile>();
				if (!proj){
					ability.projectile = null;
				} else {
					//proj.abilityBook = this;
					ability.range = proj.range;
					ability.maxTargets = proj.maxTargets + ability.maxTargetsPlus;
					ability.target = new GameObject[ability.maxTargets];
					
					ability.castTime = proj.castTime;
				}
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(!GameHandler.handler.IsPaused()){
			GameUpdate();
		}
	}
	/* Projectiles decide their own targets, easier to allow for a defensive ability that targets itself
	GameObject GetTarget(){
		//AI decisions for getting target?
		return this.gameObject;

	}
	*/
	void OnCooldown(float time){
		globalCooldownTimer = time;
	}

}


