using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum bitTypes{
	NONE,
	mattock,
	compress,
	oscillate,
	alembic,
	engine,
	silvered,
	commonAdd,
	rare1Add,
	rare2Add
}

public enum UpgradeType{
	AttackCooldown,
	AttackRange,
	AttackDamage,
	AttackType,
	MiningCooldown,
	MiningRange,
	DefenseRating,
	Defenses,
	DefenseReflect
}

public enum OreType{
	iron,
	lodestone,
	brinmistr,
	mithril
}

public class CircleMenu{

}

public enum Movements{
	small,
	large,
	seige,
	flying
}
public enum Defenses{
	normal,
	armored,
	ethereal
}
public enum AttackTypes
{
	normal,
	sound,
	chemical,
	plasma,
	health
}
public enum DamageTypes
{
	normal,
	sound,
	chemical,
	plasma,
	health
}
public enum targetAIs{
	Closest,
	Farthest,
	Healthiest,
	Weakest,
	Self,
	All
}
public enum PrefabTypes{
	Unit,
	Tower
}
public enum UnitTypes {
	Kobold,
	Troll
}
[Serializable]
public class AbilityBook{
	public bool isActive;
	public bool foldout;
	public float cooldown = 0.5f;
	public float castTime;
	public int maxTargets;
	public float range;
	public GameObject[] target;
	public GameObject projectile; //should check for Missile.cs

	public float cooldownPlus;

	public float castTimePlus;

	public int maxTargetsPlus;

	public float rangePlus;

	//projectile additives
	public float damagePlus;

	public float sDamagePlus;

	public float sRangePlus;

}
//HEXES
[Serializable]
public class HexGroup
{
	public HexTypes[] hexes = new HexTypes[0];
}
[Serializable]
public class HexTypes
{
	public Material mat;
	public Texture btn;
	public float defaultRotation;
	private bool moveFlag = false;
}
[Serializable]
class HexGraph
{
	public HexRow[] hexRow = new HexRow[1];
}
[Serializable]
class HexRow
{
	public HexData[] hexData = new HexData[1];
}
[Serializable]
public class HexData
{
	public GameObject tile;
	public Vector3 position;
	public Quaternion rotation;
	public Material mat;
	public int matIndex;
	public int oreFlag;
	//extra tags and ore/no ore
}
[Serializable]
class HexSave
{
	//[SerializeField] public Vector3 position;
	public float posX;
	public float posY;
	public float posZ;
	//public Quaternion rotation;
	//public float rotX;
	//public float rotY;
	//public float rotZ;
	public int matIndex;
	public int oreFlag;
}

public class CommonClasses : MonoBehaviour {
	public static CommonClasses common;
	void Awake(){
		if(common == null){
			DontDestroyOnLoad(gameObject);
			common = this;
		} else {
			Destroy(gameObject);
		}
	}
	//Force repath of all walking objects
	public void MassRepath(){
		Walk[] walkers = FindObjectsOfType(typeof(Walk)) as Walk[];
		for (int w = 0; w < walkers.Length; w++){
			walkers[w].Repath();
		}
	}
	//Basic FindTargets with no AI, just grabs a random selection
	public GameObject[] FindTargets(GameObject attacker, GameObject[] targetArray, Vector3 position, float range, int team, targetAIs targetAI){ //finds an appropriate target
		//fills and returns the passed array
		//needs to grab all GOs within the range, 
		//filter out non-enemies, than choose from that to fill array
		List<CollisionList> targetList = new List<CollisionList>();
		if(targetAI == targetAIs.Self){
			targetArray = new GameObject[1];
			targetArray[0] = attacker;
			Debug.Log(targetArray[0]+" : "+attacker);
			return targetArray;
		}
		Collider[] hits = Physics.OverlapSphere(position, range);

		if(hits.Length <= 0){
			return targetArray;
		}

		if (hits.Length > 0){
			for (int i = 0; i < hits.Length; i++){
				var targetBody = hits[i].GetComponent<Body>();
				if(targetBody != null){
					if(targetBody.team != team && targetBody.curHealth > 0){
							float dist = Vector3.Distance(position, hits[i].transform.position);
							CollisionList c = new CollisionList(i, targetBody.curHealth,dist);
							targetList.Add(c);
					}
				}
			}
			switch(targetAI){
				case targetAIs.Weakest:
					targetList = targetList.OrderBy(x => x.health).ToList();
					break;
				case targetAIs.Healthiest:
					targetList = targetList.OrderBy(x => -(x.health)).ToList();
					break;
				case targetAIs.Farthest:
					targetList = targetList.OrderBy(x => -(x.distance)).ToList();
					break;
				case targetAIs.Closest:
					targetList = targetList.OrderBy(x => x.distance).ToList();
					break;
				case targetAIs.All:
					targetList = targetList;
					break;
			}
			int targetfill = 0;
			foreach(CollisionList collision in targetList){
				if (targetfill < targetArray.Length){
					targetArray[targetfill] = hits[collision.hitIndex].gameObject;
					targetfill++;
				} else {
					return targetArray;
				}
			}
		}
		return targetArray;
	}
	public class CollisionList{
		public int hitIndex;
		public int health;
		public float distance;
		public CollisionList(int hI, int h, float d){
			hitIndex = hI;
			health = h;
			distance = d;
		}
	}	
}