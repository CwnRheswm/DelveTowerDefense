using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



[System.Serializable]

public class UpgradeDetail{
	public UpgradeType upgradeType;
	public float val;
	public AttackTypes attack;
	public Defenses defense;
}
[System.Serializable]

public class UpgradeLevels{ //upgradeLevels
	public float[] cost = new float[4]; //Iron;
	public float costIron;
	public float costLode;
	public float costBrin;
	public float costMith;
	//public int level;
	public List<UpgradeDetail> details = new List<UpgradeDetail> ();
}

[System.Serializable]

public class UpgradeBit{ //UpgradeBit
	public bool bitOpen;
	public bitTypes bit;
	public int currentLevel = 0;
	public List<UpgradeLevels> upgradeLevels = new List<UpgradeLevels> ();
}

public class UpgradeScript : MonoBehaviour {

	//Does it have each bit?
	public bool mattock;
	public bool compression;
	public bool oscillation;
	public bool engine;
	public bool alembic;
	public bool silvered;

	//public List<List> all = new List<List> ();
	public List<bitTypes> commons = new List<bitTypes> ();
	public List<bitTypes> rares = new List<bitTypes> ();
	public bool lockUpgrades;

	
	//list of upgradeBits (ListTestSerialize)
	public List<UpgradeBit> upgradeDict = new List<UpgradeBit>();


	public void Install(bitTypes bit){
		//Debug.Log ("I'm Upgrading!! Hu-derp!!  "+bit.ToString ());
		for(int i = 0; i < upgradeDict.Count; i++){
			//Debug.Log ("Position "+i+": "+upgradeDict[i].bit);
			if(upgradeDict[i].bit == bit){
				Debug.Log ("Installing "+bit.ToString ());
				var uBit = upgradeDict[i];
				var level = uBit.upgradeLevels[uBit.currentLevel];
				foreach(UpgradeDetail detail in level.details){
					switch(detail.upgradeType)
					{
					case(UpgradeType.AttackCooldown):
						break;
					case(UpgradeType.AttackDamage):
						GetComponent<AttackScript>().attackDamage += detail.val;
						break;
					case(UpgradeType.AttackRange):
						GetComponent<AttackScript>().attackRange += detail.val;
						break;
					case(UpgradeType.AttackType):
						break;
					case(UpgradeType.DefenseRating):
						break;
					case(UpgradeType.DefenseReflect):
						break;
					case(UpgradeType.Defenses):
						break;
					case(UpgradeType.MiningCooldown):
						break;
					case(UpgradeType.MiningRange):
						break;
					}
				}
				uBit.currentLevel++;
			}
		}
		if (upgradeDict [0].currentLevel > 0 && upgradeDict [1].currentLevel > 0) {
			//Hide 2 & 3
			ClickBitScript[] cbs;
			cbs = GetComponentsInChildren<ClickBitScript>();
			foreach(ClickBitScript btn in cbs){
				if(btn.bit == upgradeDict[2].bit || btn.bit == upgradeDict[3].bit){
					btn.Hide ();
				}
			}
		}
		if (upgradeDict [2].currentLevel > 0) {
			//Hide 3
			if(upgradeDict[0].currentLevel > 0){
				//Hide 1
			}
			if(upgradeDict[1].currentLevel > 0){
				//Hide 0
			}
		}
		if (upgradeDict [3].currentLevel > 0) {
			//Hide 2		
			if(upgradeDict[0].currentLevel > 0){
				//Hide 1
			}
			if(upgradeDict[1].currentLevel > 0){
				//Hide 0
			}
		}
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (File.Exists (Application.persistentDataPath + "/" + this.name + ".dat")) {
			file = File.Open (Application.persistentDataPath + "/" + this.name + ".dat", FileMode.Open);
		}
		else {
			file = File.Create (Application.persistentDataPath + "/" + this.name + ".dat");
				//(Application.persistentDataPath + "/" + this.name + ".dat", FileMode.Create);
		}

		bf.Serialize (file, upgradeDict);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/" + this.name + ".dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/" + this.name + ".dat", FileMode.Open);
			upgradeDict = (List<UpgradeBit>)bf.Deserialize (file);
			file.Close ();
		}
	}
}