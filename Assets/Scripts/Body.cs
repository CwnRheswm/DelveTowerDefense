using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {

	/*
	Used to hold all the basic attributes for a unit, 
	Tower.cs holds tower specifics, e.g. Mining
	Unit.cs holds unit specifics, e.g. link to Walk
	*/

	//find statics
	//private Graveyard graveyard;
	//private GameHandler handler;


	public string title; //for unique units

	//stats
	public float movementSpeed;
	public int maxHealth;
	public int curHealth;

	//cost for spawing, building, etc. if applicable
	public float[] cost = new float[4];

	//armor defense stats
	public Defenses defense;
	public float defenseRating;

	//team attribute is used to have potential 2+ teams on map
	public int team = 1; //1 is basic enemy, 0 is friend

	//animation tracker to determine when abilities/attacks can be called

	private Abilities ability; //used for unit abilities, mostly attacking, some defense/support

	// Use this for initialization
	void Start () {
		//graveyard = Graveyard.graveyard;
		ability = GetComponent<Abilities>();
		//handler = GameHandler.handler;
	}
	
	void GameUpdate() {
			//check for an object being target and attack if applicable
			//ability.use();
		
	}
	// Update is called once per frame
	void Update () {
		if(!GameHandler.handler.IsPaused()){
			GameUpdate();
		}
	}
	float AbsorbDamage(DamageTypes damageType){
		
		switch(defense)
		{
			case Defenses.armored:
				switch(damageType){
					case DamageTypes.chemical:
						break;
					case DamageTypes.normal:
						break;
					case DamageTypes.plasma:
						break;
					case DamageTypes.sound:
						break;
				}
				break;
			case Defenses.ethereal:
				switch(damageType){
					case DamageTypes.chemical:
						break;
					case DamageTypes.normal:
						break;
					case DamageTypes.plasma:
						break;
					case DamageTypes.sound:
						break;
				}
				break;
			case Defenses.normal:
				switch(damageType){
					case DamageTypes.chemical:
						break;
					case DamageTypes.normal:
						break;
					case DamageTypes.plasma:
						break;
					case DamageTypes.sound:
						break;
				}
				break;
		}
		return 1f;
	}
	public void ApplyDamage(float damage, DamageTypes damageType, float accuracy){	
		if(damage > 0){
			//called by a projectile when hitting a target
			float damageModifier = AbsorbDamage(damageType);
			Debug.Log(gameObject.name+" took ("+ damage +" * "+damageModifier+" - "+defenseRating+") = "+((damage*damageModifier)*defenseRating)+" Damage.");
			curHealth -= (int)((damage * damageModifier) - defenseRating);
			Debug.Log("("+damage+" "+damageModifier+" "+defenseRating+")"+" : "+curHealth);
		}
	}
}
