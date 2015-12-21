using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
/*
public enum PrefabTypes
{
	Unit,
	Tower
}
public enum UnitTypes
{
	Kobold,
	Troll
}*/

public class AddNewUnit : EditorWindow {
	
	public PrefabTypes prefabType = PrefabTypes.Tower;
	public UnitTypes unitType = UnitTypes.Kobold;
	public bool isEdit = false;
	
	//public Unit newUnit;
	
	public string newTitle;
	public string newName;
	public bool isUnique;
	public int newHealth;
	public float newSpeed;
	public Defenses newDefenseType;
	public float newDefenseRating;
	public Sprite newSprite;
	public Animator newAnimator;

	public bool atk1IsActive = true;
	public GameObject newAttack1Projectile;
	public float atk1Range;
	public float atk1Damage;
	public AttackTypes atk1Type = AttackTypes.normal;
	public float atk1CD;
	public float atk1Accuracy = 0.85f;
	public float atk1Speed = 10f;

	public bool hasSecondAtk;

	public bool atk2IsActive = false;
	public GameObject newAttack2Projectile;
	public float atk2Range;
	public float atk2Damage;
	public AttackTypes atk2Type = AttackTypes.normal;
	public float atk2CD;
	public float atk2Accuracy = 0.85f;
	public float atk2Speed = 10f;
	public bool atk2OnGC;

	public bool canMine;
	public bool mineIsActive = false;
	public float mineRange;
	public float mineCD;
	public float mineEffIron;
	public float mineEffLode;
	public float mineEffBrin;
	public float mineEffMith;

	public bool isUnit;
	public string type;

	static void Init()
	{
		
		AddNewUnit window = (AddNewUnit)EditorWindow.CreateInstance (typeof(AddNewUnit));
		window.Show ();
	}

	void OnGUI()
	{
		//Use PreExisting Prefab
		/*
		if(isEdit)
		{
			newTitle = newUnit.title;
			newName = newUnit.title;
			//
			newHealth = newUnit.health;
			newSpeed = newUnit.speed;
			newDefenseType = newUnit.defenseType;
			newDefenseRating = newUnit.defenseRating;
			newSprite = newUnit.sprite;
			newAnimator = newUnit.animator;
			isEdit = false;
		}
		*/

		prefabType = (PrefabTypes)EditorGUILayout.EnumPopup (prefabType);
		EditorGUILayout.Space ();

		switch (prefabType) 
		{
		case PrefabTypes.Tower:
			type = "Tower";
			isUnit = false;
			/*hasSecondAtk = true;
			atk2IsActive = false;
			canMine = true;
			mineIsActive = true;*/
			break;
		case PrefabTypes.Unit:
			EditorGUILayout.BeginHorizontal ();
			unitType = (UnitTypes)EditorGUILayout.EnumPopup (unitType);
			type = unitType.ToString ();
			EditorGUILayout.Space ();
			EditorGUILayout.EndHorizontal ();			
			EditorGUILayout.Space ();

			isUnit = true;
			/*hasSecondAtk = false;
			atk2IsActive = false;
			canMine = false;
			mineIsActive = false;*/
			break;
		}


		EditorGUILayout.BeginHorizontal ();
		EditorGUI.indentLevel = 0;
		EditorGUILayout.LabelField ("Title: ", GUILayout.Width (35));
		newTitle = EditorGUILayout.TextField (newTitle, GUILayout.Width (200), GUILayout.ExpandWidth(true));
		EditorGUILayout.LabelField ("Unique:", GUILayout.Width (50), GUILayout.ExpandWidth(false));
		isUnique = EditorGUILayout.Toggle(isUnique, GUILayout.Width (10), GUILayout.ExpandWidth(false));
		EditorGUILayout.Space ();
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.Space ();
		
		if (isUnique) 
		{		
			EditorGUILayout.BeginHorizontal ();
			EditorGUI.indentLevel = 1;
			EditorGUILayout.LabelField ("Name: ", GUILayout.Width (60));
			newName = EditorGUILayout.TextField (newName,GUILayout.Width (200), GUILayout.ExpandWidth(true));
			EditorGUILayout.EndHorizontal ();
			EditorGUI.indentLevel = 0;
		}
		else
		{
			newName = newTitle;
		}
		
		EditorGUILayout.Space ();
		newSprite = (Sprite)EditorGUILayout.ObjectField ("Sprite: ", newSprite, typeof(Sprite), true);
		EditorGUILayout.Space ();
		
		//Animator???
		newAnimator = (Animator)EditorGUILayout.ObjectField ("Animator: ", newAnimator, typeof(Animator), true);
		EditorGUILayout.Space ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUI.indentLevel = 1;
		EditorGUILayout.LabelField ("Health: ", GUILayout.Width (80));
		newHealth = EditorGUILayout.IntField (newHealth);
		EditorGUILayout.Space ();
		if (isUnit)
		{
			EditorGUILayout.LabelField ("Speed:", GUILayout.Width (80));
			newSpeed = EditorGUILayout.FloatField (newSpeed);
			EditorGUILayout.Space ();
		}
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.Space ();
		
		EditorGUILayout.BeginHorizontal ();
		EditorGUI.indentLevel = 1;
		EditorGUILayout.LabelField ("Defense: ", GUILayout.Width (80));
		newDefenseType = (Defenses)EditorGUILayout.EnumPopup (newDefenseType, GUILayout.Width (100));
		//EditorGUILayout.Space ();
		EditorGUILayout.LabelField ("Rating:", GUILayout.Width (60));
		newDefenseRating = EditorGUILayout.FloatField (newDefenseRating, GUILayout.Width (40));
		EditorGUILayout.Space ();
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();
		
		EditorGUILayout.BeginHorizontal ();
		EditorGUI.indentLevel = 0;
		EditorGUILayout.LabelField ("Attack :", GUILayout.Width (150));
		EditorGUILayout.Space ();
		atk1IsActive = EditorGUILayout.Toggle (atk1IsActive, GUILayout.Width (30));
		EditorGUILayout.EndHorizontal ();
		
		EditorGUILayout.BeginHorizontal ();
		EditorGUI.indentLevel = 1;
		EditorGUILayout.LabelField ("Object: ", GUILayout.Width (80));
		newAttack1Projectile = (GameObject)EditorGUILayout.ObjectField (newAttack1Projectile, typeof(GameObject), true);//, GUILayout.Width(100));
		EditorGUILayout.LabelField ("Damage: ", GUILayout.Width(80));
		atk1Damage = EditorGUILayout.FloatField (atk1Damage);
		EditorGUILayout.EndHorizontal ();

		atk1Type = (AttackTypes)EditorGUILayout.EnumPopup("Type: ", atk1Type);

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Range: ", GUILayout.Width (80));
		atk1Range = EditorGUILayout.FloatField (atk1Range);
		EditorGUILayout.LabelField ("CoolDown: ",GUILayout.Width (80));
		atk1CD = EditorGUILayout.FloatField (atk1CD);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Accuracy: ", GUILayout.Width (80));
		atk1Accuracy = EditorGUILayout.FloatField (atk1Accuracy);
		EditorGUILayout.LabelField ("Speed: ", GUILayout.Width (80));
		atk1Speed = EditorGUILayout.FloatField (atk1Speed);
		EditorGUILayout.EndHorizontal ();

		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.Space ();
		hasSecondAtk = EditorGUILayout.Toggle ("Has Second Attack: ", hasSecondAtk);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();
		EditorGUILayout.Space ();

		if(hasSecondAtk)
		{
			EditorGUILayout.BeginHorizontal ();
			EditorGUI.indentLevel = 0;
			EditorGUILayout.LabelField ("Attack 2 :", GUILayout.Width (150));
			EditorGUILayout.Space ();
			atk2IsActive = EditorGUILayout.Toggle (atk2IsActive, GUILayout.Width (30));
			EditorGUILayout.EndHorizontal ();
			
			EditorGUILayout.BeginHorizontal ();
			EditorGUI.indentLevel = 1;
			EditorGUILayout.LabelField ("Object: ", GUILayout.Width (80));
			newAttack2Projectile = (GameObject)EditorGUILayout.ObjectField (newAttack1Projectile, typeof(GameObject), true);
			EditorGUILayout.LabelField ("Damage: ", GUILayout.Width(80));
			atk2Damage = EditorGUILayout.FloatField (atk1Damage);
			EditorGUILayout.EndHorizontal ();

			atk2Type = (AttackTypes)EditorGUILayout.EnumPopup ("Type: ", atk2Type);

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Range: ", GUILayout.Width(80));
			atk2Range = EditorGUILayout.FloatField (atk2Range);
			EditorGUILayout.LabelField ("CoolDown: ", GUILayout.Width (80));
			atk2CD = EditorGUILayout.FloatField (atk2CD);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Accuracy: ", GUILayout.Width (80));
			atk2Accuracy = EditorGUILayout.FloatField (atk2Accuracy);
			EditorGUILayout.LabelField ("Speed: ", GUILayout.Width (80));
			atk2Speed = EditorGUILayout.FloatField (atk2Speed);
			EditorGUILayout.EndHorizontal ();

			atk2OnGC = EditorGUILayout.Toggle ("On GC: ", atk2OnGC);
		}

		EditorGUI.indentLevel = 1;
		EditorGUILayout.Space ();
		EditorGUILayout.BeginHorizontal ();
		EditorGUILayout.Space ();
		canMine = EditorGUILayout.Toggle ("Can Mine: ", canMine);
		EditorGUILayout.EndHorizontal ();
		EditorGUILayout.Space ();

		if(canMine)
		{
			///Needs to Include:
			/// Mining Range, Mining Cooldown, Mining Efficiency x4
			///
			EditorGUILayout.BeginHorizontal ();
			EditorGUI.indentLevel= 0;
			EditorGUILayout.LabelField ("Mining: ", GUILayout.Width (150));
			EditorGUILayout.Space ();
			mineIsActive = EditorGUILayout.Toggle (mineIsActive, GUILayout.Width (30));
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUI.indentLevel = 1;
			EditorGUILayout.LabelField ("Range: ", GUILayout.Width (80));
			mineRange = EditorGUILayout.FloatField (mineRange);
			EditorGUILayout.LabelField ("CoolDown: ", GUILayout.Width (80));
			mineCD = EditorGUILayout.FloatField (mineCD);
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUI.indentLevel = 2;
			EditorGUILayout.LabelField ("Iron:    ", GUILayout.Width (80));
			mineEffIron = EditorGUILayout.FloatField (mineEffIron);
			EditorGUILayout.LabelField ("Lode:    ", GUILayout.Width (80));
			mineEffLode = EditorGUILayout.FloatField (mineEffLode);
			EditorGUILayout.Space ();
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Brin:    ", GUILayout.Width (80));
			mineEffBrin = EditorGUILayout.FloatField (mineEffBrin);
			EditorGUILayout.LabelField ("Mithril: ", GUILayout.Width (80));
			mineEffMith = EditorGUILayout.FloatField (mineEffMith);
			EditorGUILayout.Space ();
			EditorGUILayout.EndHorizontal ();
		}

		EditorGUILayout.Space ();

		if(GUILayout.Button ("Register " + type))
		{
			//Build a New Prefab
			GameObject newUnit = (GameObject.Find ("UnitPrefab"));
			newUnit.name = newName;
			string folder = "Tower";
			if(isUnit)
			{
				folder = "Units";
			}
			string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/Prefabs/" + folder + "/" + newUnit.name + ".prefab");
			PrefabUtility.CreatePrefab (path, newUnit);

			//if(size == medium)
			var capsule = newUnit.GetComponent<CapsuleCollider>();

			capsule.radius = 1;
			capsule.height = 4;

			var body = newUnit.AddComponent<BodyScript>();

			body.title = newTitle;
			//body.speed = newSpeed;
			body.maxHealth = newHealth;
			body.health = newHealth;
			body.defenseRating = newDefenseRating;
			body.defenseType = newDefenseType.ToString ();

			if(isUnit)
			{
				//var seeker = newUnit.AddComponent<Seeker>();

				var controller = newUnit.AddComponent<CharacterController>();

				controller.radius = 0;
				controller.height = 0;

				var walker = newUnit.AddComponent<WalkScript>();
				walker.baseSpeed = newSpeed;

				var funnelMod = newUnit.AddComponent<Pathfinding.FunnelModifier>();
				//funnelMod.priority = 2;
				var alternMod = newUnit.AddComponent<Pathfinding.AlternativePath>(); 
				//alternMod.priority = 3;
			}

			var attack = newUnit.AddComponent<AttackScript>();

			attack.attackIsActive = atk1IsActive;
			attack.attackProjectile = newAttack1Projectile;
			attack.attackDamage = atk1Damage;
			attack.attackType = atk1Type;
			attack.attackRange = atk1Range;
			attack.attackCooldown = atk1CD;
			attack.attackAccuracy = atk1Accuracy;
			attack.attackSpeed = atk1Speed;

			if(hasSecondAtk)
			{
				attack.attack2IsActive = atk2IsActive;
				attack.attack2Projectile = newAttack2Projectile;
				attack.attack2Damage = atk2Damage;
				attack.attack2Type = atk2Type;
				attack.attack2Range = atk2Range;
				attack.attack2Cooldown = atk2CD;
				attack.attack2Accuracy = atk2Accuracy;
				attack.attack2Speed = atk2Speed;
				attack.attack2OnGC = atk2OnGC;
			}

			if(canMine)
			{
				var mine = newUnit.AddComponent<MiningScript>();

				mine.miningRange = mineRange;
				mine.miningCooldown = mineCD;
				mine.ironMiningRate = mineEffIron;
				mine.lodestoneMiningRate = mineEffLode;
				mine.brinmistrMiningRate = mineEffBrin;
				mine.mithrilMiningRate = mineEffMith;
			}
		}
	}
}