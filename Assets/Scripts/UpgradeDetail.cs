using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum upgrTypes{
	Attack_Speed,
	Attack_Range,
	Attack_Damage,
	Mining_Speed,
	Mining_Range,
	Mining_Efficiency_All,
	Mining_Efficiency_Iron,
	Mining_Efficiency_Lodestone,
	Mining_Efficiency_Brinmistr,
	Mining_Efficiency_Mithril,
	Defense_Rating,
	Damage_Reflect
}

public enum operTypes{
	add,
	mul
}


[System.Serializable]

public class UpgradeDetails{
	
	public int level;
	public string name;
	public upgrTypes upgrade;
	
	public operTypes operation;
	
	public float value = 0;
	
	public bool changeAtk;
	public AttackTypes atkType;
	public bool changeDef;
	//public DefenseType defType;
	public bool locked;
	
	//public List<UpgradeBit> gears = new List<UpgradeBit> ();
	public SpriteRenderer sprite;
}


[System.Serializable]

public class UpgradeBits{

	public bitTypes bitType;
	//public bitTypes gearA;
	//public bitTypes gearB;
	[SerializeField] public List<UpgradeDetails> numbers = new List<UpgradeDetails>();
}
