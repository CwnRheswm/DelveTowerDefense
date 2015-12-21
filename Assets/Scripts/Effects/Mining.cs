using UnityEngine;
using System.Collections;

public class Mining : MonoBehaviour {

	public float miningRange;

	//public float ironMiningRate;
	//public float lodestoneMiningRate;
	//public float brinmistrMiningRate;
	//public float mithrilMiningRate;

	//private int ironOres;
	//private int lodeOres;
	//private int brinOres;
	//private int mithOres;
	private float[] ores = new float[4];

	private VaultScript vault;
	public Missile missile;
	private Body body;
	public Tower tower;

	// Use this for initialization
	void OnEnable () {
		miningRange = transform.parent.GetComponent<Missile>().range;
		Debug.Log("Mining Range = "+miningRange);
		vault = VaultScript.vault;
		tower = transform.parent.GetComponent<Missile>().tower;
		GetOres();
		Mine();
		gameObject.SetActive(false);
	}
	
	public void Mine(){
		Debug.Log("Ores: " + ores[0]);
		tower.DepositOres(ores);
		/*
		var ironPlus = ironOres * ironMiningRate;
		var lodePlus = lodeOres * lodestoneMiningRate;
		var brinPlus = brinOres * brinmistrMiningRate;
		var mithPlus = mithOres * mithrilMiningRate;
		var coffer = vault.coffer;

		coffer["Iron"] += ironPlus;
		coffer["Lode"] += lodePlus;
		coffer["Brin"] += brinPlus;
		coffer["Mith"] += mithPlus;
		*/
	}
	void GetOres(){
		ores[0] = 0;
		ores[1] = 0;
		ores[2] = 0;
		ores[3] = 0;
		var hits = Physics.OverlapSphere(transform.position, miningRange);
		for (int i = 0; i < hits.Length; i++){
			if (hits[i].tag == "Iron"){
				ores[0] += 1;
				//trigger animation state of the ore gameObject
			}
		}
		Debug.Log("Iron Ores: "+ores[0]);
	}
}
