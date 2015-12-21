using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class AttackScript : MonoBehaviour {

	public bool attackIsActive;
	public GameObject attackProjectile;
	public float attackDamage;
	public AttackTypes attackType;  //normal, sound, chemical, plasma
	public float attackRange;
	public float attackCooldown;
	public float attackAccuracy;
	public float attackSpeed;

	public bool attack2IsActive;
	public GameObject attack2Projectile;
	public float attack2Damage;
	public AttackTypes attack2Type;
	public float attack2Range;
	public float attack2Cooldown;
	public float attack2Accuracy;
	public float attack2Speed;
	public bool attack2OnGC;

	/*public bool team;
	public GameObject target;
	List<GameObject> collisions = new List<GameObject>();*/

	private BodyScript body;

	// Use this for initialization
	public void Start () {
		body = transform.GetComponent<BodyScript> ();
		//var hammer = GameObject.Find ("1-2-0 Hammer");
		//if (hammer == null){
		//	hammer = (Resources.Load ("Prefabs/1-2-0 Hammer")) as GameObject;
		//}
		//attackProjectile = hammer;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	float max;
	GameObject target;

	public void Attack(){
		//collisions.Clear ();
		var hits = Physics.OverlapSphere (transform.position, attackRange);
		max = 0f;

		if (hits.Length > 0) {
			for (int i = 0; i < hits.Length; i++){
				var targetBody = hits[i].GetComponent<Body>();
				if (targetBody != null){
					if (targetBody.team != transform.gameObject.GetComponent<BodyScript>().team){
						//collisions.Add (hits[i].gameObject);
						if(targetBody.curHealth > max)
						{
							target = hits[i].gameObject;
							max = targetBody.curHealth;
						}
					}
				}
			}
		}

		if (target != null)
		{
			if (attackProjectile != null){

				GameObject projectile = Graveyard.graveyard.CheckTombstone(attackProjectile.name);
				Debug.Log(attackProjectile.name +" : "+projectile);
				if (projectile != null){
					projectile.GetComponent<Missile>().Recycle(transform.position);
				} else {
					//Instantiate projectile
					projectile = (GameObject)Instantiate (attackProjectile, transform.position, attackProjectile.transform.rotation);
				}
				ThrowScript ballistic = projectile.GetComponent<ThrowScript>();
				ballistic.target = target;
				ballistic.damage = attackDamage;
			}
			else
			{
				target.GetComponent<BodyScript>().ApplyDamage(attackDamage, attackType);
			}
			body.OnCooldown (attackCooldown);
			//body.Animate ("attack1");
		}
		target = null;
		hits = null;
	}

}
